using JubilantBroccoli.Domain.Models;

namespace JubilantBroccoli.Seed;

public static class UserHelper
{
    public const string personEmail1 = "pika@gmail.com";
    public const string personEmail2 = "anothercoolmail@gmail.com";

    public static string GetPasswordsByEmail(string email)
    {
        var superSecretPass = string.Empty;
        switch (email)
        {
            case personEmail1:
                {
                    superSecretPass = "pikapikapikapikapikapika";
                    break;
                }
            case personEmail2:
                {
                    superSecretPass = "ţ̷̪̖̺͈̑̇̅͗̀̽̚ŗ̸͍͖̰̼̹̈́u̶̡̎̾̊̆͐͠ͅs̶̮͕͚̕t̵̻̜̙̱̦̄̉̒ ̴͚̣̳̿͗͗̒͌͌̕n̸̻͗͆̊̒̓ö̸̭̭̱͕̅̀͋ ̸̥͔̾̎͊o̴͕̽͊̎̏̈́͒͝n̷̠̩̔̄͛́̿ê̵̠̹̯̻͋̀̍͘̚";
                    break;
                }
            default:
                break;
        }
        return superSecretPass;
    }

    public static List<User> GetUsers()
    {
        var personUserName1 = "TestPokemon";
        var phone1 = "123123123";

        var personUserName2 = "CoolGuy123";
        var phone2 = "8-800-555-35-35";

        return new()
        {
            new User
            {
                Email = personEmail1,
                EmailConfirmed = true,
                NormalizedEmail = personEmail1.ToUpper(),
                PhoneNumber = phone1,
                UserName = personUserName1,
                PhoneNumberConfirmed = true,
                NormalizedUserName = personUserName1.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D"),
                Role = AppData.UserRoleName
            },
            new User
            {
                Email = personEmail2,
                EmailConfirmed = true,
                NormalizedEmail = personEmail2.ToUpper(),
                PhoneNumber = phone2,
                UserName = personUserName2,
                PhoneNumberConfirmed = true,
                NormalizedUserName = personUserName2.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D"),
                Role = AppData.AdministratorRoleName
            },
        };
    }

}