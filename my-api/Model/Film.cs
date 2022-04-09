using System.ComponentModel.DataAnnotations.Schema;

namespace my_api.Model
{
    public class Film
    {
        [Column("Id")] public int Id { get; set; }
        [Column("sName")] public string Name { get; set; }
        [Column("sDescription")] public string Description { get; set; }
        [Column("sUrl")] public string Url { get; set; }
        [Column("fRate")] public double Rate { get; set; }

        public Film(int id, string name, string description, string url, double rate)
        {
            Id = id;
            Name = name;
            Description = description;
            Url = url;
            Rate = rate;
        }
    }
}