using Draftli.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Draftli.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DocumentsController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult GetDocument(int id)
    {
        DocumentDto document = new(id, "Sample Document", 4);

        if (document == null)
        {
            return this.NotFound();
        }
        return this.Ok(document);
    }
}