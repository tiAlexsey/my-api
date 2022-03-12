namespace my_api
{
    public class Comment
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
        public User User { get; set; }

        public Comment(int id, int filmId, string text, int userId, int like, int dislike)
        {
            Id = id;
            FilmId = filmId;
            Text = text;
            UserId = userId;
            Like = like;
            Dislike = dislike;
        }
    }
}
