using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public Comment(int id, int filmId, string text, int userId, int like = 0, int dislike = 0)
        {
            Id = id;
            FilmId = filmId;
            Text = text;
            UserId = userId;
            Like = like;
            Dislike = dislike;
        }
        public Comment(NewComment newComment)
        {
            Id = newComment.Id;
            FilmId = newComment.FilmId;
            Text = newComment.Text;
            UserId = newComment.UserId;
            Like = newComment.Like;
            Dislike = newComment.Dislike;
        }

        public static List<Comment> convertToComment(List<NewComment> comments)
        {
            List<Comment> commentsWithUser = new List<Comment>();

            foreach (NewComment c in comments)
            {
                commentsWithUser.Add(new Comment(c));
            }

            return commentsWithUser;
        }
    }
}
