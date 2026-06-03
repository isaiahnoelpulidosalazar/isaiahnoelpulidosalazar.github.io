import os
import sqlite3
from datetime import datetime, date
from functools import wraps
from flask import Flask, render_template, request, redirect, url_for, session, jsonify, flash, send_from_directory
from werkzeug.security import generate_password_hash, check_password_hash
from werkzeug.utils import secure_filename
from flask_cloudflared import run_with_cloudflared

app = Flask(__name__)
app.secret_key = 'accitrack-secret-key-2024'
app.config['UPLOAD_FOLDER'] = os.path.join(os.path.dirname(__file__), 'uploads')
app.config['MAX_CONTENT_LENGTH'] = 100 * 1024 * 1024

ALLOWED_EXTENSIONS = {'png', 'jpg', 'jpeg', 'gif', 'mp4', 'mov', 'avi', 'webm'}

ACCIDENT_TYPES = [
    "Minor Traffic Accident - Property Damage only",
    "Reckless Driving - Dangerous Operation",
    "DUI/DWI - Impaired Driver",
    "Hit and Run - Fleeing Suspect",
    "Multi-vehicle Pileup - 3+ Vehicles",
    "Reckless Imprudence Resulting in Homicide - Fatal Negligence"
]

DB_PATH = os.path.join(os.path.dirname(__file__), 'accitrack.db')


def get_db():
    conn = sqlite3.connect(DB_PATH)
    conn.row_factory = sqlite3.Row
    return conn


def init_db():
    conn = get_db()
    c = conn.cursor()

    c.execute('''CREATE TABLE IF NOT EXISTS users (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        username TEXT UNIQUE NOT NULL,
        badge_number TEXT UNIQUE NOT NULL,
        password_hash TEXT NOT NULL,
        first_name TEXT NOT NULL,
        last_name TEXT NOT NULL,
        middle_name TEXT DEFAULT '',
        contact TEXT DEFAULT '',
        role TEXT DEFAULT 'officer',
        is_online INTEGER DEFAULT 0,
        created_at TEXT DEFAULT CURRENT_TIMESTAMP
    )''')

    c.execute('''CREATE TABLE IF NOT EXISTS reports (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        officer_id INTEGER NOT NULL,
        location TEXT NOT NULL,
        accident_type TEXT NOT NULL,
        evidence_filename TEXT DEFAULT NULL,
        evidence_type TEXT DEFAULT NULL,
        status TEXT DEFAULT 'pending',
        submitted_at TEXT DEFAULT CURRENT_TIMESTAMP,
        reviewed_at TEXT DEFAULT NULL,
        reviewed_by INTEGER DEFAULT NULL,
        notes TEXT DEFAULT '',
        FOREIGN KEY(officer_id) REFERENCES users(id),
        FOREIGN KEY(reviewed_by) REFERENCES users(id)
    )''')

    admin_exists = c.execute("SELECT id FROM users WHERE username='admin'").fetchone()
    if not admin_exists:
        c.execute('''INSERT INTO users (username, badge_number, password_hash, first_name, last_name, role)
                     VALUES (?, ?, ?, ?, ?, ?)''',
                  ('admin', 'ADMIN-001', generate_password_hash('admin123'),
                   'System', 'Administrator', 'admin'))

    officer_exists = c.execute("SELECT id FROM users WHERE username='officer1'").fetchone()
    if not officer_exists:
        c.execute('''INSERT INTO users (username, badge_number, password_hash, first_name, last_name, middle_name, contact, role)
                     VALUES (?, ?, ?, ?, ?, ?, ?, ?)''',
                  ('officer1', 'PNP-12345', generate_password_hash('officer123'),
                   'Juan', 'Dela Cruz', 'Santos', '09171234567', 'officer'))

    conn.commit()
    conn.close()


def allowed_file(filename):
    return '.' in filename and filename.rsplit('.', 1)[1].lower() in ALLOWED_EXTENSIONS


def login_required(f):
    @wraps(f)
    def decorated(*args, **kwargs):
        if 'user_id' not in session:
            return redirect(url_for('login'))
        return f(*args, **kwargs)
    return decorated


def admin_required(f):
    @wraps(f)
    def decorated(*args, **kwargs):
        if 'user_id' not in session:
            return redirect(url_for('login'))
        if session.get('role') != 'admin':
            return redirect(url_for('dashboard'))
        return f(*args, **kwargs)
    return decorated


def get_current_user():
    if 'user_id' not in session:
        return None
    conn = get_db()
    user = conn.execute("SELECT * FROM users WHERE id=?", (session['user_id'],)).fetchone()
    conn.close()
    return user


