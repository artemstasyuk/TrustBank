namespace BankApplication.ViewModels;

public class HistoryViewModel
{
    public int CardId { get; set; }
    public List<Operation> Operations { get; set; } = new ();
}