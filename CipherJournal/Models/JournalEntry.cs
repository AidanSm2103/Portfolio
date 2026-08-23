namespace CipherJournal.Models;

public class JournalEntry
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string EncodedText { get; set; } = string.Empty;
    public string PlainText { get; set; } = string.Empty; // never sent to the client directly
    public int Shift { get; set; } // Caesar cipher shift amount
    public string RewardMessage { get; set; } = string.Empty;
    public string Hint { get; set; } = string.Empty;
}

// What actually gets sent to the client for GET requests — no plaintext, no reward
public record JournalEntrySummary(int Id, string Title, string EncodedText);
