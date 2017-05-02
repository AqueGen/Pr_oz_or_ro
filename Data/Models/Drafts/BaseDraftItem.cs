using Kapitalist.Data.Models.Drafts.Interfaces;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Kapitalist.Data.Models.Drafts
{
    /// <summary>
    ///
    /// </summary>
    public abstract class BaseDraftItem : IDraftItem, IEquatable<BaseDraftItem>
    {
        public BaseDraftItem()
        {
        }

        public BaseDraftItem(IDraftItem item)
        {
            StringId = item.StringId;
            Description = item.Description;
            DescriptionEn = item.DescriptionEn;
            Quantity = item.Quantity;
            DeliveryDate = item.DeliveryDate;
            DeliveryAddress = item.DeliveryAddress;
            DeliveryLocation = item.DeliveryLocation;
            Unit = item.Unit;
        }

        /// <summary>
        /// Генерується автоматично.
        /// </summary>
        [Required]
        public string StringId { get; set; }

        /// <summary>
        /// Обов'язково!
        /// OpenContracting Description: Опис товарів та послуг, які повинні бути надані.
        /// </summary>
        [JsonRequired]
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("description_en")]
        public string DescriptionEn { get; set; }

        /// <summary>
        /// OpenContracting Description: Опис одиниці виміру товару, наприклад, години, кілограми.
        /// Складається з назви одиниці та значення однієї одиниці.
        /// </summary>
        [JsonProperty("unit")]
        public Unit Unit { get; set; }

        /// <summary>
        /// OpenContracting Description: Кількість необхідних одиниць.
        /// </summary>
        [JsonProperty("quantity")]
        public long Quantity { get; set; }

        /// <summary>
        /// Період, протягом якого елемент повинен бути доставлений.
        /// </summary>
        [JsonProperty("deliveryDate")]
        public Period DeliveryDate { get; set; }

        /// <summary>
        /// Адреса місця, куди елемент повинен бути доставлений.
        /// deliveryLocation зазвичай має вищий пріоритет ніж deliveryAddress, якщо вони обидва вказані.
        /// </summary>
        [JsonProperty("deliveryAddress")]
        public AddressOptional DeliveryAddress { get; set; }

        /// <summary>
        /// Географічні координати місця доставки.
        /// deliveryLocation зазвичай має вищий пріоритет ніж deliveryAddress, якщо вони обидва вказані.
        /// </summary>
        [JsonProperty("deliveryLocation")]
        public DeliveryLocation DeliveryLocation { get; set; }

        public bool Equals(BaseDraftItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(StringId, other.StringId) && string.Equals(Description, other.Description) &&
                   Equals(Unit, other.Unit) && Quantity == other.Quantity && Equals(DeliveryDate, other.DeliveryDate) &&
                   Equals(DeliveryAddress, other.DeliveryAddress) && Equals(DeliveryLocation, other.DeliveryLocation);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BaseDraftItem)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (StringId != null ? StringId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Unit != null ? Unit.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Quantity.GetHashCode();
                hashCode = (hashCode * 397) ^ (DeliveryDate != null ? DeliveryDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DeliveryAddress != null ? DeliveryAddress.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DeliveryLocation != null ? DeliveryLocation.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}