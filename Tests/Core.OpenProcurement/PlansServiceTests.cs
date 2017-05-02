using System;
using System.Linq;
using System.Threading.Tasks;
using Kapitalist.Core.OpenProcurement;
using Kapitalist.Core.OpenProcurement.Exceptions;
using Kapitalist.Core.OpenProcurement.Interfaces;
using Kapitalist.Core.OpenProcurement.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Kapitalist.Framework;

namespace Tests.Kapitalist.Core.OpenProcurement
{
    [TestClass()]
    public class PlansServiceTests : ServiceTests<IPlansService>
    {
        public PlansServiceTests()
            : base(new PlansService())
        {
        }

        [TestMethod]
        public async Task GetPlansPage()
        {
            var page = await Service.GetModificationsPage();
        }

        [TestMethod]
        public async Task GetPlan()
        {
            var plan = await Service.GetPlan(await GetFirstPlanId());
        }

        private async Task<Guid> GetFirstPlanId()
        {
            return (await Service.GetModificationsPage(null, false, 1)).Items.First().Guid;
        }

        //// This test runs about 5 min, and checks parsing each available tenders
        //[TestMethod]
        //public async Task GetAllPlans()
        //{
        //    ModificationsPage page = new ModificationsPage
        //    {
        //        NextPage = new NextPage { Offset = DateTime.MinValue }
        //    };
        //    Plan plan;
        //    do
        //    {
        //        page = await _service.GetModificationsPage(page.NextPage.Offset);
        //        foreach (var p in page.Items)
        //        {
        //            plan = await _service.GetPlan(p.Guid);
        //        }
        //    } while (page.Items.Length > 0);
        //}

        [TestMethod()]
        public async Task CreatePlan()
        {
            var plan = await Service.CreatePlan(ValidPlan);
        }

        [TestMethod]
        public async Task ChangePlan_Error_Token()
        {
            var ex = await AssertEx.ThrowsAsync<APIStatusCodeException>(async () =>
            {
                var created = await Service.CreatePlan(ValidPlan);
                await Service.ChangePlan(created.Data.Guid, new { budget = new { description = "descr." } }, "");
            });
            Assert.AreEqual(ex.StatusCode, System.Net.HttpStatusCode.Forbidden);
        }

        [TestMethod]
        public async Task ChangePlan_Success()
        {
            string description = "description";
            var created = await Service.CreatePlan(ValidPlan);
            var changed = await Service.ChangePlan(created.Data.Guid, new { budget = new { description } }, created.Token);
            Assert.AreEqual(changed.Budget.Description, description);
        }

        private static Plan _validPlan;

        public static Plan ValidPlan
        {
            get {
                if (_validPlan == null)
                    _validPlan = new Plan
                    {
                        Classification = new Classification
                        {
                            Scheme = "CPV",
                            Id = "03111000-2",
                            Description = "seeds"
                        },
                        Budget = new global::Kapitalist.Data.Models.Budget
                        {
                            Year = 2015,
                            Amount = 1000,
                            Currency = "UAH",
                            Id = "1234567890",
                            Description = "",
                            Notes = "hello"
                        },
                        ProcuringEntity = new PlanProcuringEntity
                        {
                            Name = "nema",
                            Identifier = new Identifier
                            {
                                Scheme = "UA-EDR",
                                Id = "223344",
                                LegalName = "test name"
                            }
                        },
                        Tender = new global::Kapitalist.Data.Models.PlanTender
                        {
                            ProcurementMethod = "open",
                            ProcurementMethodType = "belowThreshold",
                            TenderPeriod = new global::Kapitalist.Data.Models.Period
                            {
                                StartDate = DateTime.UtcNow
                            }
                        }
                    };
                return _validPlan;
            }
        }
    }
}