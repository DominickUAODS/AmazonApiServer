using System.ComponentModel.DataAnnotations;

public class EmailVerificationCode
{
	public Guid Id { get; set; } = Guid.NewGuid();

	[Required]
	public string Email { get; set; } = string.Empty;

	[Required]
	public string HashedPassword { get; set; } = string.Empty;

	[Required]
	public string Code { get; set; } = string.Empty;

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public int ExpireMinutes { get; set; } = 10;

	public bool IsExpired => DateTime.UtcNow > CreatedAt.AddMinutes(ExpireMinutes);
}
