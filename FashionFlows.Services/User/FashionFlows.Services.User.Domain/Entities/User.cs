namespace FashionFlows.Services.Account.Domain.Entities;

public class User
{
    public Guid UserId { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string Status { get; set; } = null!;

    public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }
}

