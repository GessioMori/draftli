using Draftli.Api.Hubs;
using Draftli.Application.Commands;
using Draftli.Application.Queries;
using Draftli.Shared.Consts;
using Draftli.Shared.DTOs;
using Draftli.Shared.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Draftli.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DocumentsController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IHubContext<DocumentHub> hubContext;

    public DocumentsController(IMediator mediator, IHubContext<DocumentHub> hubContext)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this.hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetDocument(Guid id)
    {
        Document? document = await this.mediator.Send(new GetDocumentByIdQuery(id));

        return document is null
            ? this.NotFound()
            : this.Ok(new DocumentDto(document.Id, document.Content, document.Version));
    }

    [HttpPost("{id:guid}/update")]
    public async Task<IActionResult> UpdateDocument(Guid id, [FromBody] string content)
    {
        long newVersion = await this.mediator.Send(new UpdateDocumentContentCommand(id, content));

        await this.hubContext.Clients.Group(id.ToString())
            .SendAsync(HubMethods.DocumentUpdated, new DocumentDto(id, content, newVersion));

        return this.Ok(new { Version = newVersion });
    }
}