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

        // normalize codes for easy lookup (case-insensitive)
        var codeSet = codes is null
            ? new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            : new HashSet<string>(codes, StringComparer.OrdinalIgnoreCase);

        // HAPPYHOUR: 20% off *Hot* drinks only
        if (codeSet.Contains("HAPPYHOUR"))
        {
            var hotTotal = list
                .Where(b => string.Equals(b.Temp, "Hot", StringComparison.OrdinalIgnoreCase))
                .Sum(PriceCalculator.CalculatePrice);

            var amount = Math.Round(hotTotal * 0.20m, 2);
            if (amount > 0)
                discounts.Add(new Discount("HAPPYHOUR", amount, "20% off all hot drinks"));
        }

        // BOGO: once per order — cheapest item free (needs at least 2 items)
        if (codeSet.Contains("BOGO") && list.Count >= 2)
        {
            var cheapest = list.Min(PriceCalculator.CalculatePrice);
            var amount = Math.Round(cheapest, 2);
            if (amount > 0)
                discounts.Add(new Discount("BOGO", amount, "Buy one get one (cheapest free, once)"));
        }

        var totalAfter = Math.Round(subtotal - discounts.Sum(d => d.Amount), 2);
        if (totalAfter < 0) totalAfter = 0m; // belt & suspenders

        return (totalAfter, discounts);
    }
}
