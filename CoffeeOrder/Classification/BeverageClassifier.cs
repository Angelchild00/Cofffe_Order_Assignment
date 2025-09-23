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
       bool dairyFree = !hasDairyMilk && !hasDairyToppings; 

       bool hasHoney = beverage.Syrups.Any(s => string.Equals(s?.Trim(),"Honey", StringComparison.OrdinalIgnoreCase));
       // A beverage is vegan-friendly if it is dairy-free and does not contain honey 
       bool veganFriendly = dairyFree && !hasHoney; // Simplified assumption 

        return new ClassificationResult
        {
            KidSafe = kidSafe,
            DairyFree = dairyFree,
            VeganFriendly = veganFriendly,
            Caffeinated = caffeinated
        };
    }

}