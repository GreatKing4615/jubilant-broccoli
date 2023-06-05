using System.ComponentModel.DataAnnotations;

namespace JubilantBroccoli.Domain.Dtos.User;

public class LoginRequest
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}