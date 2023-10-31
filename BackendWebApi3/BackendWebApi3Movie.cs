namespace BackendWebApi3
{
    public class BackendWebApi3Movie
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public string? FromSource { get; set; }
    }
}
