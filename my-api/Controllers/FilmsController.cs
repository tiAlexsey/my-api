using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using my_api.Entities;
using Microsoft.EntityFrameworkCore;
using my_api.Model;
using my_api.MyLibrary;

namespace my_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("_myAllowSpecificOrigins")]
#nullable disable warnings
    public class FilmsController : ControllerBase
    {
        /// <summary>
        /// Количество фильмов
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public CommonResponse GetFilmsCount()
        {
            int countFilms;
            using (ApplicationContext db = new())
            {
                countFilms = db.Films
                    .Count();
            }

            return new CommonResponse(countFilms);
        }
        /// <summary>
        /// Список фильмов
        /// </summary>
        /// <param name="page">Страница</param>
        /// <param name="count">Количество отображаемых элеметов на странице</param>
        /// <returns></returns>
        [HttpGet("list")]
        public CommonResponse GetFilmList(int page = 1, int count = 10)
        {
            int skipedCount = page * count - count;

            List<Film> films;
            using (ApplicationContext db = new())
            {
                films = db.Films
                    .Skip(skipedCount)
                    .Take(count)
                    .ToList();
            }

            return new CommonResponse(films, films.Count);
        }
        /// <summary>
        /// Фильм
        /// </summary>
        /// <param name="id">Идентификатор фильма</param>
        /// <returns></returns>
        [HttpGet("item/{id:int}")]
        public CommonResponse GetFilm(int id)
        {
            Film film;
            List<Comment> comments;
            
            using (ApplicationContext db = new())
            {
                try
                {
                    film = db.Films
                        .Where(x => x.Id == id)
                        .Single();
                }
                catch (InvalidOperationException)
                {
                    return new CommonResponse("Film not found");
                }
            }
            
            using (ApplicationContext db = new())
            {
                comments = db.Comments
                    .Where(c => c.FilmId == id)
                    .Include(u => u.User)
                    .ToList();
            }

            return new CommonResponse(new FilmPage(film, comments));
        }
        /// <summary>
        /// Поиск фильма
        /// </summary>
        /// <param name="name">Название фильма</param>
        /// <returns></returns>
        [HttpGet("search")]
        public CommonResponse SearchFilmByName(string name)
        {
            List<Film> films = new();
            using (ApplicationContext db = new())
            {
                films = db.Films
                    .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                    .ToList();
            }

            return new CommonResponse(films);
        }
        /// <summary>
        /// Добавить комментарий к фильму
        /// </summary>
        /// <param name="newComment">Новый комментарий</param>
        /// <returns></returns>
        [HttpPost("comment/add")]
        public CommonResponse AddComment(NewComment newComment)
        {
            Comment comment = new(newComment.FilmId, newComment.Text, newComment.UserId);

            using (ApplicationContext db = new())
            {
                db.Comments.Add(comment);
                db.SaveChanges();
            }

            return new CommonResponse(comment, "Comment added");
        }
        /// <summary>
        /// Поставить\снять лайк с комментария
        /// </summary>
        /// <param name="idComment">Ид комметария</param>
        /// <param name="type">Поставить\снять</param>
        /// <returns></returns>
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

            return new CommonResponse(comment, "Comment like changed");
        }
        /// <summary>
        /// Добавить или убрать дизлайк с комментария
        /// </summary>
        /// <param name="idComment">Ид комметария</param>
        /// <param name="type">Добавить=True, снять=False</param>
        /// <returns></returns>
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

            return new CommonResponse(comment, "Comment dislike changed");
        }
    }
}