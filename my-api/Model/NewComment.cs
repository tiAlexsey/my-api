namespace my_api.Model
{
    public class NewComment
    {
        public int FilmId { get; set; }
        public string? Text { get; set; }
        public int UserId { get; set; }
    }
}