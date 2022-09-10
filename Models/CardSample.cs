using BankApplication.Models.Enums;

namespace BankApplication.Models
{       
    public class CardSample
    {
        public int Id { get; set; }
        public CardType Type { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string ImageUrl { get; set; }
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
