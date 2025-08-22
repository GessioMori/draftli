using Draftli.Application.Commands;
using Draftli.Shared.Interfaces;
using MediatR;

namespace Draftli.Application.Handlers;
public class UpdateDocumentContentHandler : IRequestHandler<UpdateDocumentContentCommand, long>
{
    private readonly IDocumentRepository documentRepository;

    public UpdateDocumentContentHandler(IDocumentRepository documentRepository)
    {
        this.documentRepository = documentRepository;
    }

    public async Task<long> Handle(UpdateDocumentContentCommand request, CancellationToken cancellationToken)
    {
        long version = await this.documentRepository.SaveContentAsync(request.DocumentId, request.NewContent);
        return version;
    }
}