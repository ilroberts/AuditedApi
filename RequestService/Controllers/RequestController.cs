using Microsoft.AspNetCore.Mvc;

namespace RequestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController(IHttpClientFactory httpClientFactory) : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        [HttpGet]
        public async Task<ActionResult> MakeRequest()
        {
            var client = _httpClientFactory.CreateClient("ClientService");
            var response = await client.GetAsync("http://localhost:5001/api/forecast");

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
