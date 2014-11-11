namespace SpeedHero.Data.Models
{
    public class TextPart
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int SerailNumber { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
