using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SignalRDemo.Api;

//dotnet user-jwts create -n "AndresC"
[Authorize]
public class NotificationHub : Hub<INotificationClient>
{
    public override async Task OnConnectedAsync()
    {
        await Clients.Client(Context.ConnectionId).ReceiveNotification(
            $"Thanks for connecting {Context.User?.Identity?.Name}");
        await base.OnConnectedAsync();
    }
}

public interface INotificationClient
{
    Task ReceiveNotification(string message);
}