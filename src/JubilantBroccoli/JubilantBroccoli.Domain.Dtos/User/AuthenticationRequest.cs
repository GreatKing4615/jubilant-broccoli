using System.ComponentModel.DataAnnotations;

namespace JubilantBroccoli.Domain.Dtos.User;

public class AuthenticationRequest
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }

    public string? Address { get; set; }
}