namespace ProiectDAW.Models
{
    public class Proposals  

    {
        public int ID { get; set; }

        public string Name { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public string ProposalBody { get; set; }

    }
}
