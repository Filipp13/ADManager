using ADManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using Xunit;

namespace Tests
{
    public class ADManagerTests
    {
        [Fact]
        public void AddAddADManagmentNullConfigThrowArgumentNullException()
        {
            Mock<IConfiguration> configurationSectionStub = new Mock<IConfiguration>();
            IServiceCollection services = new ServiceCollection();
            Assert.Throws<ArgumentNullException>(() => {
                services.AddADManagment(configurationSectionStub.Object, new ADManagerSecurityOptions());
                var context = services.BuildServiceProvider().GetService<IADManager>();
            });
        }

        [Fact]
        public void AddAddADManagmentNullConfigThrowInvalidOperationException()
        {

            Mock<IConfigurationSection> mockSection = new Mock<IConfigurationSection>();
            mockSection.Setup(x => x.Value).Returns("Config");

            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x.GetSection(It.Is<string>(k => k == "AD"))).Returns(mockSection.Object);

            IServiceCollection services = new ServiceCollection();
            Assert.Throws<InvalidOperationException>(() => {
                services.AddADManagment(mockConfig.Object, new ADManagerSecurityOptions());
                var context = services.BuildServiceProvider().GetService<IADManager>();
            });
        }
    }
}
