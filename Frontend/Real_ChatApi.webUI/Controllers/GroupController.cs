using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Real_ChatApi.Dtos.GroupDtos;
using Real_ChatApi.Dtos.UserDtos;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Real_ChatApi.webUI.Controllers
{
    public class GroupController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GroupController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://real_chatapi.api:5000/api/GroupContorller/get");

            if (!response.IsSuccessStatusCode) return View("Error");

            var json = await response.Content.ReadAsStringAsync();
            var groups = JsonConvert.DeserializeObject<List<GetGroupDto>>(json);
            return View(groups);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userList = await GetUserSelectListAsync();
            ViewBag.Users = userList;

            return View(new CreateGroupDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGroupDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Users = await GetUserSelectListAsync();
                return View(dto);
            }

            var userIdString = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "sub")?.Value;
            if (userIdString == null || !Guid.TryParse(userIdString, out Guid userId))
                return Unauthorized();

            dto.CreatedByUserId = userId;

            dto.InitialMemberUserIds ??= new List<Guid>();
            if (!dto.InitialMemberUserIds.Contains(userId))
                dto.InitialMemberUserIds.Add(userId);

            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://real_chatapi.api:5000/api/GroupContorller/creat", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View("Error");
        }

        private async Task<List<SelectListItem>> GetUserSelectListAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://real_chatapi.api:5000/api/User");

            if (!response.IsSuccessStatusCode)
                return new List<SelectListItem>();

            var json = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<GetUserDto>>(json);

            return users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.UserName
            }).ToList();
        }
    }
}
