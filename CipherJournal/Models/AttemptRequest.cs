namespace CipherJournal.Models;

public record AttemptRequest(string Answer);

public record AttemptResult(bool Correct, string? Reward, string? Message);
