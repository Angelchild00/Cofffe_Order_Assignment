# CoffeeOrder Walkthrough (Validator + Tests)

## Quick Start
```bash
# From this folder:
dotnet restore
dotnet test CoffeeOrder.Tests

# (Optional) see ignored/pending categories only
dotnet test CoffeeOrder.Tests --filter "TestCategory!=Pending"
```

### Project layout
- `CoffeeOrder/` – production code (class library)
- `CoffeeOrder.Tests/` – MSTest tests referencing the library

### Notes
- Targets **.NET 8.0** with `ImplicitUsings`/`Nullable` enabled.
- Update package versions if your environment prefers newer MSTest/Test SDK versions.
- Add your remaining classes/tests alongside these starter files to reach your 25+ tests.
