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

            var text = ReceiptFormatter.Format(
                items: new[] { bev },
                promoCodes: Array.Empty<string>(),
                now: new DateTime(2025, 01, 02, 15, 30, 0),
                author: "Leanne"
            );

            var expected = "Receipt - Leanne - 01/02/2025 15:30";
            var firstLine = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None)[0];
            Assert.AreEqual(expected, firstLine);
            StringAssert.Contains(text, "Latte (Tall Hot)".PadRight(30) + "3.00".PadLeft(10));
            StringAssert.Contains(text, "Subtotal:".PadRight(30) + "3.00".PadLeft(10));
            StringAssert.Contains(text, "Total:".PadRight(30) + "3.00".PadLeft(10));
        }
    }
}
