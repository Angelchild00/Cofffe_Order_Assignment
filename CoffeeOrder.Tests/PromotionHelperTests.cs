using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoffeeOrder.Models;
using CoffeeOrder.Pricing;
using CoffeeOrder.Promotions;


namespace CoffeeOrder.Tests
{
    [TestClass]
    public class PromotionHelperTests
    {
        [TestMethod]
        public void Apply_HappyHour_DoesNotDiscountIced()
        {
            var hot = new Beverage(
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

            var iced = new Beverage(
                baseDrink: "Latte",
                size: "Tall",
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

            var (totalAfter, discounts) = PromotionHelper.Apply(items, new[] { "HAPPYHOUR" });

            var expectedDiscount = Math.Round(PriceCalculator.CalculatePrice(hot) * 0.20m, 2);
            var expectedTotal = Math.Round(subtotal - expectedDiscount, 2);

            Assert.AreEqual(1, discounts.Count);
            Assert.AreEqual("HAPPYHOUR", discounts[0].Code);
            Assert.AreEqual(expectedDiscount, discounts[0].Amount);
            Assert.AreEqual(expectedTotal, totalAfter);
        }
    }
}
