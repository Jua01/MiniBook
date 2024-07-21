using MiniBook.Data.Context;
using MiniBook.Data.Repositories;

namespace MiniBook.Data.Test
{
    public class PostRepositoryTest
    {
        [Fact]
        public async void GetPost()
        {
           var repo = GetRespository();
           var ruesult = await repo.GetWallFeed("b1415472-ceb4-49b5-a563-a796911d1196", 0);

        }

        
        [Fact]
        public async void CreatePost()
        {
            var repo = GetRespository();
            await repo.CreateAsync("b1415472-ceb4-49b5-a563-a796911d1196", "so beautiful");
        }

        [Fact]
        public async void CommentPost()
        {
            var repo = GetRespository();
            await repo.CommentAsync("b6a4dfc2-b6a0-49cd-ae60-0b5b1d47fa23", "66913ffd095248a05698ba32", "Like");
        }
        public static ResourceDbContext GetDbContext()
        {
            return new ResourceDbContext("mongodb://localhost:27017", "MiniBook");
        }
        public static PostRepository GetRespository()
        {
            return new PostRepository(GetDbContext());
        }
    }
    public class UserRepositoryTest
    {
        [Fact]
        public async void Follow()
        {
            var repo = GetRespository();
            await repo.FollowAsync("b6a4dfc2-b6a0-49cd-ae60-0b5b1d47fa23", "b1415472-ceb4-49b5-a563-a796911d1196");
        }

        public static ResourceDbContext GetDbContext()
        {
            return new ResourceDbContext("mongodb://localhost:27017", "MiniBook");
        }
        public static UserRepository GetRespository()
        {
            return new UserRepository(GetDbContext());
        }
    }
}