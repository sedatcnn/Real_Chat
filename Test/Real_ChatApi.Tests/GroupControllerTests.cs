using Moq;
using Xunit;
using Real_ChatApi.webUI.Controllers;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using Real_ChatApi.Dtos.GroupDtos;

public class GroupControllerTests
{
    [Fact]
    public async Task Create_ValidDto_ReturnsRedirect()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        var dto = new CreateGroupDto { Name = "Test Grup", IsPrivate = false, CreatedByUserId = Guid.NewGuid() };

        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        var httpClient = new HttpClient(new FakeHttpMessageHandler(responseMessage));

        httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var controller = new GroupController(httpClientFactoryMock.Object);

        // Act
        var result = await controller.Create(dto);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
    }
}

public class FakeHttpMessageHandler : HttpMessageHandler
{
    private readonly HttpResponseMessage _response;

    public FakeHttpMessageHandler(HttpResponseMessage response)
    {
        _response = response;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
    {
        return Task.FromResult(_response);
    }
}
