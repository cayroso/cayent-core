using Cayent.Core.Data.Components.Products;
using Cayent.Core.Common.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cayent.Core.Data.Components.Promotions
{
    public enum EnumPromotionType
    {
        Unknown = 0,
        Discounts,
        Shipping,
    }

    public enum EnumPromotionProductApplicability
    {
        Unknown = 0,
        AllProducts = 1,
        SpecificProducts = 2
    }
    public enum EnumPromotionOfferType
    {
        Unknown = 0,
        NoCode,
        GenericCode
    }
    internal abstract class PromotionBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PromotionId { get; set; }

        public EnumPromotionProductApplicability Applicability { get; set; }
        public EnumPromotionOfferType OfferType { get; set; }
        public string GenericRedemptionCode { get; set; }

        public EnumPromotionType PromotionType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        DateTime _effectiveStartDate = DateTime.MaxValue;
        public DateTime EffectiveStartDate
        {
            get => _effectiveStartDate.AsUtc();
            set => _effectiveStartDate = value.Truncate();
        }

        DateTime _effectiveEndDate = DateTime.MaxValue;
        public DateTime EffectiveEndDate
        {
            get => _effectiveEndDate.AsUtc();
            set => _effectiveEndDate = value.Truncate();
        }

        DateTime _displayStartDate = DateTime.MaxValue;
        /// <summary>
        /// This attribute specifies the start date and time frame when the promotion will be live 
        /// </summary>
        public DateTime DisplayStartDate
        {
            get => _displayStartDate.AsUtc();
            set => _displayStartDate = value.Truncate();
        }

        DateTime _displayEndDate = DateTime.MaxValue;
        /// <summary>
        /// This attribute specifies the end date and time frame when the promotion will be live 
        /// </summary>
        public DateTime DisplayEndDate
        {
            get => _displayEndDate.AsUtc();
            set => _displayEndDate = value.Truncate();
        }

        #region PreConditions

        /// <summary>
        /// This attribute sets the minimum purchase quantity required for the promotion to be redeemed.
        /// This attribute can be used in combination with the percent_off, get_this_quantity_discounted, or money_off_amount attributes.
        /// For example, for X number of purchases of the same product or a combination of products, the user will receive promotion Y. 
        /// For example, set this attribute to 2 for a “Buy 2, get 20% off” promotion. 
        /// </summary>
        public double MinimumPurchaseQuantity { get; set; }
        /// <summary>
        /// This attribute sets the minimum purchase amount for the promotion to be redeemed.
        /// For example, for X amount of purchases of the same product or a combination of products, the user will receive promotion Y. 
        /// </summary>
        public double MinimumPurchaseAmount { get; set; }

        /// <summary>
        /// Set this attribute for promotions that require a membership or store credit card.
        /// FREE_MEMBERSHIP_REQUIRED, PAID_MEMBERSHIP_REQUIRED, STORE_CREDIT_CARD_REQUIRED
        /// </summary>
        public string MembershipType { get; set; }
        #endregion

        #region Categories

        /// <summary>
        /// The minimum purchase quantity required to redeem promotions offering "Buy M, get N." This field represents the "M" value for the offer.
        /// </summary>
        public uint BuyThisQuantity { get; set; }
        /// <summary>
        /// The percent discounted for precentage-based promotions (e.g.10 % off).
        /// </summary>
        public double PercentOff { get; set; }
        /// <summary>
        /// The monetary value discounted for fixed discount promotions (e.g. $200 off)
        /// </summary>
        public double MoneyOffAmount { get; set; }
        /// <summary>
        /// The quantity discounted once the minimum quantity required to purchase has been met in "Buy M, get N discounted" promotions. This field represents the "N" value of the offer.
        /// </summary>
        public uint GetThisQuantityDiscounted { get; set; }

        /// <summary>
        /// For promotions offering free shipping, this attribute indicates the type of free shipping offered. 
        /// FREE_SHIPPING_STANDARD, FREE_SHIPPING_OVERNIGHT, FREE_SHIPPING_TWO_DAY
        /// </summary>
        public string FreeShipping { get; set; }
        /// <summary>
        /// The monetary value of any free gift offered in the promotion.
        /// </summary>
        public double FreeGiftValue { get; set; }
        /// <summary>
        /// The description of any free gift offered in the promotion.
        /// </summary>
        public string FreeGiftDescription { get; set; }
        /// <summary>
        /// The item/product id of any free gift offered in the promotion.
        /// </summary>
        public string FreeGiftProductId { get; set; }
        public virtual ProductBase FreeGiftProduct { get; set; }

        #endregion

        #region Limits

        /// <summary>
        /// Maximum number of items that can be bought using this promotion
        /// </summary>
        public uint LimitQuantity { get; set; }
        /// <summary>
        /// Maximum item value allowed for this promotion
        /// </summary>
        public double LimitValue { get; set; }

        #endregion

        public bool Active { get; set; } = true;
        public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();

        //public virtual ICollection<PromotionProductFilterBase> PromotionFilters { get; set; } = new List<PromotionProductFilterBase>();
    }

    internal static class PromotionExtension
    {
        public static void ThrowIfNull(this PromotionBase me)
        {
            if (me == null)
                throw new ApplicationException("Promotion not found.");
        }

        public static void ThrowIfNullOrAlreadyUpdated(this PromotionBase me, string currentToken, string newToken)
        {
            me.ThrowIfNull();

            if (string.IsNullOrWhiteSpace(newToken))
                throw new ApplicationException("Anti-forgery token not found.");

            if (me.ConcurrencyToken != currentToken)
                throw new ApplicationException("Promotion already updated by another user.");

            me.ConcurrencyToken = newToken;
        }
    }

    internal class PromotionBaseConfiguration : EntityBaseConfiguration<PromotionBase>
    {
        public override void Configure(EntityTypeBuilder<PromotionBase> b)
        {
            b.ToTable("Promotion");
            b.HasKey(e => e.PromotionId);

            b.Property(e => e.PromotionId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.Title).HasMaxLength(NameMaxLength).IsRequired();

            b.HasQueryFilter(e => e.Active);
        }
    }
}
