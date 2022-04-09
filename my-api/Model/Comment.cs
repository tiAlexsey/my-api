using System.ComponentModel.DataAnnotations.Schema;

namespace my_api.Model
{
    public class Comment
    {
        [Column("Id")] public int Id { get; set; }
        [Column("FilmId")] public int FilmId { get; set; }
        [Column("sText")] public string Text { get; set; }
        [Column("UserId")] public int UserId { get; set; }
        [Column("iLike")] public int Like { get; set; }
        [Column("iDislike")] public int Dislike { get; set; }
        public User User { get; set; }

        public Comment(int filmId, string text, int userId)
        {
            Id = 0;
            FilmId = filmId;
            UserId = userId;
            Text = text;
            Like = 0;
            Dislike = 0;
        }
    }
}