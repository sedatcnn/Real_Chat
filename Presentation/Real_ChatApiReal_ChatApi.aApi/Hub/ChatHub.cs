using MediatR;
using Microsoft.AspNetCore.SignalR;
using Real_ChatApi.Application.Features.MedaitR.Commands.MessageCommand;
using Real_ChatApi.Infrastructure.Redis;

public class ChatHub : Hub
{
    private readonly IMediator _mediator;
    private readonly MessageCacheService _cache;

    public ChatHub(IMediator mediator, MessageCacheService cache)
    {
        _mediator = mediator;
        _cache = cache;
    }

    public async Task SendMessage(string groupId, string userId, string content, string? fileUrl)
    {
        var command = new CreateMessageCommand
        {
            GroupId = Guid.Parse(groupId),
            SenderUserId = Guid.Parse(userId),
            Text = content,
            FileUrl = fileUrl
        };

        var messageId = await _mediator.Send(command);

        await _cache.CacheMessageAsync(messageId, content);

        await Clients.Group(groupId).SendAsync("ReceiveMessage", new
        {
            Id = messageId,
            GroupId = groupId,
            SenderUserId = userId,
            Content = content,
            FileUrl = fileUrl,
            SentAt = DateTime.UtcNow,
            IsDeleted = false,
            IsEdited = false
        });
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var groupId = httpContext?.Request.Query["groupId"];

        if (!string.IsNullOrEmpty(groupId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var httpContext = Context.GetHttpContext();
        var groupId = httpContext?.Request.Query["groupId"];

        if (!string.IsNullOrEmpty(groupId))
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
        }

        await base.OnDisconnectedAsync(exception);
    }
}
