using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using my_api.Entities;

namespace my_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("_myAllowSpecificOrigins")]
    public class FilmController : ControllerBase
    {
        public FilmController()
        {
        }

        [HttpGet("list")]
        public CommonResponse GetFilmList(int page = 1, int count = 10)
        {
            List<Film> films;
            using (ApplicationContext db = new ApplicationContext())
            {
                films = db.Films.ToList();
            }
            int iBegin = page * count - count;
            int iEnd = page * count;
            iEnd = (iEnd>films.Count) ? films.Count : iEnd;
            List<Film> filmPage = new List<Film>(page);
            CommonResponse response;
            try
            {
                for (int i = iBegin; i < iEnd; i++)
                {
                    filmPage.Add(films[i]);
                }
                response = new CommonResponse(filmPage, films.Count);
            }
            catch (Exception e)
            {
                response = new CommonResponse(films, films.Count);
            }
            return response;
        }

        [HttpGet("item/{id:int}")]
        public CommonResponse GetFilm(int id)
        {
            List<NewComment> comments;
            List<Comment> commentsWithUser;

            using (ApplicationContext db = new ApplicationContext())
            {
                comments = db.Comments
                    .ToList()
                    .FindAll(x => x.FilmId == id)
                    .ToList();
            }

            commentsWithUser = Comment.convertToComment(comments);
            foreach (Comment c in commentsWithUser)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    c.User = db.Users
                         .Where(x => x.Id==c.UserId)
                         .FirstOrDefault();
                }
            }

            Film film;
            using (ApplicationContext db = new ApplicationContext())
            {
                film = db.Films
                    .Where(x => x.Id==id)
                    .FirstOrDefault();
            }

            FilmPage filmPage = new FilmPage();
            filmPage.Film=film;
            filmPage.Comments=commentsWithUser;
            CommonResponse response = new CommonResponse(filmPage);
            return response;
        }


        [HttpGet("list/search")]
        public CommonResponse SeachFilmByName(string name)
        {
            List<Film> films = new List<Film>();
            using (ApplicationContext db = new ApplicationContext())
            {
                films = db.Films
                    .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                    .ToList();
            }

            CommonResponse response = new CommonResponse(films);
            return response;
        }

        [HttpPost("comment/add")]
        public CommonResponse AddComment(NewComment newComment)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Comments.Add(newComment);
                Console.WriteLine(db.Comments.Add(newComment));
                db.SaveChanges();
            }

            CommonResponse response = new CommonResponse(newComment);
            return response;
        }
    }
}
