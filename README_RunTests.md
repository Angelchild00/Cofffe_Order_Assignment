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

#AppDriver(CLI) - run a demo receipt:
dotnet run --project CoffeeOrder.App --HAPPYHOUR BOGO
```