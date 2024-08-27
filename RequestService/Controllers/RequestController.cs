using Microsoft.AspNetCore.Mvc;
using RequestService.Policies;

namespace RequestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController(ClientPolicy clientPolicy) : ControllerBase
    {
        private readonly ClientPolicy _clientPolicy = clientPolicy;

        [HttpGet]
        public async Task<ActionResult> MakeRequest()
        {
            var client = new HttpClient();
            var response = await _clientPolicy.ImmediateHttpRetry.ExecuteAsync(
              () => client.GetAsync("http://localhost:5001/api/forecast"));

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
