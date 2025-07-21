using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using Real_ChatApi.webUI.Controllers;
using Real_ChatApi.Dtos.GroupDtos;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections.Generic;
using System;
using Moq.Protected;

public class GroupControllerTests
{
    [Fact]
    public async Task Create_Post_ValidDto_ReturnsRedirectToIndex()
    {
        // Arrange
        var dto = new CreateGroupDto
        {
            Name = "Test Group",
            IsPrivate = false,
            InitialMemberUserIds = new List<Guid>()
        };

        // Mock HttpMessageHandler
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        handlerMock
           .Protected()
           .Setup<Task<HttpResponseMessage>>(
              "SendAsync",
              ItExpr.IsAny<HttpRequestMessage>(),
              ItExpr.IsAny<CancellationToken>()
           )
           .ReturnsAsync(new HttpResponseMessage
           {
               StatusCode = HttpStatusCode.OK,
               Content = new StringContent("true", Encoding.UTF8, "application/json"),
           })
           .Verifiable();

        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://real_chatapi.api:5000/")
        };

        // Mock IHttpClientFactory
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        httpClientFactoryMock
            .Setup(_ => _.CreateClient(It.IsAny<string>()))
            .Returns(httpClient);

        // Mock HttpContext User claim for CreatedByUserId
        var controller = new GroupController(httpClientFactoryMock.Object);
        var userId = Guid.NewGuid();

        var claimsPrincipal = new System.Security.Claims.ClaimsPrincipal(
            new System.Security.Claims.ClaimsIdentity(new[]
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, userId.ToString())
            })
        );

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext { User = claimsPrincipal }
        };

        // Act
        var result = await controller.Create(dto);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);

        // Verify HTTP call happened once
        handlerMock.Protected().Verify(
           "SendAsync",
           Times.Once(),
           ItExpr.IsAny<HttpRequestMessage>(),
           ItExpr.IsAny<CancellationToken>());
    }
}
