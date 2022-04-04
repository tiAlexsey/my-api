using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using my_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace my_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("_myAllowSpecificOrigins")]

#nullable disable warnings
    public class FilmController : ControllerBase
    {
        public FilmController()
        {
        }

        [HttpGet("list")]
        public CommonResponse GetFilmList(int page = 1, int count = 10)
        {
            List<Film> films;
            using (ApplicationContext db = new())
            {
                films = db.Films.ToList();
            }
            int iBegin = page * count - count;
            int iEnd = page * count;
            iEnd = (iEnd>films.Count) ? films.Count : iEnd;
            List<Film> filmPage = new(page);
            CommonResponse response;
            try
            {
                for (int i = iBegin; i < iEnd; i++)
                {
                    filmPage.Add(films[i]);
                }
                response = new CommonResponse(filmPage, films.Count);
            }
            catch (IndexOutOfRangeException)
            {
                response = new CommonResponse(films, films.Count);
            }
            return response;
        }

        [HttpGet("item/{id:int}")]
        public CommonResponse GetFilm(int id)
        {
            List<Comment> comments;

            using (ApplicationContext db = new())
            {
                comments = db.Comments
                    .Where(f => f.FilmId == id)
                    .Include(u => u.User)
                    .ToList();
            }

            Film film;
            using (ApplicationContext db = new())
            {
                try
                {
                    film = db.Films
                    .Where(x => x.Id==id)
                    .Single();
                }
                catch (InvalidOperationException)
                {
                    return new CommonResponse("Film not found");
                }
            }

            FilmPage filmPage = new();
            filmPage.Film=film;
            filmPage.Comments=comments;
            CommonResponse response = new(filmPage);
            return response;
        }


        [HttpGet("search")]
        public CommonResponse SeachFilmByName(string name)
        {
            List<Film> films = new();
            using (ApplicationContext db = new())
            {
                films = db.Films
                    .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                    .ToList();
            }

            CommonResponse response = new(films);
            return response;
        }

        [HttpPost("comment/add")]
        public CommonResponse AddComment(NewComment newComment)
        {
            Comment comment = new(newComment.FilmId, newComment.Text, newComment.UserId);

            using (ApplicationContext db = new())
            {
                db.Comments.Add(comment);
                db.SaveChanges();
            }

            CommonResponse response = new(comment, "Comment added");
            return response;
        }

        [HttpPost("comment/like")]
        public CommonResponse LikeComment(int idComment, bool type)
        {
            Comment comment;
            using (ApplicationContext db = new())
            {
                comment = db.Comments
                    .Where(x => x.Id == idComment)
                    .FirstOrDefault();
                if (type)
                {
                    comment.Like++;
                }
                else if (comment.Like > 0)
                {
                    comment.Like--;
                }
                db.Comments.Update(comment);
                db.SaveChanges();
            }

            CommonResponse response = new(comment, "Comment like changed");
            return response;
        }

        [HttpPost("comment/dislike")]
        public CommonResponse DislikeComment(int idComment, bool type)
        {
            Comment comment;
            using (ApplicationContext db = new())
            {
                comment = db.Comments
                    .Where(x => x.Id == idComment)
                        .FirstOrDefault();
                if (type)
                {
                    comment.Dislike++;
                }
                else if (comment.Dislike > 0)
                {
                    comment.Dislike--;
                }
                db.Comments.Update(comment);
                db.SaveChanges();
            }

            CommonResponse response = new(comment, "Comment dislike changed");
            return response;
        }
    }
}
