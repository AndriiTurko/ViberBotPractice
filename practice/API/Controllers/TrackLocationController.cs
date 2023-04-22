using Microsoft.AspNetCore.Mvc;
using practice.BLL.Interfaces;

namespace practice.API.Controllers
{
    [ApiController]
    [Route("/test")]
    public class TrackLocationController : ControllerBase
    {
        private readonly ITrackLocationService _trackLocationService;

        public TrackLocationController(ITrackLocationService trackLocationService)
        {
            _trackLocationService = trackLocationService;
        }

        [HttpGet("getInfo", Name = "Get Info on All Walks")]
        public async Task<IActionResult> UpdateItemQuantity([FromQuery] string imei)
        {
            return StatusCode(200, await _trackLocationService.GetOverallInfoOnWalks(imei));
        }
    }
}
