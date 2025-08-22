using Microsoft.AspNetCore.SignalR;

namespace Draftli.Api.Hubs;

public class DocumentHub : Hub
{
    public async Task JoinDocument(Guid documentId)
    {
        await this.Groups.AddToGroupAsync(this.Context.ConnectionId, documentId.ToString());
    }
}