using MiniBook.Services;
using Xunit;

namespace xUnitTestProject
{
    public class AccountServiceTest
    {
        [Fact]
        public async void Register()
        {
            var accountService = new AccountService(new HttpService());

            var result = await accountService.RegisterAsync(new MiniBook.Models.User()
            {
                FirstName = "Khanh",
                LastName = "Rua",
                Email = "ruaaa@gmail.com",
                Gender = "male",
                DoB = "2001-05-13",
            }, "String@123");

            //Assert.True(result.Successful);
        }

        [Fact]
        public async void Login()
        {
            var accountService = new AccountService(new HttpService());

            var result = await accountService.LoginAsync("khanh@gmail.com","String@123");

            //Assert.True(result.Successful);
        }

    }
}
