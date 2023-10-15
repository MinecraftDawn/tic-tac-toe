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
        private readonly IHubContext<TestHub> _hubContext;

        public TicTacToeController(ILogger<TicTacToeController> logger, TicTacToeService service, IHubContext<TestHub> hubContext) {
            _logger = logger;
            _service = service;
            _hubContext = hubContext;
        }

        [HttpPost("game")]
        public IActionResult Post(string player) {
            _service.joinGame(player);
            _hubContext.Clients.All.SendAsync("ReceiveMessage", "test signalR");

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

        [HttpDelete("game")]
        public IActionResult Delete() {
            _service.resetGame();
            return Ok();
        }

        
    }
}