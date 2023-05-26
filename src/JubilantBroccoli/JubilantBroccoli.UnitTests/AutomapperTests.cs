namespace JubilantBroccoli.UnitTests
{
    public class AutomapperTests
    {
        [Fact]
        [Trait("Automapper", "Mapper Configuration")]
        public void ItShouldCorrectlyConfiguredMapper()
        {
            //arrange
            var config = MapperRegistration.GetMapperConfiguration();

            //assert
            config.AssertConfigurationIsValid();
        }
    }
}