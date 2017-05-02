using System;
using System.Collections.Generic;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Store;
using Kapitalist.Services.Prozorro.Mappers;
using Kapitalist.Web.Security;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Store.Models;

namespace Kapitalist.Services.Prozorro.Providers
{
    public class ProfileProvider : BaseProvider, IProfileProvider
    {
        public ProfileProvider(StoreContext context, IAccessManager accessManager)
            : base(context, accessManager)
        {
        }

        public async Task<OrganizationDTO> GetUserOrganization(int id)
        {
            var organization = await Context.UserOrganizations
                .Include(o => o.ContactPoints)
                .FirstOrDefaultAsync(m => m.Id == id);
            return organization.ToDTO();
        }

        public async Task AddContact(int userOrganizationId, ContactPointDTO contactDTO)
        {
            var contact = new ContactPoint(contactDTO);
            var userOrganizationContactPoint = new UserOrganizationContactPoint(contact)
            {
                OrganizationId = userOrganizationId
            };
            Context.UserOrganizationContactPoints.Add(userOrganizationContactPoint);
            await Context.SaveChangesAsync();
        }

        public async Task<ContactPointDTO> GetContact(int userOrganizationId, int contactId)
        {
            var contact = await Context.UserOrganizationContactPoints
                .FirstOrDefaultAsync(m => m.Id == contactId && m.OrganizationId == userOrganizationId);
            return contact.ToDTO();
        }

        public async Task EditContact(int userOrganizationId, ContactPointDTO contactDTO)
        {
            var savedItem = await Context.UserOrganizationContactPoints
                .FirstOrDefaultAsync(m => m.Id == contactDTO.Id && m.OrganizationId == userOrganizationId);
            Context.Entry(savedItem).CurrentValues.SetValues(contactDTO);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ContactPointDTO>> GetContacts(int userOrganizationId)
        {
            var contacts = await Context.UserOrganizationContactPoints
                .Where(m => m.OrganizationId == userOrganizationId).ToListAsync();
            return contacts.Select(m => m.ToDTO());
        }

        public async Task<IEnumerable<ContactPointDTO>> GetContacts(int userOrganizationId, Guid tenderGuid)
        {
            var tender = await Context.DraftTenders.FirstOrDefaultAsync(m => m.Guid == tenderGuid);
            var tenderContactsId = tender.ContactPointRefs.Select(m => m.ContactPointId);
            var contacts = await Context.UserOrganizationContactPoints
                .Where(m => m.OrganizationId == userOrganizationId).ToListAsync();
            return contacts.Where(m => !tenderContactsId.Contains(m.Id)).Select(m => m.ToDTO());
        }

        public async Task DeleteContact(int userOrganizationId, int contactId)
        {
            var contact = await Context.UserOrganizationContactPoints
                .FirstOrDefaultAsync(m => m.Id == contactId && m.OrganizationId == userOrganizationId);
            Context.UserOrganizationContactPoints.Remove(contact);
            Context.SaveChangesAsync();
        }
    }
}