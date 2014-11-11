namespace SpeedHero.Data.Models
{
    public class Picture
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
