using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using tic_tac_toe.Hubs;
using tic_tac_toe.Services;

namespace tic_tac_toe.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class TicTacToeController : ControllerBase {

        private readonly ILogger<TicTacToeController> _logger;
        private readonly TicTacToeService _service;

        public TicTacToeController(ILogger<TicTacToeController> logger, TicTacToeService service) {
            _logger = logger;
            _service = service;
        }

        [HttpPost("game")]
        public IActionResult Post(string player) {
            _service.joinGame(player);

            return Ok();
        }

        [HttpGet("game")]
        public IActionResult Get(string player) {
            var state = _service.getGameState(player);
            return Ok(state);
        }

        [HttpPatch("game")]
        public IActionResult Patch(string player, int number) {
            _service.modifyCell(player, number);
            return Ok();
        }

        [HttpDelete("game/{i}")]
        public IActionResult Delete(int i) {
            _service.resetGame(i);
            return Ok();
        }

        
    }
}