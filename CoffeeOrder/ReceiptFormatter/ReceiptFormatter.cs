using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using CoffeeOrder.Models;
using CoffeeOrder.Pricing;
using CoffeeOrder.Promotions;
using System.Globalization;

namespace CoffeeOrder.Receipt;

public static class ReceiptFormatter
{
    public static string Format(
        IEnumerable<Beverage> items,
        IEnumerable<string> promoCodes,
        DateTime now,
        string author)
    {
        var list = items?.ToList() ?? new List<Beverage>();
        var codes = promoCodes ?? Array.Empty<string>();
        var sb = new StringBuilder();

        // HEADER — MM/dd/yyyy HH:mm 
        sb.AppendLine($"Receipt - {author} - {now.ToString("MM'/'dd'/'yyyy HH:mm", CultureInfo.InvariantCulture)}");
        sb.AppendLine(new string('-', 40));

        // ITEMS
        foreach (var item in list)
        {
            var price = PriceCalculator.CalculatePrice(item);
            sb.AppendLine($"{item.BaseDrink} ({item.Size} {item.Temp})".PadRight(30) + $"{price:0.00}".PadLeft(10));
        }

        // SUBTOTAL
        var subtotal = PriceCalculator.CalculateOrderPrice(list);
        sb.AppendLine(new string('-', 40));
        sb.AppendLine($"Subtotal:".PadRight(30) + $"{subtotal:0.00}".PadLeft(10));

        // PROMOS
        var (totalAfter, discounts) = PromotionHelper.Apply(list, codes);
        foreach (var d in discounts)
        {
            sb.AppendLine($"{d.Code} Discount:".PadRight(30) + $"-{d.Amount:0.00}".PadLeft(10));
        }

        // TOTAL
        sb.AppendLine(new string('-', 40));
        sb.AppendLine($"Total:".PadRight(30) + $"{totalAfter:0.00}".PadLeft(10));

        return sb.ToString();
    }
}
