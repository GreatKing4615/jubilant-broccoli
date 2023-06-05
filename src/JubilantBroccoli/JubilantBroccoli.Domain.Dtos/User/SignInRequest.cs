using System.ComponentModel.DataAnnotations;

namespace JubilantBroccoli.Domain.Dtos.User;

public class SignInRequest: LoginRequest
{
    [Required]
    public string Email { get; set; }

    public string? Address { get; set; }
}