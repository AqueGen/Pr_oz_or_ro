using Kapitalist.Data.Models;

namespace Kapitalist.Web.Client.ViewModels
{
    public class BudgetViewModel
    {
        public string Id { get; set; }

        public int? Year { get; set; }

        public decimal Amount { get; set; }


        public string Currency { get; set; }


        public string Description { get; set; }

        public string Notes { get; set; }

        public BudgetViewModel()
        {
            
        }

        public BudgetViewModel(Budget budget)
        {
            Amount = budget.Amount;
            Currency = budget.Currency;
            Description = budget.Description;
            Id = budget.Id;
            Notes = budget.Notes;      
            Year = budget.Year;
        }

        public Budget ToDTO()
        {
            return new Budget
            {
                Year = Year,
                Amount = Amount,
                Notes = Notes,
                Description = Description,
                Currency = Currency,
                Id = Id
            };
        }
    }

    public class ProjectViewModel
    {
        public ProjectViewModel()
        {
        }

        public ProjectViewModel(Project project)
        {
        }

        public Project ToDTO()
        {
            return new Project();
        }
    }
}