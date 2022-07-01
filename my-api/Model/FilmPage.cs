namespace my_api.Model
{
    public class FilmPage
    {
        public Film Film { get; }
        public List<Comment> Comments { get; }

        public FilmPage(Film film, List<Comment> comments)
        {
            Film = film;
            Comments = comments;
        }
    }
}