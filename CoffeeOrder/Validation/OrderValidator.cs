using CoffeeOrder.Models;

namespace CoffeeOrder.Validation;

public static class OrderValidator
{
    public static ValidationResult Validate(Beverage beverage)
    {
        var errors = new List<string>();

        if (beverage is null)
        {
            return ValidationResult.Fail("Beverage must not be null.");
        }

        // Required fields
        if (string.IsNullOrWhiteSpace(beverage.BaseDrink))
            errors.Add("A base drink is required.");
        if (string.IsNullOrWhiteSpace(beverage.Size))
            errors.Add("A size selection is required.");
        if (string.IsNullOrWhiteSpace(beverage.Temp))
            errors.Add("Temperature (Hot/Iced) must be selected.");

        // Milk XOR rule: cannot select both dairy and plant milk
        if (!string.IsNullOrWhiteSpace(beverage.Milk) && !string.IsNullOrWhiteSpace(beverage.PlantMilk))
            errors.Add("Milk selection invalid: choose dairy OR plant milk, not both.");

        // Limits
        if (beverage.Shots < 0 || beverage.Shots > 4)
            errors.Add("Shots must be between 0 and 4 inclusive.");

        if (beverage.Syrups is null || beverage.Syrups.Length > 5 || beverage.Syrups.Any(s => s is null))
            errors.Add("Syrups must contain 0..5 non-null entries.");

        // Note: Additional rules (allergen warnings, kid-safe classification) are out of scope for the validator.

        return errors.Count == 0 ? ValidationResult.Ok() : ValidationResult.Fail(errors.ToArray());
    }
}