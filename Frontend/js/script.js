const API_BASE = "https://cipherjournal.onrender.com"; 

async function loadCipherPanel() {
  const panel = document.getElementById("cipher-panel");

  try {
    const res = await fetch(`${API_BASE}/entries`);
    const entries = await res.json();
    const entry = entries[0]; 

    panel.innerHTML = `
      <p class="encoded-text">${entry.encodedText}</p>
      <form id="cipher-form">
        <input type="text" id="cipher-guess" placeholder="Type your answer..." autocomplete="off" />
        <button type="submit">Decode</button>
      </form>
      <button id="hint-btn" class="hint-btn">Need a hint?</button>
      <p id="cipher-feedback"></p>
    `;

    document.getElementById("cipher-form").addEventListener("submit", (e) => {
      e.preventDefault();
      submitGuess(entry.id);
    });

    document.getElementById("hint-btn").addEventListener("click", () => {
      showHint(entry.id);
    });
  } catch (err) {
    panel.innerHTML = `<p class="placeholder-note">Couldn't reach the cipher archive right now.</p>`;
    console.error(err);
  }
}

async function submitGuess(id) {
  const input = document.getElementById("cipher-guess");
  const feedback = document.getElementById("cipher-feedback");
  const guess = input.value;

  try {
    const res = await fetch(`${API_BASE}/entries/${id}/attempt`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ answer: guess }),
    });
    const result = await res.json();

    if (result.correct) {
      revealReward(result.reward);
    } else {
      feedback.textContent = result.message || "Not quite — try again.";
      feedback.className = "feedback-wrong";
    }
  } catch (err) {
    feedback.textContent = "Something went wrong reaching the archive.";
    console.error(err);
  }
}

async function showHint(id) {
  const feedback = document.getElementById("cipher-feedback");
  try {
    const res = await fetch(`${API_BASE}/entries/${id}/hint`);
    const data = await res.json();
    feedback.textContent = `Hint: ${data.hint}`;
    feedback.className = "feedback-hint";
  } catch (err) {
    console.error(err);
  }
}

function revealReward(message) {
  const panel = document.getElementById("cipher-panel");
  panel.innerHTML = `<p class="reward-message">${message}</p>`;
}

loadCipherPanel();