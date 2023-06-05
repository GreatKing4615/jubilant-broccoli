using AutoMapper;
using JubilantBroccoli.MapperProfiles;
using Xunit;

namespace JubilantBroccoli.UnitTests
{
    public class AutoMapperTests
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ItemMapperConfiguration>();
                cfg.AddProfile<ItemOptionMapperConfiguration>();
                cfg.AddProfile<OrderMapperConfiguration>();
                cfg.AddProfile<RestaurantMapperConfiguration>();
                cfg.AddProfile<UserMapperConfiguration>();
            });

            // Act & Assert
            configuration.AssertConfigurationIsValid();
        }
    }
}