using Microsoft.AspNetCore.SignalR;
namespace TaskManager.Api.MessageBroker

{
    public class SignalRManager : Hub
    {
        public async Task SendMessage()
        {
            await Clients.All.SendAsync("ReceiveMessage");
        }
    }
}
