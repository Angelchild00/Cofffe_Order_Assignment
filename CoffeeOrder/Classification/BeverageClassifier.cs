using CoffeeOrder.Models;

namespace CoffeeOrder.Classification;

public static class BeverageClassifier
{
    public static ClassificationResult Classify(Beverage beverage)
    {
        // A beverage is kid-safe if it has no shots and is not decaf
        // A beverage is caffeinated if it has one or more shots and is not decaf
       bool caffeinated = beverage.Shots > 0 && !beverage.IsDecaf;
       bool kidSafe = !caffeinated;


        // A beverage is dairy-free if it does not contain dairy milk and has no toppings that contain dairy        
       bool hasDairyMilk = !string.IsNullOrWhiteSpace(beverage.Milk);
       bool hasDairyToppings = beverage.Toppings.Any(t => t.Equals("Whipped Cream", StringComparison.OrdinalIgnoreCase));
       bool containsDairy = hasDairyMilk || hasDairyToppings;
       bool dairyFree = !containsDairy;

       bool hasHoney = beverage.Syrups.Any(s => string.Equals(s?.Trim(),"Honey", StringComparison.OrdinalIgnoreCase));

       // A beverage is vegan-friendly if it is dairy-free and does not contain honey 
       bool veganFriendly = dairyFree && !hasHoney;

       // A beverage contains nuts if it has almond, cashew, or pistachio milk
       bool containsTreeNuts = string.Equals(beverage.PlantMilk?.Trim(), "Almond", StringComparison.OrdinalIgnoreCase) || 
                               string.Equals(beverage.PlantMilk?.Trim(), "Cashew", StringComparison.OrdinalIgnoreCase) ||
                               string.Equals(beverage.PlantMilk?.Trim(), "Pistachio", StringComparison.OrdinalIgnoreCase);
       containsTreeNuts = containsTreeNuts || beverage.Syrups.Any(t => t.Equals("Hazelnut", StringComparison.OrdinalIgnoreCase));

        return new ClassificationResult
        {
            KidSafe = kidSafe,
            ContainsDairy = containsDairy,
            DairyFree = dairyFree,
            VeganFriendly = veganFriendly,
            Caffeinated = caffeinated,
            ContainsTreeNuts = containsTreeNuts
        };
    }

}