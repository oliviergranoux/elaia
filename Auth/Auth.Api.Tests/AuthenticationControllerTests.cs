using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;

using Elaia.Auth.Api.Controllers;
using Elaia.Auth.Api.Services;
using Elaia.Auth.Api.Common.Interfaces;
using Elaia.Auth.Business.Interfaces;

namespace Elaia.Auth.Api.Tests
{
    public class Tests
    {
        #region Properties
        private Mock<IUserManager> _userManagerMock;
        private Mock<IAuthenticationService> _authenticationSvcMock;
        private Mock<IClaimsPrincipalService> _claimsPrincipalSvcMock;

        private Mock<IOptions<AppSettings>> _settingsMock;
        private Mock<ILogger<AuthenticationController>> _loggerMock;
        #endregion

        [SetUp]
        public void Setup()
        {
            _userManagerMock = new Mock<IUserManager>();
            _authenticationSvcMock = new Mock<IAuthenticationService>();
            _claimsPrincipalSvcMock = new Mock<IClaimsPrincipalService>();

            _settingsMock = new Mock<IOptions<AppSettings>>();
            _loggerMock = new Mock<ILogger<AuthenticationController>>();
        }

        [Test]
        public void Test_01_Login_ArgumentNullException()
        {
            //Arrange
            _userManagerMock.Setup(x => x.GetUser(It.IsAny<Api.Common.Models.Login>()))
                .Returns(new Business.Common.Models.User());         

            //Act
            var authenticationCtl = new AuthenticationController(_userManagerMock.Object, _authenticationSvcMock.Object, _claimsPrincipalSvcMock.Object, _settingsMock.Object, _loggerMock.Object);

            //Assert
            Assert.Throws<ArgumentNullException>(()=>authenticationCtl.Login(null));
        }

        [Test]
        public void Test_02_Login_NotFound()
        {
            //Arrange
            _userManagerMock.Setup(x => x.GetUser(It.IsAny<Api.Common.Models.Login>()))
                .Returns((Business.Common.Models.User)null);
            
            _authenticationSvcMock.Setup(x => x.Authenticate(It.IsAny<Business.Common.Models.User>()))
                .Returns("test");

            //Act
            var authenticationCtl = new AuthenticationController(_userManagerMock.Object, _authenticationSvcMock.Object, _claimsPrincipalSvcMock.Object, _settingsMock.Object, _loggerMock.Object);
            var result = authenticationCtl.Login(new Api.Common.Models.Login());

            //Assert
            Assert.IsNotNull(result);

            var notFoundResult = result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [Test]
        public void Test_03_Login()
        {
            //Arrange
            _userManagerMock.Setup(x => x.GetUser(It.IsAny<Api.Common.Models.Login>()))
                .Returns(new Business.Common.Models.User());
            
            _authenticationSvcMock.Setup(x => x.Authenticate(It.IsAny<Business.Common.Models.User>()))
                .Returns("test");

            //Act
            var authenticationCtl = new AuthenticationController(_userManagerMock.Object, _authenticationSvcMock.Object, _claimsPrincipalSvcMock.Object, _settingsMock.Object, _loggerMock.Object);
            var result = authenticationCtl.Login(new Api.Common.Models.Login());

            //Assert
            Assert.IsNotNull(result);

            var okObjectResult = result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.AreEqual("test", okObjectResult.Value);
        }
    }
}