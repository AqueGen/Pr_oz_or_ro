using Kapitalist.Core.OpenProcurement;
using Kapitalist.Core.OpenProcurement.Exceptions;
using Kapitalist.Core.OpenProcurement.Interfaces;
using Kapitalist.Core.OpenProcurement.Models;
using Kapitalist.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Consts;
using Kapitalist.Data.Models.Enums;
using Tests.Kapitalist.Framework;

namespace Tests.Kapitalist.Core.OpenProcurement
{
    [TestClass]
    public class TendersServiceTests : DocumentsServiceTests<ITendersService>
    {
        public TendersServiceTests()
            : base(new TendersService())
        {
        }

        [TestMethod]
        public async Task CreateTender_Error()
        {
            var ex = await AssertEx.ThrowsAsync<APIErrorsException>(async () =>
            {
                var created = await Service.CreateTender(new Tender
                {
                });
            });
        }

        [TestMethod]
        public async Task CreateTender_Success()
        {
            var t = await Service.CreateTender(ValidTender);
        }

        /// <summary>
        /// Тест виявлення обов'язкових полів для чернетки закупівлі.
        /// Якщо колись ЦБД зменшить вимоги до чернетки - можливо можна буде обійтись без наших внутрішніх чернеток, а відразу відправляти в ЦБД.
        /// </summary>
        [TestMethod]
        public async Task CreateDraftTender_Requirements()
        {
            Tender t = ValidTender;
            t.Status = TenderStatus.DRAFT;
            var v = await Service.CreateTender(t);

            var ex = await AssertEx.ThrowsAsync<APIErrorsException>(async () =>
            {
                await Service.CreateTender(new Tender());
            });
            Assert.AreEqual(ex.Errors.Length, 7);   
        }

        [TestMethod]
        public async Task ChangeTender_Error_Token()
        {
            var ex = await AssertEx.ThrowsAsync<APIStatusCodeException>(async () =>
            {
                var created = await Service.CreateTender(ValidTender);
                await Service.ChangeTender(created.Data.Guid, new { title = "Title 2" }, "");
            });
            Assert.AreEqual(ex.StatusCode, System.Net.HttpStatusCode.Forbidden);
        }

        [TestMethod]
        public async Task ChangeTender_Success()
        {
            string title = "Title 3";
            var created = await Service.CreateTender(ValidTender);
            var changed = await Service.ChangeTender(created.Data.Guid, new { title } , created.Token);
            Assert.AreEqual(changed.Title, title);
        }

        [TestMethod]
        public async Task ChangeFeatures_Error_Token()
        {
            var ex = await AssertEx.ThrowsAsync<APIStatusCodeException>(async () =>
            {
                var created = await Service.CreateTender(ValidTender);
                await Service.ChangeFeatures(created.Data.Guid, ValidFeatures, "");
            });
            Assert.AreEqual(ex.StatusCode, System.Net.HttpStatusCode.Forbidden);
        }

        [TestMethod]
        public async Task ChangeFeatures_Success()
        {
            var created = await Service.CreateTender(ValidTender);
            var changed = await Service.ChangeFeatures(created.Data.Guid, ValidFeatures, created.Token);
            Assert.IsTrue(changed.Features.Length > 0);
        }

        [TestMethod]
        public async Task DeleteFeatures_Error_Token()
        {
            var ex = await AssertEx.ThrowsAsync<APIStatusCodeException>(async () =>
            {
                var created = await Service.CreateTender(ValidTender);
                await Service.DeleteFeatures(created.Data.Guid, "");
            });
            Assert.AreEqual(ex.StatusCode, System.Net.HttpStatusCode.Forbidden);
        }

        [TestMethod]
        public async Task DeleteFeatures_Success()
        {
            var created = await Service.CreateTender(ValidTender);
            await Service.ChangeFeatures(created.Data.Guid, ValidFeatures, created.Token);
            var woFeatures = await Service.DeleteFeatures(created.Data.Guid, created.Token);
            Assert.IsNull(woFeatures.Features);
        }

