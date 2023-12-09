namespace VezeetaManagement.Models.Domain
{
    public class TbImage
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
