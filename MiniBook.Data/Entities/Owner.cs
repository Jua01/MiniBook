namespace MiniBook.Data.Entities
{
    public class Owner : Profile
    {
        public Owner()
        {

        }
        public string Id { get; set; }

        public Owner(string userId, Profile profile)
        {
            Id = userId;
            Name = profile.Name;
            Image = profile.Image;
            Gender = profile.Gender;
        }


    }
}
