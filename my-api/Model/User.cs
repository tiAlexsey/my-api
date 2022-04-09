using System.ComponentModel.DataAnnotations.Schema;

namespace my_api.Model
{
    public class User
    {
        [Column("Id")] public int Id { get; set; }
        [Column("sName")] public string Name { get; set; }
        [Column("sUrl")] public string Url { get; set; }

        public User(int id, string name, string url)
        {
            Id = id;
            Name = name;
            Url = url;
        }
    }
}