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

       bool dairyFree = string.IsNullOrWhiteSpace(beverage.Milk); 

        return new ClassificationResult
        {
            KidSafe = kidSafe,
            DairyFree = dairyFree,
            VeganFriendly = false,
            Caffeinated = caffeinated
        };
    }

}