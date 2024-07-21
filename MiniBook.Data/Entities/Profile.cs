namespace MiniBook.Data.Entities
{
    public class Profile
    {
        public Profile()
        {

        }
        public Profile(string name, string gender, string image)
        {
            Name = name;
            Gender = gender;
            Image = image;
        }

        public string Name { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
    }
}
