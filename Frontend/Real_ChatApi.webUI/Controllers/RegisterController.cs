using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Real_ChatApi.Dtos.UserDtos;
using System.Text;

namespace Real_ChatApi.webUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public RegisterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(RegisterUserDto creatUserDto)
        {
            if (ModelState.IsValid)
            {
                var clientPost = _httpClientFactory.CreateClient();
                var userList = JsonConvert.SerializeObject(creatUserDto);
                StringContent stringContent = new StringContent(userList, Encoding.UTF8, "application/json");
                var responseMessage = await clientPost.PostAsync("http://real_chatapi.api:5000/api/Registers/register", stringContent);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ModelState.AddModelError("", "Kayıt başarısız. Tekrar dene");
                }
            }

            return View("Index", creatUserDto);
        }

    }
}

