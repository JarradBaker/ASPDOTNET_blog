


namespace DOTNET_DIARIES.Models 
{
    public class Blogpost
    {
        public int Id { get; set; }
        public string Title { get; set ;}
        public string ImageUrl { get; set; }
        public string Content { get; set; }
        public DateTime PostedDate { get; set; }
        public List<BlogpostTag>? BlogpostTags { get; set; }
    }
}