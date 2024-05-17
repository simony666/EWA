using Microsoft.AspNetCore.SignalR;

namespace Demo;

public class ChatHub : Hub
{
    
    private static List<object> list = new();

    public void SendText(string name, string text)
    {
      
        list.Add(new { name, text });
        // if message store more than 50 message clear the first one
        while (list.Count > 50) list.RemoveAt(0);


        // Clients.Caller (send message person)
        // Clients.Others(receive message person)
        // Clients.All(sender and receiver)
        Clients.All.SendAsync("ReceiveText", name, text);
    }

    public override Task OnConnectedAsync()
    {
        Clients.Caller.SendAsync("Initialize", list);
        return base.OnConnectedAsync();
    }
}
