using CipherJournal.Models;

namespace CipherJournal.Services;

public class CipherService
{
    private readonly List<JournalEntry> _entries;

    public CipherService()
    {
        // Seed data — in-memory for now, same JSON-persistence pattern as Evidence Locker could be added later
        _entries = new List<JournalEntry>
        {
            new JournalEntry
            {
                Id = 1,
                Title = "Entry No. 001",
                Shift = 3,
                PlainText = "the archive remembers what you forget",
                RewardMessage = "You cracked it! Since you made it this far you must be a thorough recruiter so here's my <a href=\"files/Aidan_Smith_CV.pdf\" target=\"_blank\">CV</a>. I build things properly, even the parts nobody's meant to see.",
                Hint = "It's a Caesar cipher — each letter is shifted forward in the alphabet by x positions. Try shifting back 3 positions. (e.g. 'd' becomes 'a')"
            }
        };

        foreach (var entry in _entries)
        {
            entry.EncodedText = Encode(entry.PlainText, entry.Shift);
        }
    }

    public List<JournalEntrySummary> GetAllSummaries() =>
        _entries.Select(e => new JournalEntrySummary(e.Id, e.Title, e.EncodedText)).ToList();

    public JournalEntrySummary? GetSummaryById(int id) =>
        _entries.Where(e => e.Id == id)
                .Select(e => new JournalEntrySummary(e.Id, e.Title, e.EncodedText))
                .FirstOrDefault();

    public string? GetHint(int id) =>
        _entries.FirstOrDefault(e => e.Id == id)?.Hint;

    public AttemptResult CheckAttempt(int id, string answer)
    {
        var entry = _entries.FirstOrDefault(e => e.Id == id);
        if (entry is null)
            return new AttemptResult(false, null, "Entry not found.");

        var normalizedGuess = answer.Trim().ToLowerInvariant();
        var normalizedAnswer = entry.PlainText.Trim().ToLowerInvariant();

        if (normalizedGuess == normalizedAnswer)
        {
            return new AttemptResult(true, entry.RewardMessage, "Decoded successfully.");
        }

        return new AttemptResult(false, null, "Not quite — try again.");
    }

    private static string Encode(string text, int shift)
    {
        var result = new char[text.Length];
        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];
            if (char.IsLetter(c))
            {
                char baseChar = char.IsUpper(c) ? 'A' : 'a';
                result[i] = (char)(((c - baseChar + shift) % 26 + 26) % 26 + baseChar);
            }
            else
            {
                result[i] = c;
            }
        }
        return new string(result);
    }
}
