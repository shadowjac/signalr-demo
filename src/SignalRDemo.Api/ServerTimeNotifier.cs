using Microsoft.AspNetCore.SignalR;

namespace SignalRDemo.Api;

public class ServerTimeNotifier : BackgroundService
{
    private static readonly TimeSpan Period = TimeSpan.FromSeconds(5);
    private readonly IHubContext<NotificationHub, INotificationClient> _hubContext;
    private readonly ILogger<ServerTimeNotifier> _logger;

    public ServerTimeNotifier(IHubContext<NotificationHub, INotificationClient> hubContext,
        ILogger<ServerTimeNotifier> logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(Period);

        while (!stoppingToken.IsCancellationRequested &&
               await timer.WaitForNextTickAsync(stoppingToken))
        {
            var datetime = DateTime.Now;
            _logger.LogInformation("Executing {Service} {Name}", nameof(ServerTimeNotifier), datetime);
            await _hubContext.Clients.User("AndresC").ReceiveNotification($"Server time = {datetime}");
        }
    }
}