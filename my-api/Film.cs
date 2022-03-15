namespace my_api
{
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public double Rate { get; set; }

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
