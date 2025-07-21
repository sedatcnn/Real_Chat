using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Real_ChatApi.Dtos.UserGroups;
using System.Security.Claims;
using System.Text;

namespace Real_ChatApi.webUI.Controllers
{
    public class GroupUserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GroupUserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(Guid groupId)
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.GetAsync($"UserGroup/get?groupId={groupId}");
            if (!response.IsSuccessStatusCode)
                return View("Error");

            var json = await response.Content.ReadAsStringAsync();
            var members = JsonConvert.DeserializeObject<List<GroupMemberDto>>(json);

            ViewBag.GroupId = groupId;
            return View(members);
        }

        [HttpGet]
        public IActionResult AddUser(Guid groupId)
        {
            var model = new AddUserToGroupDto
            {
                GroupId = groupId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserToGroupDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var userIdString = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "sub")?.Value;
            if (userIdString == null || !Guid.TryParse(userIdString, out Guid currentUserId))
                return Unauthorized();


            var client = _httpClientFactory.CreateClient("api");
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("UserGroup/add", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index", new { groupId = dto.GroupId });

            ModelState.AddModelError(string.Empty, "Kullanıcı eklenemedi.");
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUser(RemoveMembersDto dto)
        {
            var userIdString = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "sub")?.Value;
            if (userIdString == null || !Guid.TryParse(userIdString, out Guid currentUserId))
                return Unauthorized();

            var client = _httpClientFactory.CreateClient("api");
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Delete, "UserGroup/remove")
            {
                Content = content
            };

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index", new { groupId = dto.GroupId });

            return BadRequest("Kullanıcı silinemedi.");
        }
    }
}
