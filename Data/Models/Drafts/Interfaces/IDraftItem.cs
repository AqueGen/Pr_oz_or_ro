using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Models.Drafts.Interfaces
{
    public interface IDraftItem : IStringId
    {
        /// <summary>
        /// Обов'язково!
        /// OpenContracting Description: Опис товарів та послуг, які повинні бути надані.
        /// </summary>
        string Description { get; set; }

        string DescriptionEn { get; set; }

        /// <summary>
        /// OpenContracting Description: Опис одиниці виміру товару, наприклад, години, кілограми.
        /// Складається з назви одиниці та значення однієї одиниці.
        /// </summary>
        Unit Unit { get; set; }

        /// <summary>
        /// OpenContracting Description: Кількість необхідних одиниць.
        /// </summary>
        long Quantity { get; set; }

        /// <summary>
        /// Період, протягом якого елемент повинен бути доставлений.
        /// </summary>
        Period DeliveryDate { get; set; }

        /// <summary>
        /// Адреса місця, куди елемент повинен бути доставлений.
        /// deliveryLocation зазвичай має вищий пріоритет ніж deliveryAddress, якщо вони обидва вказані.
        /// </summary>
        AddressOptional DeliveryAddress { get; set; }

        /// <summary>
        /// Географічні координати місця доставки.
        /// deliveryLocation зазвичай має вищий пріоритет ніж deliveryAddress, якщо вони обидва вказані.
        /// </summary>
        DeliveryLocation DeliveryLocation { get; set; }
    }
}