        //// This test runs about 5 min, and checks parsing each available tenders
        //[TestMethod]
        //public async Task GetAllTenders()
        //{
        //    ModificationsPage page = new ModificationsPage
        //    {
        //        NextPage = new NextPage { Offset = DateTime.MinValue }
        //    };
        //    Tender tender;
        //    do
        //    {
        //        page = await _service.GetModificationsPage(page.NextPage.Offset);
        //        foreach (var t in page.Items)
        //        {
        //            tender = await _service.GetTender(t.Guid);
        //            if (tender.Revisions != null)
        //            {
        //                // TODO Taras 5: Implement Change model when find any info about it.
        //                throw new NotImplementedException(
        //                    $"Tender {t.Guid} contains Revision, but Revision.Change is not implemented yet!");
        //            }
        //        }
        //    } while (page.Items.Length > 0);
        //}

        [TestMethod]
        public async Task GetDocuments()
        {
            var documents = await Service.GetDocuments(await GetFirstTenderId());
        }

        [TestMethod]
        public async Task GetTender()
        {
            var tender = await Service.GetTender(await GetFirstTenderId());
        }

        [TestMethod]
        public async Task GetTendersPage()
        {
            var page = await Service.GetModificationsPage();
        }

        private async Task<Guid> GetFirstTenderId()
        {
            return (await Service.GetModificationsPage(null, false, 1)).Items.First().Guid;
        }

        private static Tender _validDraft;
        public static Tender ValidDraft
        {
            get {
                if (_validDraft == null)
                {
                    _validDraft = new Tender
                    {
                        Status = TenderStatus.DRAFT,    
                        Title = "Title",
                        TenderPeriod = new Period {},
                        EnquiryPeriod = new EnquiryPeriod { },
                        Value = new Value { },
                        MinimalStep = new Value { },
                        ProcuringEntity = new ProcuringEntity{},
                        Items = new Item[] { }
                    };
                }
                return _validDraft;
            }
        }

        private static Tender _validTender;
        public static Tender ValidTender
        {
            get {
                if (_validTender == null)
                {
                    DateTime now = DateTime.UtcNow;
                    _validTender = new Tender
                    {
                        Title = "Title",
                        Value = new Value
                        {
                            Amount = 10,
                            Currency = "UAH"
                        },
                        MinimalStep = new Value
                        {
                            Amount = 1,
                            Currency = "UAH"
                        },
                        EnquiryPeriod = new EnquiryPeriod
                        {
                            StartDate = now,
                            EndDate = now.AddMinutes(1)
                        },
                        TenderPeriod = new Period
                        {
                            StartDate = now.AddMinutes(1),
                            EndDate = now.AddMinutes(10)
                        },
                        ProcuringEntity = new ProcuringEntity
                        {
                            Name = "Name",
                            Kind = ProcuringEntityType.General,
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
                        },
                        Items = new Item[]
                        {
                        new Item
                        {
                            Description = "Description",
                            Classification = new Classification
                            {
                                Scheme = "CPV",
                                Id = "19212310-1",
                                Description = "Description"
                            },
                            AdditionalClassifications = new Classification[]
                            {
                                new Classification
                                {
                                    Scheme = "NONE",
                                    Id = "",
                                    Description = "Description"
                                }
                            }
                        }
                        }
                    };
                }
                return _validTender;
            }
        }

        private static Feature[] _validFeatures;
        public static Feature[] ValidFeatures
        {
            get {
                if (_validFeatures == null)
                    _validFeatures = new Feature[]
                    {
                        new Feature
                        {
                            Title = "Title",
                            Values = new FeatureValue[]
                            {
                                new FeatureValue
                                {
                                    Title = "Title"
                                }
                            }
                        }
                    };
                return _validFeatures;
            }
        }
    }
}