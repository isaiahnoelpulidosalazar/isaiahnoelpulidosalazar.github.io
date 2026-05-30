const express = require('express');
const { WebSocketServer } = require('ws');
const { v4: uuidv4 } = require('uuid');
const http = require('http');
const path = require('path');
const cors = require('cors');

const app = express();
app.use(cors({ origin: '*' })); 
app.use(express.json());
app.use(express.static(path.join(__dirname, '../')));

const server = http.createServer(app);
const wss = new WebSocketServer({ server });

const sessions = new Map();

setInterval(() => {
  const now = Date.now();
  for (const [id, session] of sessions.entries()) {
    const inactive = now - session.lastActive > 30 * 60 * 1000;
    const ended = session.status === 'ENDED' && now - session.lastActive > 60 * 1000;
    if (inactive || ended) {
      if (session.host) try { session.host.close(); } catch (e) {}
      if (session.viewer) try { session.viewer.close(); } catch (e) {}
      sessions.delete(id);
      console.log(`[cleanup] Session ${id} removed`);
    }
  }
}, 5 * 60 * 1000);

app.post('/create-session', (req, res) => {
  const sessionId = uuidv4();
  sessions.set(sessionId, {
    sessionId,
    host: null,
    viewer: null,
    status: 'WAITING',
    lastActive: Date.now(),
  });
  res.json({ sessionId });
  console.log(`[session] Created: ${sessionId}`);
});

app.get('/session/:id', (req, res) => {
  const session = sessions.get(req.params.id);
  if (!session) return res.status(404).json({ error: 'Session not found' });
  res.json({
    sessionId: session.sessionId,
    status: session.status,
    hasHost: !!session.host,
    hasViewer: !!session.viewer,
  });
});

function send(ws, msg) {
  if (ws && ws.readyState === 1) {
    ws.send(JSON.stringify(msg));
  }
}

wss.on('connection', (ws, req) => {
  const url = new URL(req.url, 'http://localhost');
  const sessionId = url.pathname.replace('/ws/', '').replace('ws/', '');
  
  const session = sessions.get(sessionId);
  if (!session) {
    send(ws, { type: 'error', payload: 'Session not found' });
    ws.close();
    return;
  }

  let role = null;

  if (!session.host) {
    role = 'host';
    session.host = ws;
    console.log(`[ws] Host joined session ${sessionId}`);
    send(ws, { type: 'join', sender: 'server', sessionId, payload: { role: 'host', status: session.status } });

    if (session.viewer) {
      session.status = 'ACTIVE';
      session.lastActive = Date.now();
      send(ws, { type: 'join', sender: 'viewer', sessionId, payload: { status: 'ACTIVE' } });
      send(session.viewer, { type: 'join', sender: 'host', sessionId, payload: { status: 'ACTIVE' } });
    }
  } else if (!session.viewer) {
    role = 'viewer';
    session.viewer = ws;
    console.log(`[ws] Viewer joined session ${sessionId}`);

    if (session.host) {
      session.status = 'ACTIVE';
      session.lastActive = Date.now();
      send(ws, { type: 'join', sender: 'server', sessionId, payload: { role: 'viewer', status: 'ACTIVE' } });
      send(session.host, { type: 'join', sender: 'viewer', sessionId, payload: { status: 'ACTIVE' } });
    } else {
      send(ws, { type: 'join', sender: 'server', sessionId, payload: { role: 'viewer', status: 'WAITING' } });
    }
  } else {
    send(ws, { type: 'error', payload: 'Session is full' });
    ws.close();
    return;
  }

  ws.on('message', (data) => {
    session.lastActive = Date.now();
    let msg;
    try {
      msg = JSON.parse(data);
    } catch {
      send(ws, { type: 'error', payload: 'Invalid JSON' });
      return;
    }

    const { type } = msg;

    if (role === 'host' && session.viewer) {
      send(session.viewer, msg);
    } else if (role === 'viewer' && session.host) {
      send(session.host, msg);
    }

    // Ignore WebRTC setup spam in logs
    if (!['screen-frame', 'offer', 'answer', 'ice-candidate'].includes(type)) {
      console.log(`[ws] [${role}→relay] type=${type} session=${sessionId}`);
    }
  });

  ws.on('close', () => {
    console.log(`[ws] ${role} disconnected from session ${sessionId}`);
    session.lastActive = Date.now();

    if (role === 'host') {
      session.host = null;
      session.status = 'ENDED';
      send(session.viewer, { type: 'disconnect', sender: 'host', sessionId, payload: 'Host ended the session' });
    } else if (role === 'viewer') {
      session.viewer = null;
      if (session.status === 'ACTIVE') session.status = 'WAITING';
      send(session.host, { type: 'disconnect', sender: 'viewer', sessionId, payload: 'Viewer disconnected' });
    }
  });

  ws.on('error', (err) => {
    console.error(`[ws] Error (${role}, ${sessionId}):`, err.message);
  });
});

const PORT = process.env.PORT || 3000;
server.listen(PORT, () => console.log(`Server running on port ${PORT}`));