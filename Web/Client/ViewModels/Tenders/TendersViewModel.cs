using System.Linq;
using Kapitalist.Data.Models.DTO;
using PagedList;

namespace Kapitalist.Web.Client.ViewModels.Tenders
{
    public class TendersViewModel
    {
        public TenderQueryViewModel Query;
        public IPagedList<TenderDTO> Tenders;

        public TendersViewModel(IPagedList<TenderDTO> tenders, TenderQueryViewModel query)
        {
            Query = query;
            Tenders = tenders;
        }
    }
}