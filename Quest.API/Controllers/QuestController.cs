using Microsoft.AspNetCore.Mvc;
using Quest.Core.Interfaces;
using Quest.Core.Dto.Responses;
using Quest.Core.Dto.Requests;

namespace Quest.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class QuestController : ControllerBase
    {
        private readonly IQuestService _service;

        public QuestController(IQuestService service) 
        { 
            _service = service;
        }

        // GET: /api/state
        [HttpGet("state")]
        public async Task<IActionResult> Get([FromQuery] string playerId)
        {
            if (playerId == null) return BadRequest();

            var playerState = await _service.GetPlayerState(playerId);
            if (playerState == null) return NotFound();

            return Ok(new StateResponse(playerState.TotalQuestPercentCompleted, playerState.LastMilestoneIndexCompleted));
        }

        
         // POST /api/progress
         [HttpPost("progress")]
         public async Task<IActionResult> PostProgress([FromBody] ProgressRequest request)
         {
            var result = await _service.AddOrUpdatePlayerQuestState(request);
            return Ok(result);
         }
       
    }
}
