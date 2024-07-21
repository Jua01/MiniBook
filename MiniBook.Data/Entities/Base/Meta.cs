namespace MiniBook.Data.Entities.Base
{
    public class Meta
    {
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime? Delete { get; set; }
        public Meta()
        {
            Updated = Created = DateTime.UtcNow;
        }
    }
}
