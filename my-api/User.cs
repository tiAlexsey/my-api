namespace my_api
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        
        public User(int id, string name, string url)
        {
            Id = id;
            Name = name;
            Url = url;
        }
    }
}
