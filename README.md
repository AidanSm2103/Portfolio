# Portfolio

Aidan Smith's personal portfolio site, built as two connected pieces:

- **[`CipherJournal/`](./CipherJournal)** — a lightweight ASP.NET Core Web API serving encoded journal entries and checking decode attempts.
- **[`Frontend/`](./Frontend)** — the static portfolio site (HTML/CSS/JS) that showcases my projects and hosts an interactive cipher-cracking panel powered by the API.

---

## Why this exists

Most student portfolios describe projects. This one includes a small, real, full-stack feature: a Caesar-cipher puzzle backed by a live API, built specifically to demonstrate:

- Designing and building a thin REST API from scratch
- Connecting a static front-end to a separately hosted backend
- Clean separation of concerns between presentation and API logic

Solve the cipher on the site to unlock a short message with an additional reward.

---

## Tech stack

| Layer      | Tech                                   |
|------------|------------------------------------------|
| API        | ASP.NET Core (.NET 8), Minimal APIs     |
| Front-end  | HTML, CSS, vanilla JavaScript           |
| Data       | In-memory (seeded on startup)           |
| Docs       | Swagger / OpenAPI (dev environment)     |

---

## Project structure

```
Portfolio/
├── CipherJournal/       # ASP.NET Core Web API
│   ├── Models/           # JournalEntry, AttemptRequest, etc.
│   ├── Services/         # CipherService — encode/decode/check logic
│   └── Program.cs        # Endpoints + app configuration
│
└── Frontend/             # Static portfolio site
    ├── css/
    ├── js/
    ├── files/            # Hosted assets (e.g. CV PDF)
    └── index.html
```

---

## Running locally

**API:**
1. Open `CipherJournal/CipherJournal.csproj` in Visual Studio (or run `dotnet run` from the `CipherJournal/` folder).
2. Run using the `https` launch profile so it starts in Development mode with Swagger enabled.
3. API will be available at `https://localhost:<port>` — check the console output for the exact port.
4. Swagger UI: `https://localhost:<port>/swagger`

**Front-end:**
1. Open the `Frontend/` folder in VS Code.
2. Use the Live Server extension (or any static file server) to serve `index.html`.
3. Make sure `API_BASE` in `js/script.js` matches the port your API is running on locally.

---

## API endpoints

| Method | Route                     | Description                                  |
|--------|----------------------------|-----------------------------------------------|
| GET    | `/entries`                 | List all journal entries (encoded text only) |
| GET    | `/entries/{id}`             | Get a single entry                            |
| GET    | `/entries/{id}/hint`        | Get a hint for an entry                       |
| POST   | `/entries/{id}/attempt`     | Submit a decode guess                         |

