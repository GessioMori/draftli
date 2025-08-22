namespace Draftli.Shared.Interfaces;
public interface IDocumentRepository
{
    Task<long> SaveContentAsync(Guid documentId, string content);
}