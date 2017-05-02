using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kapitalist.Core.OpenProcurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Core.OpenProcurement.Interfaces;
using Kapitalist.Core.OpenProcurement.Models;
using Kapitalist.Data.Models;
using Kapitalist.Core.OpenProcurement.Exceptions;
using Tests.Kapitalist.Framework;

namespace Tests.Kapitalist.Core.OpenProcurement
{
    [TestClass()]
    public class LotsServiceTests : TendersServiceTestsEx<ILotsService>
    {
        [ClassInitialize]
        public static void Init(TestContext context)
        {
            Init<LotsService>(TendersServiceTests.ValidTender);
        }

        [TestMethod]
        public async Task CreateLot_Error_Token()
        {
            var ex = await AssertEx.ThrowsAsync<APIStatusCodeException>(async () =>
            {
                var created = await Service.CreateLot(ValidLot, "");
            });
            Assert.AreEqual(ex.StatusCode, System.Net.HttpStatusCode.Forbidden);
        }

        [TestMethod()]
        public async Task CreateLot_Success()
        {
            var created = await Service.CreateLot(ValidLot, Token);
        }

        [TestMethod]
        public async Task ChangeLot_Error_Token()
        {
            var ex = await AssertEx.ThrowsAsync<APIStatusCodeException>(async () =>
            {
                var created = await Service.CreateLot(ValidLot, Token);
                var changed = await Service.ChangeLot(created.StringId, new { title = "Title 2" }, "");
            });
            Assert.AreEqual(ex.StatusCode, System.Net.HttpStatusCode.Forbidden);
        }

        [TestMethod()]
        public async Task ChangeLot_Success()
        {
            string title = "Title 3";
            var created = await Service.CreateLot(ValidLot, Token);
            var changed = await Service.ChangeLot(created.StringId, new { title }, Token);
            Assert.AreEqual(changed.Title, title);         
        }

        [TestMethod]
        public async Task AssignLots_Error_Token()
        {
            var ex = await AssertEx.ThrowsAsync<APIStatusCodeException>(async () =>
            {
                var dict = new Dictionary<Item, Lot>();
                dict.Add(Tender.Items[0], await Service.CreateLot(ValidLot, Token));
                var tender = await Service.AssignLots(dict, "");
            });
            Assert.AreEqual(ex.StatusCode, System.Net.HttpStatusCode.Forbidden);
        }

        [TestMethod()]
        public async Task AssignLots_Success()
        {
            var dict = new Dictionary<Item, Lot>();
            await Service.CreateLot(ValidLot, Token);
            dict.Add(Tender.Items[0], await Service.CreateLot(ValidLot, Token));
            var tender = await Service.AssignLots(dict, Token);
            Assert.IsNotNull(tender.Items[0].RelatedLot);
        }

        [TestMethod]
        public async Task DeleteLot_Error_Token()
        {
            var ex = await AssertEx.ThrowsAsync<APIStatusCodeException>(async () =>
            {
                var created = await Service.CreateLot(ValidLot, Token);
                var deleted = await Service.DeleteLot(created.StringId, "");
            });
            Assert.AreEqual(ex.StatusCode, System.Net.HttpStatusCode.Forbidden);
        }

        [TestMethod()]
        public async Task DeleteLot_Success()
        {
            var created = await Service.CreateLot(ValidLot, Token);
            var deleted = await Service.DeleteLot(created.StringId, Token);
        }

        [TestMethod()]
        public async Task GetLot()
        {
            var created = await Service.CreateLot(ValidLot, Token);
            var lot = await Service.GetLot(created.StringId);
            Assert.IsNotNull(lot);
        }

        [TestMethod()]
        public async Task GetLots()
        {
            var created = await Service.CreateLot(ValidLot, Token);
            created = await Service.CreateLot(ValidLot, Token);
            var lots = await Service.GetLots();
            Assert.IsTrue(lots.Length >= 2);
        }

        private static Lot _validLot;
        public static Lot ValidLot
        {
            get {
                if (_validLot == null)
                    _validLot = new Lot
                    {
                        Title = "title",
                        Value = new Value
                        {

                        },
                        MinimalStep = new Value
                        {

                        }
                    };
                return _validLot;
            }
        }
    }
}