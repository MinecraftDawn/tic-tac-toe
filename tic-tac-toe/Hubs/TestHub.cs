using Microsoft.AspNetCore.SignalR;

namespace tic_tac_toe.Hubs {
    public class TestHub : Hub {

        public async Task SendMessage(string user, string message) {
            Console.WriteLine(user + " : "+ message);
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }


    }
}
