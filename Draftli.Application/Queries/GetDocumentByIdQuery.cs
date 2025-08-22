using Draftli.Shared.Entities;
using MediatR;

namespace Draftli.Application.Queries;
public record GetDocumentByIdQuery(Guid DocumentId) : IRequest<Document?>;