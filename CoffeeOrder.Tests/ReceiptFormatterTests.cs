using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoffeeOrder.Models;
using CoffeeOrder.Pricing;
using CoffeeOrder.Promotions;
using CoffeeOrder.Receipt;
using System;

namespace CoffeeOrder.Tests
{
    [TestClass]
    public class ReceiptFormatterTests
    {
        [TestMethod]
        public void Format_OneItem_NoPromos_ShowsHeaderItemAndTotals()
        {
            var bev = new Beverage(
                baseDrink: "Latte",
                size: "Tall",
                temp: "Hot",
                milk: null,
                plantMilk: null,
                shots: 0,
                syrups: Array.Empty<string>(),
                toppings: Array.Empty<string>(),
                isDecaf: true
            );
            //act
            var text = ReceiptFormatter.Format(
                items: new[] { bev },
                promoCodes: Array.Empty<string>(),
                now: new DateTime(2025, 01, 02, 15, 30, 0),
                author: "Leanne"
            );

            //assert
            var expected = "Receipt - Leanne - 01/02/2025 15:30";
            var firstLine = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None)[0];
            Assert.AreEqual(expected, firstLine);
            StringAssert.Contains(text, "Latte (Tall Hot)".PadRight(30) + "3.00".PadLeft(10));
            StringAssert.Contains(text, "Subtotal:".PadRight(30) + "3.00".PadLeft(10));
            StringAssert.Contains(text, "Total:".PadRight(30) + "3.00".PadLeft(10));
        }
        [TestMethod]
        public void Format_HappyHourPromo_ShowsDiscountAndTotal()
        {
            var hot = new Beverage(
                baseDrink: "Latte", // Base price: 3.00
                size: "Tall",   // Size adjustment: 0.00
                temp: "Hot",
                milk: null,
                plantMilk: null,
                shots: 0,
                syrups: Array.Empty<string>(),
                toppings: Array.Empty<string>(),
                isDecaf: true
            );

            var iced = new Beverage(
                baseDrink: "Latte", // Base price: 3.00
                size: "Tall",  // Size adjustment: 0.00    
                temp: "Iced",
                milk: null,
                plantMilk: null,
                shots: 0,
                syrups: Array.Empty<string>(),
                toppings: Array.Empty<string>(),
                isDecaf: true
            );

            var items = new[] { hot, iced };
            var subtotal = PriceCalculator.CalculateOrderPrice(items);
            var expectedDiscount = Math.Round(PriceCalculator.CalculatePrice(hot) * 0.20m, 2);
            var expectedTotal = Math.Round(subtotal - expectedDiscount, 2);

            //act
            var text = ReceiptFormatter.Format(
                items: items,
                promoCodes: new[] { "HAPPYHOUR" },
                now: new DateTime(2025, 01, 02, 15, 30, 0),
                author: "Leanne"
            );
            //assert
            StringAssert.Contains(text, "HAPPYHOUR Discount:".PadRight(30) + $"-{expectedDiscount:0.00}".PadLeft(10));
            StringAssert.Contains(text, "Total:".PadRight(30) + $"{expectedTotal:0.00}".PadLeft(10));
        }
    }
}

