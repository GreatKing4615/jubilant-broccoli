namespace JubilantBroccoli.Domain.Dtos.User;

public class AuthenticationResponse
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}