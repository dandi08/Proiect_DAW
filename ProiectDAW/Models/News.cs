namespace ProiectDAW.Models
{
    public class News
    {
        public int Id { get; set; }
        public string NewsTitle { get; set; }
        public string Lead { get; set; }
        public string Content { get; set; }
        public string Writer { get; set; }
        public List<Comments> Comments { get; set; }=new List<Comments>();
        public DateTime Date { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }


    }
}
