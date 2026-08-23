<div align="center">

# 🗝️ Portfolio

**Aidan Smith** — Software Engineering Student

[![Live Site](https://img.shields.io/badge/Live%20Site-aidansm2103.github.io-F7A72C?style=for-the-badge)](https://aidansm2103.github.io/Portfolio/)
[![API](https://img.shields.io/badge/API-Render-6C5CE7?style=for-the-badge)](https://cipherjournal.onrender.com/entries)
[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)

A personal portfolio site built as two connected pieces — a static front-end and a live ASP.NET Core API.
</div>

---

## 🔍 What this is

Most student portfolios describe projects in prose. This one includes a small, real, full-stack feature: solve a Caesar-cipher puzzle on the site, and it unlocks a personal message + a reward.

| | |
|---|---|
| 🌐 **Live site** | [aidansm2103.github.io/Portfolio](https://aidansm2103.github.io/Portfolio/) |
| ⚙️ **API** | [cipherjournal.onrender.com](https://cipherjournal.onrender.com/entries) |

> **Note:** the API runs on Render's free tier, which sleeps after inactivity. The first request after a while can take 20–40 seconds to wake up — normal, not broken.

---

## 🧩 Architecture

```
Portfolio/
├── CipherJournal/        ASP.NET Core Web API (.NET 8, Minimal APIs)
│   ├── Models/            JournalEntry, AttemptRequest, AttemptResult
│   ├── Services/          CipherService — Caesar encode/decode, attempt checking
│   ├── Dockerfile          Multi-stage build for Render deployment
│   └── Program.cs          Endpoints, CORS, DI wiring
│
└── Frontend/              Static site (HTML / CSS / vanilla JS)
    ├── css/style.css        Dark, archive-themed styling
    ├── js/script.js          Talks to the live API — fetch, decode, reveal
    ├── files/                 Hosted assets (CV PDF)
    └── index.html
```

Two apps, deployed independently, talking over a REST API with locked-down CORS — deliberately separated to demonstrate front-end/back-end architecture rather than a single bundled site.

---

## ⚙️ Tech stack

| Layer | Tech |
|---|---|
| API | ASP.NET Core 8, Minimal APIs, Swagger/OpenAPI |
| Front-end | HTML, CSS, vanilla JavaScript |
| Data | In-memory, seeded on startup |
| API hosting | Render (Docker) |
| Site hosting | GitHub Pages (via GitHub Actions) |

---

## 📡 API reference

| Method | Route | Description |
|---|---|---|
| `GET` | `/entries` | List journal entries (encoded text only) |
| `GET` | `/entries/{id}` | Get a single entry |
| `GET` | `/entries/{id}/hint` | Get a hint for an entry |
| `POST` | `/entries/{id}/attempt` | Submit a decode guess |

Full interactive docs available via Swagger when running in Development mode.

---

## 🖥️ Running locally

**API**
```bash
cd CipherJournal
dotnet run
```
Runs at `https://localhost:<port>` with Swagger UI at `/swagger` in Development mode.

**Front-end**
```bash
cd Frontend
# Serve with any static server, e.g. VS Code's Live Server extension
```
Update `API_BASE` in `js/script.js` to match your local API port.

---

## 🚀 Deployment

- **`CipherJournal/`** is deployed on **Render** via a multi-stage `Dockerfile`, with `PORT` read from the environment at runtime.
- **`Frontend/`** is deployed on **GitHub Pages** via a GitHub Actions workflow (`.github/workflows/deploy-frontend.yml`) that publishes only the `Frontend/` folder on every push.
- CORS on the API is scoped specifically to the deployed front-end's origin — no wildcard access.
