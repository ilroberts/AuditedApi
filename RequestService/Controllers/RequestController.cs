using Microsoft.AspNetCore.Mvc;

namespace RequestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        public RequestController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("ClientService");
        }

        [HttpGet]
        public async Task<ActionResult> MakeRequest()
        {
            var response = await _httpClient.GetAsync("http://localhost:5001/api/forecast");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Request was successful");
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                Console.WriteLine("Request failed");
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }
    }
}
