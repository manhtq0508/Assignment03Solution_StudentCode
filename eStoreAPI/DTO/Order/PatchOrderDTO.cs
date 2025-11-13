using System.ComponentModel.DataAnnotations;

namespace eStoreAPI.DTO.Order;

public class PatchOrderDTO : IValidatableObject
{
    public DateTime? RequiredDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public decimal? Freight { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (RequiredDate == null && ShippedDate == null && Freight == null)
        {
            yield return new ValidationResult(
                "At least one field (RequiredDate, ShippedDate, Freight) must be provided for update.",
                new[] { nameof(RequiredDate), nameof(ShippedDate), nameof(Freight) });
        }

        if (RequiredDate != null && RequiredDate < DateTime.Now.Date)
        {
            yield return new ValidationResult(
                "RequiredDate cannot be in the past.",
                new[] { nameof(RequiredDate) });
        }

        if (ShippedDate != null && ShippedDate < DateTime.Now.Date)
        {
            yield return new ValidationResult(
                "ShippedDate cannot be in the past.",
                new[] { nameof(ShippedDate) });
        }

        if (Freight != null && Freight < 0)
        {
            yield return new ValidationResult(
                "Freight must be a non-negative value.",
                new[] { nameof(Freight) });
        }
    }
}
