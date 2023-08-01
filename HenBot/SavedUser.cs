namespace HenBot
{
    public class SavedUser
    {
        public int Step { get; set; }
        public int Page { get; set; } = 0;
        public int Limit { get; set; } = 10;
        public bool IsConfiguring { get; set; } = false;
        public bool IsAyaya { get; set; } = false;
        public bool IsAyayaed { get; set; } = false;
        public string LastTag { get; set; }
        public List<string> SavedTags { get; set; } = new List<string>() { };
        public Ratings SettedRating { get; set; } = Ratings.General;
    }
}
