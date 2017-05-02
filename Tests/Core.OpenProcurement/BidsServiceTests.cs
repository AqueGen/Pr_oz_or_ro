using Kapitalist.Core.OpenProcurement;
using Kapitalist.Core.OpenProcurement.Exceptions;
using Kapitalist.Core.OpenProcurement.Interfaces;
using Kapitalist.Core.OpenProcurement.Models;
using Kapitalist.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Consts;
using Tests.Kapitalist.Framework;

namespace Tests.Kapitalist.Core.OpenProcurement
{
    [TestClass]
    public class BidsServiceTests : TendersServiceTestsEx<IBidsService>
    {
        [ClassInitialize]
        public static void Init(TestContext context)
        {
            Init<BidsService>(TendersServiceTests.ValidTender);

            // wait for tendering period
            using (TendersService service = new TendersService())
            {
                while (!string.Equals(Tender.Status, TenderStatus.ACTIVE_TENDERING, StringComparison.OrdinalIgnoreCase))
                {
                    Thread.Sleep(TimeSpan.FromSeconds(30));
                    Tender = service.GetTender(Tender.Guid).Result;
                }
            }
        }

        [TestMethod()]
        public async Task CreateBid()
        {
            var bid = await Service.CreateBid(ValidBid);
        }

        [TestMethod]
        public async Task ChangeBid_Error_Token()
        {
            var ex = await AssertEx.ThrowsAsync<APIStatusCodeException>(async () =>
            {
                var created = await Service.CreateBid(ValidBid);
                var changed = await Service.ChangeBid(created.Data.StringId, new { value = new { amount = 1 } }, "");
            });
            Assert.AreEqual(ex.StatusCode, System.Net.HttpStatusCode.Forbidden);
        }

        [TestMethod]
        public async Task ChangeBid_Success()
        {
            decimal amount = 1;
            var created = await Service.CreateBid(ValidBid);
            var changed = await Service.ChangeBid(created.Data.StringId, new { value = new { amount } }, created.Token);
            Assert.AreEqual(changed.Value.Amount, amount);
        }

        [TestMethod]
        public async Task DeleteBid_Error_Token()
        {
            var ex = await AssertEx.ThrowsAsync<APIStatusCodeException>(async () =>
            {
                var created = await Service.CreateBid(ValidBid);
                var deleted = await Service.DeleteBid(created.Data.StringId, "");
            });
            Assert.AreEqual(ex.StatusCode, System.Net.HttpStatusCode.Forbidden);
        }

        [TestMethod]
        public async Task DeleteBid_Success()
        {
            var created = await Service.CreateBid(ValidBid);
            var deleted = await Service.DeleteBid(created.Data.StringId, created.Token);
        }

        //[TestMethod()]
        //public async Task GetBid()
        //{
        //    var created = await Service.CreateBid(ValidBid);
        //    var bid = await Service.GetBid(created.Data.StringId);
        //    Assert.IsNotNull(bid);
        //}

        //[TestMethod()]
        //public async Task GetBids()
        //{
        //    var created = await Service.CreateBid(ValidBid);
        //    created = await Service.CreateBid(ValidBid);
        //    var bids = await Service.GetBids();
        //    Assert.IsTrue(bids.Length >= 2);
        //}

        private static Bid _validBid;

        public static Bid ValidBid
        {
            get {
                if (_validBid == null)
                    _validBid = new Bid
                    {
                        Value = new Value
                        {
                        },
                        Tenderers = new Organization[]
                        {
                            new Organization
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
                        }
                    };
                return _validBid;
            }
        }
    }
}