


namespace DOTNET_DIARIES.Models 
{
    public class Blogpost
    {
        public int Id { get; set; }
        public string Title { get; set ;} = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime PostedDate { get; set; }
        public List<BlogpostTag> BlogpostTags { get; set; } = new();
    }
}