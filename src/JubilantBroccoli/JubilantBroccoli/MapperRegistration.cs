using System.Reflection;
using AutoMapper;
using JubilantBroccoli.Infrastructure.Mappers.Base;

namespace JubilantBroccoli;
public class MapperRegistration
{
    public static MapperConfiguration GetMapperConfiguration()
    {
        var profiles = GetProfiles();
        return new MapperConfiguration(cfg =>
        {
            foreach (var profile in profiles.Select(profile => (Profile)Activator.CreateInstance(profile)))
            {
                cfg.AddProfile(profile);
            }
        });
    }

    private static List<Type> GetProfiles()
    {
        return (from t in typeof(Program).GetTypeInfo().Assembly.GetTypes()
            where typeof(IAutomapper).IsAssignableFrom(t) && !t.GetTypeInfo().IsAbstract
            select t).ToList();
    }
}