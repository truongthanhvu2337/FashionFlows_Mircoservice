namespace FashionFlows.Payment.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public Guid PaymentId { get; set; }
    public string TransactionType { get; set; } 
    public decimal Amount { get; set; }
    public string Status { get; set; } = "Pending"; 
    public string? StripeTransactionId { get; set; }
    public string? url { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Payment Payment { get; set; }
}

