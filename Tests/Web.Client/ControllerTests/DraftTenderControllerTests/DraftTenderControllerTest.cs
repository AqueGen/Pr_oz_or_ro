using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using NUnit.Framework;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Data.Store;
using Kapitalist.Web.Client.Controllers;
using Kapitalist.Web.Client.ViewModels;
using Kapitalist.Web.Security;
using Kapitalist.Services.Prozorro.Mappers;
using Kapitalist.Web.Client.Controllers.Drafts;
using Kapitalist.Web.Client.ViewModels.Drafts;
using Kapitalist.Web.Client.ViewModels.Published;

namespace Tests.Kapitalist.Web.Client.ControllerTests.DraftTenderControllerTests
{
    [TestFixture]
    public class DraftTenderTest : BaseDraftTenderControllerTest
    {
        private StoreContext _context;
        private Lazy<IAccessManager> _accessManager;
        private DraftProvider _provider;
        private DraftTenderBelowThresholdController _controller;

        [SetUp]
        public void Init()
        {
            _context = new StoreContext();
            _accessManager = new Lazy<IAccessManager>(() => new AccessManagerForTest(_context));
            _provider = new DraftProvider(_context, _accessManager.Value);
            _controller = new DraftTenderBelowThresholdController(_provider, _accessManager);
        }

        [TearDown]
        public void Dispose()
        {
            _context.Dispose();
        }

        [Test]
        public async Task CreateDraftTenderTest()
        {
            var viewModel = new DraftTenderBelowThresholdViewModel
            {
                Description = "test desc",
                Guid = Guid.NewGuid(),
                Title = "test title",
                Value = new ValueViewModel
                {
                    Currency = "UAH",
                    Amount = 15.4M,
                    VATIncluded = true
                },
                EnquiryPeriod = new PeriodViewModel
                {
                    StartDate = DateTime.Now.Add(new TimeSpan(1, 0, 0, 0)),
                    EndDate = DateTime.Now.Add(new TimeSpan(2, 0, 0, 0)),
                },
                TenderPeriod = new PeriodViewModel
                {
                    StartDate = DateTime.Now.Add(new TimeSpan(3, 0, 0, 0)),
                    EndDate = DateTime.Now.Add(new TimeSpan(4, 0, 0, 0)),
                }
            };

            var tenderDTO = viewModel.ToDTO();
            var tender = tenderDTO.ToDraft();

            await _controller.AddTender(viewModel);

            var draftTenderEntity = await _context.DraftTenders.FirstOrDefaultAsync(m => m.Guid == viewModel.Guid);

            Assert.That(tender.Guid, Is.EqualTo(draftTenderEntity.Guid));
            Assert.That(tender.Title, Is.EqualTo(draftTenderEntity.Title));
            Assert.That(tender.TitleEn, Is.EqualTo(draftTenderEntity.TitleEn));
            Assert.That(tender.Description, Is.EqualTo(draftTenderEntity.Description));
            Assert.That(tender.Value.Equals(draftTenderEntity.Value));
            Assert.That(tender.Guarantee.Equals(draftTenderEntity.Guarantee));
            Assert.That(tender.MinimalStep.Equals(draftTenderEntity.MinimalStep));
            Assert.That(tender.EnquiryPeriod.Equals(draftTenderEntity.EnquiryPeriod));
            Assert.That(tender.TenderPeriod.Equals(draftTenderEntity.TenderPeriod));

            _context.DraftTenders.Remove(draftTenderEntity);
            await _context.SaveChangesAsync();
        }


        [Test]
        public async Task CreateDraftItem()
        {
            var viewModel = new DraftTenderBelowThresholdViewModel
            {
                Description = "test desc",
                Guid = Guid.NewGuid(),
                Title = "test title",
                Value = new ValueViewModel
                {
                    Currency = "UAH",
                    Amount = 15.4M,
                    VATIncluded = true
                },
                EnquiryPeriod = new PeriodViewModel
                {
                    StartDate = DateTime.Now.Add(new TimeSpan(1, 0, 0, 0)),
                    EndDate = DateTime.Now.Add(new TimeSpan(2, 0, 0, 0)),
                },
                TenderPeriod = new PeriodViewModel
                {
                    StartDate = DateTime.Now.Add(new TimeSpan(3, 0, 0, 0)),
                    EndDate = DateTime.Now.Add(new TimeSpan(4, 0, 0, 0)),
                }
            };

            var tenderDTO = viewModel.ToDTO();
            var tender = tenderDTO.ToDraft();

            await _controller.AddTender(viewModel);


            var itemViewModel = new ItemViewModel
            {
                StringId = Guid.NewGuid().ToString("N"),
                Description = "item desc",
                TenderGuid = tender.Guid,
                Classification = new ClassificationViewModel {Id = "classification id", Description = "some desc"},
                LotStringId = null,
                AdditionalClassifications =
                    new List<ClassificationViewModel>
                    {
                        new ClassificationViewModel
                        {
                            Id = "classification id",
                            Description = "some desc",
                            Scheme = "ДКПП",
                            Uri = "http://yandex.ru"
                        }
                    },
                DeliveryLocation = new DeliveryLocationViewModel
                {
                    Longitude = "154",
                    Elevation = "234",
                    Latitude = "345"
                },
                Quantity = 150,
                DeliveryAddress = new AddressViewModel
                {
                    Locality = "sumy",
                    Region = "sumska",
                    PostalCode = "40024",
                    Country = "Ukraine",
                    Street = "SKD"
                },
                DeliveryDate = new PeriodViewModel {
                    StartDate = DateTime.Now.Add(new TimeSpan(1, 0, 0, 0)),
                    EndDate = DateTime.Now.Add(new TimeSpan(2, 0, 0, 0))
                },
                Unit = new UnitViewModel
                {
                    Name = "some unit name",
                    Code = "some unit code"
                }
            };

            //await _controller.AddItem(itemViewModel);

            var itemDTO = itemViewModel.ToDTO();
            var item = itemDTO.ToDraft();

            var draftItemEntity = await _context.DraftItems
                .FirstOrDefaultAsync(m => m.Tender.Guid == viewModel.Guid && m.StringId == itemViewModel.StringId);


            Assert.That(item.StringId, Is.EqualTo(draftItemEntity.StringId));
            Assert.That(item.Description, Is.EqualTo(draftItemEntity.Description));
            Assert.That(itemViewModel.TenderGuid == draftItemEntity.Tender.Guid);
            Assert.That(item.Classification, Is.EqualTo(draftItemEntity.Classification));
            Assert.That(item.LotId, Is.EqualTo(draftItemEntity.LotId));
            Assert.That(item.AdditionalClassifications, Is.EqualTo(draftItemEntity.AdditionalClassifications));
            Assert.That(item.DeliveryLocation, Is.EqualTo(draftItemEntity.DeliveryLocation));
            Assert.That(item.Quantity, Is.EqualTo(draftItemEntity.Quantity));
            Assert.That(item.DeliveryAddress, Is.EqualTo(draftItemEntity.DeliveryAddress));
            Assert.That(item.DeliveryDate, Is.EqualTo(draftItemEntity.DeliveryDate));
            Assert.That(item.Unit, Is.EqualTo(draftItemEntity.Unit));

            _context.DraftTenders.Remove(draftItemEntity.Tender);
            await _context.SaveChangesAsync();
        }
    }
}