namespace BankApplication.Models
{
    public enum CardType
    {
        None,
        Debit,
        Credit,
        Special
    }
    
    public class CardSample
    {
        public int Id { get; set; }
        public CardType Type { get; set; }
        public string Name { get; set; }
        public List<Feature> Features { get; set; }
    }

    public class Feature
    {
        public int Id { get; set; }
        public int CardSampleId { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }

        public CardSample CardSample { get; set; }
    }
}
