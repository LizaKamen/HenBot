namespace HenBot.Models
{
    public record Post
    {
        public int Id { get; set; }
        public string File_Url { get; set; }
        public string Sample_Url { get; set; }
        public int Sample { get; set; }
    }
}
