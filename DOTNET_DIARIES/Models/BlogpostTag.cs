namespace DOTNET_DIARIES.Models
{
    public class BlogpostTag
    {
        public int BlogpostId { get; set; }
        public Blogpost Blogpost { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}