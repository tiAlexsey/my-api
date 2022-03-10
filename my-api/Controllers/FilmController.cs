using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace my_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("_myAllowSpecificOrigins")]
    public class FilmController : ControllerBase
    {
        private List<Film> films;
        public FilmController()
        {
            films = new List<Film>();
            films.Add(new Film(0, "Начало", "Профессиональные воры внедряются в сон наследника огромной империи. Фантастический боевик Кристофера Нолана", "https://avatars.mds.yandex.net/get-kinopoisk-image/1600647/a4a709fc-8dd9-41f4-8105-17d6e0b8bed0/2560x"));
            films.Add(new Film(1, "Довод", "Протагонист пытается обезвредить террориста с помощью уникальной технологии. Блокбастер-пазл Кристофера Нолана", "https://games.mail.ru/hotbox/content_files/gallery/2020/08/12/766acda8f2a347baa1a130a7a40d4d77.jpeg"));
            films.Add(new Film(2, "Интерстеллар", "Фантастический эпос про задыхающуюся Землю, космические полеты и парадоксы времени. «Оскар» за спецэффекты", "https://avatars.mds.yandex.net/get-zen_doc/3866587/pub_5f78ac3a61e6d41ef54175dd_5f78cef7952c3b370e175229/scale_1200"));
        }

        [HttpGet("list")]
        public IEnumerable<Film> GetFilmList()
        {
            return films;
        }

        [HttpGet("item/{id:int}")]
        public Film GetFilm(int id)
        {
            return films[id];
        }
    }
}
