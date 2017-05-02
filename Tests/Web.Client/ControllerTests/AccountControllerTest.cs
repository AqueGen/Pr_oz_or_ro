using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Enums;
using Kapitalist.Web.Client.Controllers;
using Kapitalist.Web.Client.ViewModels;
using Kapitalist.Web.Client.ViewModels.Account;
using Kapitalist.Web.Client.ViewModels.Profile;
using NUnit.Framework;
using Kapitalist.Web.Framework.Enums;

namespace Tests.Kapitalist.Web.Client.ControllerTests
{
    [TestFixture]
    public class AccountControllerTest
    {
        public AccountControllerTest()
        {

        }

        [Test]
        public async Task SuccessfulRegistration()
        {
            var controller = new AccountController();

            var viewModel = new RegisterViewModel
            {
                Kind = ProcuringEntityType.Defense,
                Address = new AddressViewModelRequired
                {
                    Country = "Ukraine",
                    Locality = "Sumy",
                    PostalCode = "0542",
                    Region = "Sumska",
                    Street = "SKD"
                },
                Company = new IdentifierViewModel()
                {
                    Id = "Identifier Id",
                    Uri = "http://yandex.ru",
                    Scheme = "Identifier scheme",
                    LegalName = "Identifier legalname"
                },
                CompanyType = CompanyType.Corporation,
                CaptchaSuccess = true,
                ContactPoint = new ContactPointViewModel
                {
                    Url = "http://yandex.ru",
                    Name = "Petya",
                    Email = "a@yandex.ru",
                    FaxNumber = "2345678",
                    Telephone = "1234567"
                },
                Email = "q@yandex.ru",
                Password = "123456aA!",
                ConfirmPassword = "123456aA!",
                Phone = "123457"
            };
        }

    }
}