def get_greeting():
    hour = datetime.now().hour
    if hour < 12:
        return "Good Morning"
    elif hour < 18:
        return "Good Afternoon"
    else:
        return "Good Evening"


def get_stats(officer_id=None):
    conn = get_db()
    today = date.today().isoformat()
    yesterday_dt = datetime.now()

    if officer_id:
        total_recent = conn.execute(
            "SELECT COUNT(*) FROM reports WHERE officer_id=? AND DATE(submitted_at)=?",
            (officer_id, today)).fetchone()[0]
        resolved_today = conn.execute(
            "SELECT COUNT(*) FROM reports WHERE officer_id=? AND status='approved' AND DATE(reviewed_at)=?",
            (officer_id, today)).fetchone()[0]
        yesterday_count = conn.execute(
            "SELECT COUNT(*) FROM reports WHERE officer_id=? AND DATE(submitted_at)=DATE('now','-1 day')",
            (officer_id,)).fetchone()[0]
    else:
        total_recent = conn.execute(
            "SELECT COUNT(*) FROM reports WHERE DATE(submitted_at)=?", (today,)).fetchone()[0]
        resolved_today = conn.execute(
            "SELECT COUNT(*) FROM reports WHERE status='approved' AND DATE(reviewed_at)=?",
            (today,)).fetchone()[0]
        yesterday_count = conn.execute(
            "SELECT COUNT(*) FROM reports WHERE DATE(submitted_at)=DATE('now','-1 day')").fetchone()[0]

    online_count = conn.execute("SELECT COUNT(*) FROM users WHERE is_online=1").fetchone()[0]
    pending_count = conn.execute("SELECT COUNT(*) FROM reports WHERE status='pending'").fetchone()[0]
    conn.close()

    diff = total_recent - yesterday_count
    return {
        'recent_incidents': total_recent,
        'incidents_diff': diff,
        'resolved_today': resolved_today,
        'online_personnel': online_count,
        'pending_count': pending_count
    }


@app.route('/', methods=['GET', 'POST'])
def login():
    if 'user_id' in session:
        return redirect(url_for('dashboard'))

    error = None
    if request.method == 'POST':
        username = request.form.get('username', '').strip()
        badge = request.form.get('badge_number', '').strip()
        password = request.form.get('password', '')

        conn = get_db()
        user = conn.execute(
            "SELECT * FROM users WHERE username=? AND badge_number=?",
            (username, badge)).fetchone()
        conn.close()

        if user and check_password_hash(user['password_hash'], password):
            session['user_id'] = user['id']
            session['role'] = user['role']
            session['name'] = f"{user['first_name']} {user['last_name']}"

            conn = get_db()
            conn.execute("UPDATE users SET is_online=1 WHERE id=?", (user['id'],))
            conn.commit()
            conn.close()
            return redirect(url_for('dashboard'))
        else:
            error = "Invalid credentials. Please check your username, badge number, and password."

    return render_template('login.html', error=error)


@app.route('/logout')
def logout():
    if 'user_id' in session:
        conn = get_db()
        conn.execute("UPDATE users SET is_online=0 WHERE id=?", (session['user_id'],))
        conn.commit()
        conn.close()
    session.clear()
    return redirect(url_for('login'))


@app.route('/dashboard')
@login_required
def dashboard():
    user = get_current_user()
    if not user:
        return redirect(url_for('login'))

    if user['role'] == 'admin':
        return redirect(url_for('admin_home'))
    return redirect(url_for('officer_home'))


@app.route('/officer/home')
@login_required
def officer_home():
    user = get_current_user()
    if user['role'] == 'admin':
        return redirect(url_for('admin_home'))

    stats = get_stats(officer_id=user['id'])
    conn = get_db()
    active_cases = conn.execute(
        '''SELECT r.*, u.first_name, u.last_name FROM reports r
           JOIN users u ON r.officer_id=u.id
           WHERE r.status='approved' AND r.officer_id=?
           ORDER BY r.reviewed_at DESC LIMIT 5''',
        (user['id'],)).fetchall()
    conn.close()

    greeting = get_greeting()
    today_str = datetime.now().strftime("%A, %B %d, %Y")
    return render_template('officer_home.html', user=user, stats=stats,
                           active_cases=active_cases, greeting=greeting, today_str=today_str,
                           active_page='home')


