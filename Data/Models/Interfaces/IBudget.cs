namespace Kapitalist.Data.Models.Interfaces
{
    public interface IBudget
    {
        string Id { get; set; }

        int? Year { get; set; }

        /// <summary>
		/// Обов'язково!
		/// OpenContracting Description: Кількість як число.
		/// Повинно бути додатнім.
		/// </summary>
        decimal AmountNet { get; set; }

        /// <summary>
		/// Обов'язково!
		/// OpenContracting Description: Кількість як число.
		/// Повинно бути додатнім.
		/// </summary>
        decimal Amount { get; set; }

        /// <summary>
        /// Обов'язково!
        /// OpenContracting Description: Валюта у трибуквенному форматі ISO 4217.
        /// За замовчуванням = UAH
        /// </summary>
        string Currency { get; set; }

        /// <summary>
		/// Детальний опис буджету плану.
		/// </summary>
        string Description { get; set; }

        string Notes { get; set; }

        Project Project { get; set; }
    }
}