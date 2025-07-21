using Microsoft.AspNetCore.Mvc;
using Real_ChatApi.Dtos.MessageDtos;
using System.Security.Claims;
using System.Text.Json;
using System.Text;

namespace Real_ChatApi.webUI.Controllers
{
    public class MessageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MessageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index() { 

                       return View();
        }
        public async Task<IActionResult> Chat(Guid groupId)
        {
            var userIdString = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "sub")?.Value;
            if (userIdString == null || !Guid.TryParse(userIdString, out Guid userId))
                return Unauthorized();

            var client = _httpClientFactory.CreateClient("api");
            var response = await client.GetAsync($"Message/group?groupId={groupId}");
            if (!response.IsSuccessStatusCode)
                return View("Error");

            var json = await response.Content.ReadAsStringAsync();
            var messages = JsonSerializer.Deserialize<List<GetMessageDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            ViewBag.CurrentUserId = userId;
            ViewBag.GroupCreatorId = groupId; 

            ViewBag.GroupId = groupId;

            return View(messages);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMessage([FromBody] DeleteMessageDto command)
        {
            var client = _httpClientFactory.CreateClient("api");
            var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Delete, "Message/delete")
            {
                Content = content
            };

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return BadRequest();

            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> EditMessage([FromBody] UpdateMessageDto dto)
        {
            var client = _httpClientFactory.CreateClient("api");
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var response = await client.PutAsync("Message/edit", content);

            if (!response.IsSuccessStatusCode)
                return BadRequest();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] CreateMessageDto dto)
        {
            var client = _httpClientFactory.CreateClient("api");
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Message", content);

            if (!response.IsSuccessStatusCode)
                return BadRequest();

            return Ok();
        }
    }

}
