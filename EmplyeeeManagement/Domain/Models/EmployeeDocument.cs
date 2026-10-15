using EmployeeManagement.Domain.Models;

public class EmployeeDocument
{
    public int EmployeeDocumentId { get; set; }

    public int EmployeeId { get; set; }

    public string DocumentName { get; set; } = string.Empty;

    public string FilePath { get; set; } = string.Empty;

    public string DocumentType { get; set; } = string.Empty;

    public DateTime UploadedOn { get; set; }

    public Employee Employee { get; set; } = null!;
}