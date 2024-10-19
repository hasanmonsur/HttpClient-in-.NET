using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWebApiClient.Models;
using RestWebApiClient.Services;

namespace RestWebApiClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyApiController : ControllerBase
    {
        private readonly MyApiService _apiService;

        public MyApiController(MyApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetData(int id)
        {
            var result = await _apiService.GetAsync<MyDataModel>($"data/{id}");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostData(MyDataModel data)
        {
            var result = await _apiService.PostAsync<MyDataModel>("data", data);
            return Ok(result);
        }
    }
}
