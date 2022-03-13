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
        private List<User> users;
        private List<Comment> comments;
        public FilmController()
        {
            users = new List<User>();
            users.Add(new User(1, "Егор", "https://cs6.pikabu.ru/avatars/2097/x2097369-1271064885.png"));
            users.Add(new User(2, "Гена Гиена", "https://mobimg.b-cdn.net/v3/fetch/4a/4ab4b2a2f3984e85c6b8325f972f65dc.jpeg"));
            users.Add(new User(3, "Диппер-Разрушитель", "https://avatars.mds.yandex.net/get-zen_doc/1639101/pub_611768146eab3f04defe25d7_6117687f7e37175eb6759ed8/scale_1200"));
            users.Add(new User(4, "Катя", "https://img1.goodfon.ru/original/320x240/6/ea/lisa-ryzhaia-morda-vzgliad-portret.jpg"));

            comments = new List<Comment>();
            comments.Add(new Comment(1, 1, "ЕГООООР", 1, 10, 2));
            comments.Add(new Comment(2, 1, "Какой ужасный фильм", 2, 1, 12));
            comments.Add(new Comment(3, 1, "Бекон", 3, 25, 4));
            comments.Add(new Comment(4, 1, "Я катя", 4, 25, 0));

            films = new List<Film>();
            films.Add(new Film(1, "Начало", "Профессиональные воры внедряются в сон наследника огромной империи. Фантастический боевик Кристофера Нолана", "https://avatars.mds.yandex.net/get-kinopoisk-image/1600647/a4a709fc-8dd9-41f4-8105-17d6e0b8bed0/2560x", 8.7));
            films.Add(new Film(2, "Довод", "Протагонист пытается обезвредить террориста с помощью уникальной технологии. Блокбастер-пазл Кристофера Нолана", "https://games.mail.ru/hotbox/content_files/gallery/2020/08/12/766acda8f2a347baa1a130a7a40d4d77.jpeg", 7.8));
            films.Add(new Film(3, "Интерстеллар", "Фантастический эпос про задыхающуюся Землю, космические полеты и парадоксы времени. «Оскар» за спецэффекты", "https://avatars.mds.yandex.net/get-zen_doc/3866587/pub_5f78ac3a61e6d41ef54175dd_5f78cef7952c3b370e175229/scale_1200", 8.6));
            films.Add(new Film(4, "Зеленая миля", "В тюрьме для смертников появляется заключенный с божественным даром. Мистическая драма по роману Стивена Кинга", "https://artfiles.alphacoders.com/992/thumb-1920-99225.jpg", 9.1));
            films.Add(new Film(5, "Властелин колец: Возвращение короля", "Арагорн штурмует Мордор, а Фродо устал бороться с чарами кольца. Эффектный финал саги, собравший 11 «Оскаров»", "https://www.film.ru/sites/default/files/movies/posters/The-Lord-of-the-Rings-The-Return-of-the-King-3.jpg", 8.6));
            films.Add(new Film(6, "Темный рыцарь", "У Бэтмена появляется новый враг — философ-террорист Джокер. Кинокомикс, который вывел жанр на новый уровень", "https://mir-s3-cdn-cf.behance.net/project_modules/max_1200/ad7b6a28200049.55bdb46e8f33f.jpg", 8.5));
            films.Add(new Film(7, "Рататуй", "Крысенок-кулинар попадает на кухню ресторана. Аппетитная комедия с «Оскаром» за лучший анимационный фильм", "https://avatars.mds.yandex.net/get-kinopoisk-image/1900788/daed0206-3a64-4397-a6a9-12f33d7ab092/orig", 8.0));
            films.Add(new Film(8, "Шрэк 2", "Счастью новобрачных огров мешают Фея-крестная и ее сын. Номинация на «Оскар» и первое появление Кота в сапогах", "https://www.kinogallery.com/images/shrek2/kinogallery.com-shrek2-265160.jpg", 7.7));
            films.Add(new Film(9, "Крестный отец 2 ", "Юность Вито Корлеоне и первые шаги его сына Майкла в роли главы клана — сразу и приквел, и сиквел. Шесть «Оскаров»", "https://www.yekmovie.tv/wp-content/uploads/2018/03/tt0071562_big.jpg", 8.5));
            films.Add(new Film(10, "Один дома 2: Затерянный в Нью-Йорке", "Неугомонный малыш Кевин в одиночку покоряет Нью-Йорк. Очередной шедевр от Джона Хьюза и Криса Коламбуса", "https://www.film.ru/sites/default/files/movies/posters/Home-Alone-2-Lost-in-New-York-3.jpg", 8.2));
        }


        [HttpGet("list")]
        public CommonResponse GetFilmList(int page = 1, int count = 10)
        {
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
            List<Comment> filmComments = comments.FindAll(x => x.FilmId == id);
            foreach (Comment comment in filmComments)
            {
                comment.User = users.Find(x => x.Id==comment.UserId);
            }
            Film film = films.Find(x => x.Id == id);
            film.Comments = filmComments;
            CommonResponse response = new CommonResponse(film);
            return response;
        }
    }
}
