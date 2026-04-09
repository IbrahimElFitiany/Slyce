# Use Case: Create Menu Meal

## Purpose

Allows a restaurant to add a new meal to their menu. The meal is created with a full ingredient list and one or more size variants, each with its own pricing, ingredient quantities, and auto-calculated nutrition.

---

## Flow

1. _(Not yet implemented)_ Verify the caller has permission to create meals for the given restaurant.
2. Check that no meal with the same name already exists in this restaurant.
3. Fetch nutrition data for all requested ingredients from the external Food Service.
4. Validate that every requested ingredient was found in the Food Service response.
5. Build the domain objects: `MealIngredient` list, and `MealSizeCreationInput` list (with nutrition calculated per size).
6. Construct the `MenuMeal` aggregate — domain invariants are enforced inside the constructor.
7. Persist the meal and commit the unit of work.

---

## External Dependencies

| Dependency   | Interface       | Purpose                                                                                                                                                                                         |
| ------------ | --------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Food Service | `IFoodServices` | Fetches nutrition data (protein, fat, carbs, etc.) for each ingredient by `FoodId`. Returns whatever it finds — the handler is responsible for validating that all requested IDs were returned. |

---

## Business Rules

- Meal name must be unique per restaurant.
- A meal must have at least one ingredient.
- A meal must have at least one size and cannot exceed `MealSizesLimit`.
- Size names and sort orders must be unique within a meal.
- Every size must reference exactly the same set of ingredients as the meal — no more, no less. A size cannot omit an ingredient or introduce one that isn't declared at the meal level.
- Nutrition per size is calculated automatically from ingredient quantities and their per-unit nutrition data — it is not accepted from the caller.
- Meal is created as available by default.

---

## Errors

| Exception                             | Condition                                                               |
| ------------------------------------- | ----------------------------------------------------------------------- |
| `DuplicateException`                  | A meal with the same name already exists in this restaurant             |
| `NotFoundException`                   | One or more ingredient IDs were not found in the Food Service           |
| `MealSizesLimitExceededException`     | The number of sizes exceeds the allowed limit                           |
| `DuplicateMealSizeException`          | Two sizes share the same name or sort order                             |
| `MealSizeIngredientMismatchException` | A size's ingredient set doesn't exactly match the meal's ingredient set |
| `ArgumentException`                   | Any required field is null, empty, or out of range                      |

---

## Notes

- Permission check is not yet implemented — see `TODO` in handler.
- Nutrition is sourced entirely from the Food BC via `Food.Contracts`. The Menus BC does not store or manage raw food data.
- The `NutritionCalculator` is a domain service responsible for aggregating per-ingredient nutrition weighted by quantity.