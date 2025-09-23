namespace CoffeeOrder.Promotions;

using System;
using System.Collections.Generic;
using System.Linq;
using CoffeeOrder.Models;
using CoffeeOrder.Pricing;

public record Discount(string Code, decimal Amount, string Description);

public static class PromotionHelper
{
    // Returns final total and list of discounts applied
    public static (decimal totalAfter, List<Discount> discounts) Apply(
        IEnumerable<Beverage> items,
        IEnumerable<string> codes)
    {
        var list = items?.ToList() ?? new List<Beverage>();
        var discounts = new List<Discount>();
        var subtotal = PriceCalculator.CalculateOrderPrice(list);

        // HAPPYHOUR: 20% off all *Hot* drinks
        if (codes?.Any(c => string.Equals(c, "HAPPYHOUR", StringComparison.OrdinalIgnoreCase)) == true)
        {
            var hotTotal = list
                .Where(b => string.Equals(b.Temp, "Hot", StringComparison.OrdinalIgnoreCase))
                .Sum(PriceCalculator.CalculatePrice);

            var amount = Math.Round(hotTotal * 0.20m, 2);
            if (amount > 0)
            {
                discounts.Add(new Discount("HAPPYHOUR", amount, "20% off all hot drinks"));
            }
        }

        var totalAfter = Math.Round(subtotal - discounts.Sum(d => d.Amount), 2);
        return (totalAfter, discounts);
    }
}
