using Kapitalist.Core.OpenProcurement;
using Kapitalist.Core.OpenProcurement.Exceptions;
using Kapitalist.Core.OpenProcurement.Interfaces;
using Kapitalist.Core.OpenProcurement.Models;
using Kapitalist.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Tests.Kapitalist.Framework;

namespace Tests.Kapitalist.Core.OpenProcurement
{
    [TestClass()]
    public class TenderComplaintsServiceTests : TendersServiceTestsEx<IComplaintsService>
    {
        [ClassInitialize]
        public static void Init(TestContext context)
        {
            Init<TenderComplaintsService>(TendersServiceTests.ValidTender);
        }

        [TestMethod()]
        public async Task CreateComplaintTest()
        {
            var created = await Service.CreateComplaint(ValidComplaint);
        }

        [TestMethod]
        public async Task ChangeComplaint_Error_Token()
        {
            var ex = await AssertEx.ThrowsAsync<APIStatusCodeException>(async () =>
            {
                var created = await Service.CreateComplaint(ValidComplaint);
                var changed = await Service.ChangeComplaint(created.Data.StringId, new { title = "title 2" }, "");
            });
            Assert.AreEqual(ex.StatusCode, System.Net.HttpStatusCode.Forbidden);
        }

        [TestMethod()]
        public async Task ChangeComplaint_Success()
        {
            string title = "Title 3";
            var created = await Service.CreateComplaint(ValidComplaint);
            var changed = await Service.ChangeComplaint(created.Data.StringId, new { title }, created.Token);
            Assert.AreEqual(changed.Title, title);
        }

        [TestMethod()]
        public async Task GetComplaintTest()
        {
            var created = await Service.CreateComplaint(ValidComplaint);
            var complaint = await Service.GetComplaint(created.Data.StringId);
            Assert.IsNotNull(complaint);
        }

        [TestMethod()]
        public async Task GetComplaintsTest()
        {
            var created = await Service.CreateComplaint(ValidComplaint);
            created = await Service.CreateComplaint(ValidComplaint);
            var complaints = await Service.GetComplaints();
            Assert.IsTrue(complaints.Length >= 2);
        }

        private static Complaint _validComplaint;

        public static Complaint ValidComplaint
        {
            get {
                if (_validComplaint == null)
                    _validComplaint = new Complaint
                    {
                        Title = "title",
                        Author = new Organization
                        {
                            Name = "Name",
                            Identifier = new Identifier
                            {
                                Scheme = "UA-EDR",
                                Id = "1"
                            },
                            ContactPoint = new ContactPoint
                            {
                                Name = "Name",
                                Telephone = "0"
                            },
                            Address = new Address
                            {
                                CountryName = "Ukraine"
                            }
                        }
                    };
                return _validComplaint;
            }
        }
    }
}