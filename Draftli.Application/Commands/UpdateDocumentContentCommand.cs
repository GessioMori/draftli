using MediatR;

namespace Draftli.Application.Commands;
public record UpdateDocumentContentCommand(Guid DocumentId, string NewContent) : IRequest<long>;