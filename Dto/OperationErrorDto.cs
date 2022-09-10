namespace BankApplication.Dto;

public class OperationErrorDto
{
    public Operation Operation { get; set; }
    public string Error { get; set; } = String.Empty;
    public bool Status { get; set; }
}