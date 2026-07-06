function showTab(tabId) {
    document.querySelectorAll('.tab-content').forEach(el => el.classList.remove('active'));
    document.querySelectorAll('.tab-btn').forEach(el => el.classList.remove('active'));
    
    document.getElementById(tabId).classList.add('active');
    event.currentTarget.classList.add('active');
}
function checkAnswer(btn, isCorrect) {
    const feedback = document.getElementById('quiz-feedback');
    const options = document.querySelectorAll('.quiz-option');
    
    options.forEach(opt => {
        opt.disabled = true;
        opt.style.cursor = 'default';
    });
    if (isCorrect) {
        btn.classList.add('correct');
        feedback.innerHTML = "Correct! She was the pioneering female force of the Thirteen Moderns.";
        feedback.style.color = "#28A745";
    } else {
        btn.classList.add('wrong');
        feedback.innerHTML = "Not quite! Try exploring her biography again.";
        feedback.style.color = "#DC3545";
        
        options.forEach(opt => {
            if (opt.textContent.includes('Thirteen Moderns')) {
                opt.classList.add('correct');
            }
        });
    }
}
function startVideo() {
    const cover = document.getElementById('videoCover');
    cover.style.opacity = '0';
    setTimeout(() => {
        cover.style.display = 'none';
    }, 400);
}