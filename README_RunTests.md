# CoffeeOrder

TDD-built C# class library + MSTest suite for validating, classifying, pricing, discounting, and printing receipts for coffee orders.

**Author:** Leanne Kidder

## Requirements
- .NET 8 SDK
- MSTest.TestFramework / TestAdapter (restored via `dotnet restore`)

## Quick Start
```bash
# From repo root:
dotnet restore
dotnet build
dotnet test

#Run only the test project explicitly
dotnet test CoffeeOrder.Tests

#Filter tests exclude pending/ignored
dotnet test CoffeeOrder.Tests --filter "TestCategory!=Pending"

#Run All Tests
dotnet test
```

## Project Layout
CoffeeOrder/
  Models/
    Beverage.cs
    ClassificationResult.cs
  Validation/
    OrderValidator.cs
  Classification/
    BeverageClassifier.cs
  Pricing/
    PriceCalculator.cs
  Promotions/
    PromotionHelper.cs
  Receipt/
    ReceiptFormatter.cs
CoffeeOrder.Tests/
  *.cs (MSTest unit tests)

## Domain Model

# Beverage (immutable):
- BaseDrink
- Size
- Temp
- Milk
- PlantMilk
- Shots (int)
- Syrups (string[])
- Toppings (string[])
- IsDecaf (bool)

# ClassificationResult:
- KidSafe
- Caffeinated
- DairyFree
- VeganFriendly
- ContainsDairy
- ContainsTreeNuts

## Requirements

# Validation (OrderValidator)
Required: BaseDrink, Size (null/empty/whitespace fails).
Size must be one of Tall/Grande/Venti.
Syrups/Toppings cannot contain null or whitespace-only entries.
(Add’l rules live in tests; validator focuses on structure/presence.)

# Classification (BeverageClassifier)
- Caffeinated: Shots > 0 && !IsDecaf
- KidSafe: !Caffeinated (decaf with shots is still kid-safe)
- ContainsDairy: dairy milk or "Whipped Cream" topping (trim + case-insensitive)
- DairyFree: !ContainsDairy
- VeganFriendly: DairyFree && !Honey (any syrup equal to "Honey" disqualifies; trim + case-insensitive)
- ContainsTreeNuts: PlantMilk in {Almond, Cashew, Pistachio} or any syrup equal to "Hazelnut" (trim + case-insensitive)

# Pricing (PriceCalculator)

- Base prices (by drink)
    -Latte 3.00
    -Cappuccino 3.50
    -Espresso 2.50
    -Americano 2.00
    -Mocha 3.75 
    -Hot Chocolate 2.50
(case-insensitive keys)

- Size modifiers:
    -Tall +0.00
    -Grande +0.50
    -Venti +1.00

- Add-ons:
    -Shot 0.50 each
    -Syrup 0.25 each
    -Topping 0.30 each
    -Plant milk surcharge 0.50

- CalculateOrderPrice(IEnumerable<Beverage>) sums item prices.

# Promotions (PromotionHelper)
- HAPPYHOUR: 20% off subtotal of Hot drinks (rounded to 2 dp).
- BOGO: once per order, cheapest item free (rounded to 2 dp).
- Codes are case-insensitive and can stack. Returns a list of Discount records.

# Receipt (ReceiptFormatter)
- Header: Receipt - {author} - {MM/dd/yyyy HH:mm} (slashes forced via InvariantCulture).
- Line items: "Name (Size Temp)".PadRight(30) + price.ToString("0.00", InvariantCulture).PadLeft(10)
- Subtotal, discount lines (CODE Discount:), and Total.

## Sample

Receipt - Leanne - 01/02/2025 15:30
----------------------------------------
Latte (Tall Hot)                  3.00
----------------------------------------
Subtotal:                          3.00
HAPPYHOUR Discount:               -0.60
----------------------------------------
Total:                             2.40

## Testing & TDD

- Classifier: kid-safe paths; dairy/vegan + honey; allergen flags (dairy, tree nuts).

- Pricing: base, modifiers, add-ons, order sum, case-insensitive keys.

- Promotions: HAPPYHOUR hot-only; BOGO cheapest-free; stacking.

- Receipt: header (locale-invariant), items, subtotal, discounts, total.

## License

Educatonal Project

## Collaboration & Sources
- This work was completed individually.
- Tools used: .NET 8, MSTest.
- Assistance: ChatGPT used for rubber-ducking/TDD scaffolding (no code pasted verbatim without understanding).
