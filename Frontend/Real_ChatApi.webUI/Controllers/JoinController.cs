using Microsoft.AspNetCore.Mvc;
using Real_ChatApi.Dtos.JoinDtos;
using System.Security.Claims;
using System.Text.Json;
using System.Text;

namespace Real_ChatApi.webUI.Controllers
{
    public class JoinController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public JoinController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }
        private Guid? GetUserId()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "sub");
            return claim != null && Guid.TryParse(claim.Value, out var userId) ? userId : null;
        }

        [HttpPost]
        public async Task<IActionResult> RequestJoin(Guid groupId, Guid approverUserId)
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            var dto = new JoinRequestDto
            {
                GroupId = groupId,
                RequestingUserId = userId.Value,
                ApproverUserId = approverUserId
            };

            var client = _httpClientFactory.CreateClient("api");
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("JoinGroup/request", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index", "Group");

            return BadRequest("Katılma isteği gönderilemedi.");
        }

        [HttpPost]
        public async Task<IActionResult> JoinPublic(Guid groupId)
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            var dto = new JoinPublicDto
            {
                GroupId = groupId,
                UserId = userId.Value
            };

            var client = _httpClientFactory.CreateClient("api");
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("JoinGroup/join-public", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index", "Group");

            return BadRequest("Public gruba katılım başarısız.");
        }

        [HttpPost]
        public async Task<IActionResult> ApproveRequest(Guid requestId)
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            var dto = new ApproveDto
            {
                RequestId = requestId,
                ApprovingUserId = userId.Value
            };

            var client = _httpClientFactory.CreateClient("api");
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("JoinGroup/approve", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("JoinRequests");

            return BadRequest("Onaylama işlemi başarısız.");
        }

        [HttpPost]
        public async Task<IActionResult> RejectRequest(Guid requestId)
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            var dto = new RejectDto
            {
                RequestId = requestId,
                RejectingUserId = userId.Value
            };

            var client = _httpClientFactory.CreateClient("api");
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("JoinGroup/reject", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("JoinRequests");

            return BadRequest("Reddetme işlemi başarısız.");
        }

        [HttpGet]
        public async Task<IActionResult> JoinRequests(Guid groupId)
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.GetAsync($"JoinGroup/get-requests?groupId={groupId}");

            if (!response.IsSuccessStatusCode)
                return BadRequest("İstekler alınamadı");

            var json = await response.Content.ReadAsStringAsync();
            var requests = JsonSerializer.Deserialize<List<GetJoinDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(requests);
        }
    }
}
