namespace DOTNET_DIARIES.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BlogpostTag>? BlogpostTags { get; set; }
    }
}