using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kapitalist.Data.Models.DTO;

namespace Kapitalist.Services.Prozorro.Providers
{
    public interface IProfileProvider
    {
        Task<OrganizationDTO> GetUserOrganization(int id);
        Task AddContact(int userOrganizationId, ContactPointDTO contactDTO);
        Task<ContactPointDTO> GetContact(int userOrganizationId, int contactId);
        Task EditContact(int userOrganizationId, ContactPointDTO contactDTO);
        Task<IEnumerable<ContactPointDTO>> GetContacts(int userOrganizationId);
        Task<IEnumerable<ContactPointDTO>> GetContacts(int userOrganizationId, Guid tenderGuid);
        Task DeleteContact(int userOrganizationId, int contactId);

    }
}