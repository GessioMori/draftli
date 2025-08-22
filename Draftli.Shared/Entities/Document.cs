namespace Draftli.Shared.Entities;
public class Document
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public long Version { get; set; }
}