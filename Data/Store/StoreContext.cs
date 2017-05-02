using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Attributes;
using Kapitalist.Data.Models.Interfaces;
using Kapitalist.Data.Store.Models;
using Kapitalist.Services.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Kapitalist.Data.Store
{
    public class StoreContext : DbContext
    {
        //static StoreContext()
        //{
        //    Database.SetInitializer(new DropCreateDatabaseAlways<StoreContext>());
        //}
        public StoreContext()
            : base("StoreContext")
        {
            ((IObjectContextAdapter)this).ObjectContext.ObjectMaterialized += ObjectMaterialized;
        }

        public ITrace Trace { get; set; }

        public DbSet<SyncState> SyncStates { get; set; }

        public DbSet<SyncError> SyncErrors { get; set; }

        public DbSet<UserOrganization> UserOrganizations { get; set; }

        public DbSet<UserOrganizationIdentifier> UserOrganizationIdentifiers { get; set; }

        public DbSet<UserOrganizationContactPoint> UserOrganizationContactPoints { get; set; }

        public DbSet<CreatedTender> CreatedTenders { get; set; }

        public DbSet<CreatedBid> CreatedBids { get; set; }

        public DbSet<CreatedAward> CreatedAward { get; set; }

        public DbSet<CreatedTenderComplaint> CreatedTenderComplaint { get; set; }

        public DbSet<CreatedAwardComplaint> CreatedAwardComplaint { get; set; }

        public DbSet<CreatedContract> CreatedContract { get; set; }

        public DbSet<DraftTender> DraftTenders { get; set; }

        public DbSet<DraftLot> DraftLots { get; set; }

        public DbSet<DraftItem> DraftItems { get; set; }

        public DbSet<DraftFeature> DraftFeatures { get; set; }

        public DbSet<DraftFeatureValue> DraftFeatureValues { get; set; }

        public DbSet<DraftClassification> DraftClassifications { get; set; }

        public DbSet<DraftTenderDocument> DraftTenderDocuments { get; set; }

        public DbSet<Tender> Tenders { get; set; }

        public DbSet<Lot> Lots { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Feature> Features { get; set; }

        public DbSet<FeatureValue> FeatureValues { get; set; }

        public DbSet<StandardUnit> Units { get; set; }

        public DbSet<Classification> Classifications { get; set; }

        public DbSet<ClassificationScheme> ClassificationSchemes { get; set; }

        public DbSet<ClassificationCPV> ClassificationsCPV { get; set; }

        public DbSet<ClassificationGSIN> ClassificationsGSIN { get; set; }

        public DbSet<TenderDocument> TenderDocuments { get; set; }

        public DbSet<ProcuringEntity> ProcuringEntities { get; set; }

        public DbSet<ProcuringEntityIdentifier> ProcuringEntityIdentifiers { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<QuestionAuthor> QuestionAuthors { get; set; }

        public DbSet<QuestionAuthorIdentifier> QuestionAuthorIdentifiers { get; set; }

        public DbSet<TenderComplaint> TenderComplaints { get; set; }

        public DbSet<TenderComplaintAuthor> TenderComplaintAuthors { get; set; }

        public DbSet<TenderComplaintAuthorIdentifier> TenderComplaintAuthorIdentifiers { get; set; }

        public DbSet<TenderComplaintDocument> TenderComplaintDocuments { get; set; }

        public DbSet<Bid> Bids { get; set; }

        public DbSet<Tenderer> Tenderers { get; set; }

        public DbSet<BidDocument> BidDocuments { get; set; }

        public DbSet<Parameter> Parameters { get; set; }

        public DbSet<LotValue> LotValues { get; set; }

        public DbSet<Award> Awards { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<SupplierIdentifier> SupplierIdentifiers { get; set; }

        public DbSet<AwardDocument> AwardDocuments { get; set; }

        public DbSet<AwardComplaint> AwardComplaints { get; set; }

        public DbSet<AwardComplaintAuthor> AwardComplaintAuthors { get; set; }

        public DbSet<AwardComplaintAuthorIdentifier> AwardComplaintAuthorIdentifiers { get; set; }

        public DbSet<AwardComplaintDocument> AwardComplaintDocuments { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<ContractSupplier> ContractSuppliers { get; set; }

        public DbSet<ContractSupplierIdentifier> ContractSupplierIdentifiers { get; set; }

        public DbSet<ContractDocument> ContractDocuments { get; set; }

        public DbSet<Cancellation> Cancellations { get; set; }

        public DbSet<CancellationDocument> CancellationDocuments { get; set; }

        public DbSet<Revision> Revisions { get; set; }

        public DbSet<Change> Changes { get; set; }

        public DbSet<Plan> Plans { get; set; }

        public DbSet<PlanClassification> PlanClassifications { get; set; }

        public DbSet<PlanProcuringEntity> PlanProcuringEntities { get; set; }

        public DbSet<PlanProcuringEntityIdentifier> PlanProcuringEntityIdentifiers { get; set; }

        public DbSet<PlanItem> PlanItems { get; set; }

        public DbSet<PlanItemClassification> PlanItemClassifications { get; set; }

        public DbSet<DraftPlan> DraftPlans { get; set; }

        public DbSet<DraftPlanClassification> DraftPlanClassifications { get; set; }

        public DbSet<DraftPlanItem> DraftPlanItems { get; set; }

        public DbSet<DraftPlanItemClassification> DraftPlanItemClassifications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
            modelBuilder.Types<IGuid>().Configure(x => x.Property(p => p.Guid)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute())));
            modelBuilder.Types<IStringId>().Configure(x => x.Property(p => p.StringId).IsRequired().HasMaxLength(64)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute())));
            modelBuilder.Types<Address>().Configure(x => x.Property(a => a.CountryName).IsRequired());
            modelBuilder.Types<ContactPoint>().Configure(x => x.Property(c => c.Name).IsRequired());
            var tender = modelBuilder.Entity<Tender>();
            tender.Property(p => p.Guid).HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
            tender.Property(t => t.GuaranteeOptional.Amount).HasColumnName("Guarantee_Amount");
            tender.Property(t => t.GuaranteeOptional.Currency).HasColumnName("Guarantee_Currency");
            tender.HasRequired(t => t.ProcuringEntity).WithRequiredPrincipal(o => o.Tender).WillCascadeOnDelete(true);
            tender.HasMany(t => t.Contracts).WithRequired(c => c.Tender).WillCascadeOnDelete(false);
            var lot = modelBuilder.Entity<Lot>();
            lot.Property(l => l.GuaranteeOptional.Amount).HasColumnName("Guarantee_Amount");
            lot.Property(l => l.GuaranteeOptional.Currency).HasColumnName("Guarantee_Currency");
            var item = modelBuilder.Entity<Item>();
            item.Property(i => i.Classification.Id).HasColumnName("CPV_Id");
            item.Property(i => i.Classification.Description).HasColumnName("CPV_Description");
            var question = modelBuilder.Entity<Question>();
            question.HasRequired(q => q.Author).WithRequiredPrincipal(o => o.Question).WillCascadeOnDelete(true);
            modelBuilder.Entity<TenderComplaint>().HasRequired(c => c.Author).WithRequiredPrincipal(o => o.Complaint)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<AwardComplaint>().HasRequired(c => c.Author).WithRequiredPrincipal(o => o.Complaint)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<CreatedTender>().HasRequired(c => c.Tender).WithOptional();
            modelBuilder.Entity<CreatedBid>().HasRequired(c => c.Bid).WithOptional();
            modelBuilder.Entity<CreatedAward>().HasRequired(c => c.Award).WithOptional();
            modelBuilder.Entity<CreatedTenderComplaint>().HasRequired(c => c.TenderComplaint).WithOptional();
            modelBuilder.Entity<CreatedAwardComplaint>().HasRequired(c => c.AwardComplaint).WithOptional();
            modelBuilder.Entity<CreatedContract>().HasRequired(c => c.Contract).WithOptional();
            var plan = modelBuilder.Entity<Plan>();
            plan.Property(p => p.Guid).HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
            plan.HasRequired(p => p.ProcuringEntity).WithRequiredPrincipal(o => o.Plan).WillCascadeOnDelete(true);
            plan.Property(i => i.Classification.Id).HasColumnName("CPV_Id");
            plan.Property(i => i.Classification.Description).HasColumnName("CPV_Description");
            var planItem = modelBuilder.Entity<PlanItem>();
            planItem.Property(i => i.Classification.Id).HasColumnName("CPV_Id");
            planItem.Property(i => i.Classification.Description).HasColumnName("CPV_Description");
            var ppe = modelBuilder.Entity<PlanProcuringEntity>();
            ppe.Ignore(o => o.Address);
            ppe.Property(o => o.AddressOptional.StreetAddress).HasColumnName("Address_StreetAddress");
            ppe.Property(o => o.AddressOptional.Locality).HasColumnName("Address_Locality");
            ppe.Property(o => o.AddressOptional.Region).HasColumnName("Address_Region");
            ppe.Property(o => o.AddressOptional.PostalCode).HasColumnName("Address_PostalCode");
            ppe.Property(o => o.AddressOptional.CountryName).HasColumnName("Address_CountryName");
            ppe.Ignore(o => o.ContactPoint);
            ppe.Property(l => l.ContactPointOptional.Name).HasColumnName("ContactPoint_Name");
            ppe.Property(l => l.ContactPointOptional.Email).HasColumnName("ContactPoint_Email");
            ppe.Property(l => l.ContactPointOptional.Telephone).HasColumnName("ContactPoint_Telephone");
            ppe.Property(l => l.ContactPointOptional.FaxNumber).HasColumnName("ContactPoint_FaxNumber");
            ppe.Property(l => l.ContactPointOptional.Url).HasColumnName("ContactPoint_Url");
            var draftPlan = modelBuilder.Entity<DraftPlan>();
            draftPlan.Property(p => p.Guid).HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
            draftPlan.Property(i => i.Classification.Id).HasColumnName("CPV_Id");
            draftPlan.Property(i => i.Classification.Description).HasColumnName("CPV_Description");
            var draftPlanItem = modelBuilder.Entity<DraftPlanItem>();
            draftPlanItem.Property(i => i.Classification.Id).HasColumnName("CPV_Id");
            draftPlanItem.Property(i => i.Classification.Description).HasColumnName("CPV_Description");
            modelBuilder.Entity<DraftTenderContactPoint>().HasRequired(c => c.ContactPoint)
                .WithMany().WillCascadeOnDelete(false);
        }

        private void ObjectMaterialized(object sender, System.Data.Entity.Core.Objects.ObjectMaterializedEventArgs e)
        {
            object entity = e.Entity;
            if (entity == null)
                return;

            //Find all properties that are of type DateTime or DateTime? and fix it's kind to Utc;
            var properties = entity.GetType().GetProperties()
                .Where(x => x.PropertyType == typeof(DateTime)
                         || x.PropertyType == typeof(DateTime?));

            foreach (var property in properties)
            {
                var dt = property.PropertyType == typeof(DateTime?)
                    ? (DateTime?)property.GetValue(entity)
                    : (DateTime)property.GetValue(entity);

                if (dt == null)
                    continue;

                //If the value is not null set the appropriate DateTimeKind;
                property.SetValue(entity, dt.Value.AsUtc());
            }
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            DbEntityValidationResult result = base.ValidateEntity(entityEntry, items);
            if (!result.IsValid)
            {
                try
                {
                    foreach (DbValidationError error in result.ValidationErrors)
                    {
                        var prop = entityEntry.Property(error.PropertyName);
                        string text = prop.CurrentValue as string;

                        // truncate all long strings marked by TruncateAttribute
                        if (text != null)
                        {
                            object entity = entityEntry.Entity;
                            Type entityType = entity.GetType();
                            PropertyInfo pi = entityType.GetProperty(error.PropertyName);
                            if (pi == null)
                            {
                                Type valueType = entityType;
                                foreach (string subName in error.PropertyName.Split('.'))
                                {
                                    pi = valueType.GetProperty(subName);
                                    valueType = pi.PropertyType;
                                }
                            }
                            if (pi.GetCustomAttribute<TruncateAttribute>() != null)
                            {
                                int maxLength = 4000; // nvarchar(MAX)
                                MaxLengthAttribute mla;
                                StringLengthAttribute sla;
                                if ((sla = pi.GetCustomAttribute<StringLengthAttribute>()) != null)
                                {
                                    maxLength = sla.MaximumLength;
                                }
                                else if ((mla = pi.GetCustomAttribute<MaxLengthAttribute>()) != null)
                                {
                                    maxLength = mla.Length;
                                }
                                if (text.Length > maxLength)
                                {
                                    Trace.TraceWarning("{0}.{1} = \"{2}\" will be truncated to {3} chars.",
                                        entityType.Name.Split('_')[0], error.PropertyName, text, maxLength);
                                    prop.CurrentValue = text.Truncate(maxLength);
                                    continue;
                                }
                            }
                        }
                        Trace.TraceWarning("{0}.{1} validation error: {2}",
                                        entityEntry.Entity.GetType().Name.Split('_')[0],
                                        error.PropertyName,
                                        error.ErrorMessage);
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError("Entity property cannot be truncated automaticly. " + ex);
                }
                return base.ValidateEntity(entityEntry, items);
            }
            return result;
        }

        public override async Task<int> SaveChangesAsync()
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
                .ToList();

            foreach (var changedEntity in changedEntities)
            {
                var lot = changedEntity.Entity as DraftLot;
                if (lot != null)
                {
                    var tender = lot.Tender ?? this.DraftTenders.First(l => l.Id == lot.TenderId);
                    lot.LotChangeSubscriber = tender.OnLotChange;
                    lot.LotChangeSubscriber(lot, changedEntity.State);
                }
            }

            return await base.SaveChangesAsync();
        }
    }
}