@app.route('/officer/reports', methods=['GET', 'POST'])
@login_required
def officer_reports():
    user = get_current_user()
    if user['role'] == 'admin':
        return redirect(url_for('admin_reports'))

    if request.method == 'POST':
        location = request.form.get('location', '').strip()
        accident_type = request.form.get('accident_type', '').strip()
        evidence_filename = None
        evidence_type = None

        if 'evidence' in request.files:
            file = request.files['evidence']
            if file and file.filename and allowed_file(file.filename):
                ext = file.filename.rsplit('.', 1)[1].lower()
                filename = secure_filename(f"evidence_{user['id']}_{int(datetime.now().timestamp())}.{ext}")
                file.save(os.path.join(app.config['UPLOAD_FOLDER'], filename))
                evidence_filename = filename
                evidence_type = 'video' if ext in {'mp4', 'mov', 'avi', 'webm'} else 'image'

        conn = get_db()
        conn.execute(
            '''INSERT INTO reports (officer_id, location, accident_type, evidence_filename, evidence_type, status, submitted_at)
               VALUES (?, ?, ?, ?, ?, 'pending', ?)''',
            (user['id'], location, accident_type, evidence_filename, evidence_type,
             datetime.now().strftime('%Y-%m-%d %H:%M:%S')))
        conn.commit()
        conn.close()
        return jsonify({'success': True, 'message': 'Report submitted successfully.'})

    conn = get_db()
    reports = conn.execute(
        '''SELECT r.*, u.first_name, u.last_name, u.badge_number FROM reports r
           JOIN users u ON r.officer_id=u.id
           WHERE r.officer_id=? ORDER BY r.submitted_at DESC''',
        (user['id'],)).fetchall()
    conn.close()

    return render_template('officer_reports.html', user=user, reports=reports,
                           accident_types=ACCIDENT_TYPES, active_page='reports')


@app.route('/officer/notifications')
@login_required
def officer_notifications():
    user = get_current_user()
    if user['role'] == 'admin':
        return redirect(url_for('admin_notifications'))

    conn = get_db()
    reports = conn.execute(
        '''SELECT r.*, u.first_name, u.last_name FROM reports r
           JOIN users u ON r.officer_id=u.id
           WHERE r.officer_id=? ORDER BY r.submitted_at DESC''',
        (user['id'],)).fetchall()
    conn.close()

    return render_template('officer_notifications.html', user=user, reports=reports, active_page='notifications')


@app.route('/officer/profile', methods=['GET', 'POST'])
@login_required
def officer_profile():
    user = get_current_user()
    if request.method == 'POST':
        first_name = request.form.get('first_name', '').strip()
        last_name = request.form.get('last_name', '').strip()
        middle_name = request.form.get('middle_name', '').strip()
        contact = request.form.get('contact', '').strip()
        new_password = request.form.get('new_password', '').strip()

        conn = get_db()
        if new_password:
            pw_hash = generate_password_hash(new_password)
            conn.execute(
                '''UPDATE users SET first_name=?, last_name=?, middle_name=?, contact=?, password_hash=?
                   WHERE id=?''',
                (first_name, last_name, middle_name, contact, pw_hash, user['id']))
        else:
            conn.execute(
                '''UPDATE users SET first_name=?, last_name=?, middle_name=?, contact=? WHERE id=?''',
                (first_name, last_name, middle_name, contact, user['id']))
        conn.commit()
        conn.close()

        session['name'] = f"{first_name} {last_name}"
        return jsonify({'success': True, 'message': 'Profile updated successfully.'})

    return render_template('officer_profile.html', user=user, active_page='profile')


@app.route('/admin/home')
@admin_required
def admin_home():
    user = get_current_user()
    stats = get_stats()
    conn = get_db()
    active_cases = conn.execute(
        '''SELECT r.*, u.first_name, u.last_name FROM reports r
           JOIN users u ON r.officer_id=u.id
           WHERE r.status='approved'
           ORDER BY r.reviewed_at DESC LIMIT 5''').fetchall()
    pending_reports = conn.execute(
        '''SELECT r.*, u.first_name, u.last_name, u.badge_number FROM reports r
           JOIN users u ON r.officer_id=u.id
           WHERE r.status='pending' ORDER BY r.submitted_at DESC LIMIT 10''').fetchall()
    conn.close()

    greeting = get_greeting()
    today_str = datetime.now().strftime("%A, %B %d, %Y")
    return render_template('admin_home.html', user=user, stats=stats,
                           active_cases=active_cases, pending_reports=pending_reports,
                           greeting=greeting, today_str=today_str, active_page='home')


@app.route('/admin/reports', methods=['GET'])
@admin_required
def admin_reports():
    user = get_current_user()
    conn = get_db()
    reports = conn.execute(
        '''SELECT r.*, u.first_name, u.last_name, u.badge_number FROM reports r
           JOIN users u ON r.officer_id=u.id
           ORDER BY r.submitted_at DESC''').fetchall()
    conn.close()
    return render_template('admin_reports.html', user=user, reports=reports,
                           accident_types=ACCIDENT_TYPES, active_page='reports')


