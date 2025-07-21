using Microsoft.AspNetCore.Mvc;
using Real_ChatApi.Dtos.UserDtos;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Real_ChatApi.webUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.GetAsync("user");
            if (!response.IsSuccessStatusCode)
                return View("Error");

            var json = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<GetUserDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.DeleteAsync($"user/delete/{id}");

            if (!response.IsSuccessStatusCode)
                return BadRequest("Kullanıcı silinemedi.");

            return RedirectToAction(nameof(Index));
        }
    }
}