@app.route('/admin/report/<int:report_id>/action', methods=['POST'])
@admin_required
def admin_report_action(report_id):
    action = request.form.get('action')
    notes = request.form.get('notes', '')

    status_map = {'approve': 'approved', 'decline': 'declined', 'close': 'closed'}
    new_status = status_map.get(action)
    if not new_status:
        return jsonify({'success': False, 'message': 'Invalid action.'})

    conn = get_db()
    conn.execute(
        '''UPDATE reports SET status=?, reviewed_at=?, reviewed_by=?, notes=? WHERE id=?''',
        (new_status, datetime.now().strftime('%Y-%m-%d %H:%M:%S'), session['user_id'], notes, report_id))
    conn.commit()
    conn.close()
    return jsonify({'success': True, 'message': f'Report {new_status}.'})


@app.route('/admin/notifications')
@admin_required
def admin_notifications():
    user = get_current_user()
    conn = get_db()
    reports = conn.execute(
        '''SELECT r.*, u.first_name, u.last_name, u.badge_number FROM reports r
           JOIN users u ON r.officer_id=u.id
           ORDER BY r.submitted_at DESC''').fetchall()
    conn.close()
    return render_template('admin_notifications.html', user=user, reports=reports, active_page='notifications')


@app.route('/admin/users', methods=['GET'])
@admin_required
def admin_users():
    user = get_current_user()
    conn = get_db()
    users = conn.execute("SELECT * FROM users ORDER BY created_at DESC").fetchall()
    conn.close()
    return render_template('admin_users.html', user=user, users=users, active_page='users')


@app.route('/admin/users/create', methods=['POST'])
@admin_required
def admin_create_user():
    data = request.form
    username = data.get('username', '').strip()
    badge = data.get('badge_number', '').strip()
    password = data.get('password', '').strip()
    first_name = data.get('first_name', '').strip()
    last_name = data.get('last_name', '').strip()
    middle_name = data.get('middle_name', '').strip()
    contact = data.get('contact', '').strip()
    role = data.get('role', 'officer')

    if not all([username, badge, password, first_name, last_name]):
        return jsonify({'success': False, 'message': 'Required fields missing.'})

    try:
        conn = get_db()
        conn.execute(
            '''INSERT INTO users (username, badge_number, password_hash, first_name, last_name, middle_name, contact, role)
               VALUES (?, ?, ?, ?, ?, ?, ?, ?)''',
            (username, badge, generate_password_hash(password), first_name, last_name, middle_name, contact, role))
        conn.commit()
        conn.close()
        return jsonify({'success': True, 'message': 'User created successfully.'})
    except sqlite3.IntegrityError as e:
        return jsonify({'success': False, 'message': 'Username or badge number already exists.'})


@app.route('/admin/users/<int:user_id>/update', methods=['POST'])
@admin_required
def admin_update_user(user_id):
    data = request.form
    first_name = data.get('first_name', '').strip()
    last_name = data.get('last_name', '').strip()
    middle_name = data.get('middle_name', '').strip()
    contact = data.get('contact', '').strip()
    new_password = data.get('new_password', '').strip()

    conn = get_db()
    if new_password:
        conn.execute(
            '''UPDATE users SET first_name=?, last_name=?, middle_name=?, contact=?, password_hash=? WHERE id=?''',
            (first_name, last_name, middle_name, contact, generate_password_hash(new_password), user_id))
    else:
        conn.execute(
            '''UPDATE users SET first_name=?, last_name=?, middle_name=?, contact=? WHERE id=?''',
            (first_name, last_name, middle_name, contact, user_id))
    conn.commit()
    conn.close()
    return jsonify({'success': True, 'message': 'User updated successfully.'})


@app.route('/admin/users/<int:user_id>/delete', methods=['POST'])
@admin_required
def admin_delete_user(user_id):
    if user_id == session['user_id']:
        return jsonify({'success': False, 'message': 'Cannot delete your own account.'})
    conn = get_db()
    conn.execute("DELETE FROM users WHERE id=?", (user_id,))
    conn.commit()
    conn.close()
    return jsonify({'success': True, 'message': 'User deleted.'})


@app.route('/uploads/<filename>')
@login_required
def uploaded_file(filename):
    return send_from_directory(app.config['UPLOAD_FOLDER'], filename)


if __name__ == '__main__':
    os.makedirs(app.config['UPLOAD_FOLDER'], exist_ok=True)
    init_db()
    run_with_cloudflared(app)
    app.run(debug=True, port=5000)
