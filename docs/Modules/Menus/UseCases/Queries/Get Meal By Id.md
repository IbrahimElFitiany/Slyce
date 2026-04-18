# Use Case: Get Meal By Id 

## Purpose

Returns full meal details (sizes, ingredients per size) by id.

---

## Flow

1. f
2. Validate that the requested ingredient set exactly matches the meal's declared ingredients.
3. Fetch nutrition data for all requested ingredients from the external Food Service.
4. Build the domain objects: `IngredientQuantity` list, with nutrition calculated via `NutritionCalculator`.
5. Call `meal.AddSize(...)` — domain invariants (duplicate name/sort order, ingredient mismatch, size limit) are enforced inside the aggregate.
6. Persist and commit the unit of work.

---

## External Dependencies

| Dependency   | Interface       | Purpose                                                                                                                                                                                         |
| ------------ | --------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Food Service | `IFoodServices` | Fetches nutrition data (protein, fat, carbs, etc.) for each ingredient by `FoodId`. Returns whatever it finds — the handler is responsible for validating that all requested IDs were returned. |

---

## Business Rules

- The target meal must exist.
- The new size's ingredient set must exactly match the meal's declared ingredients — no more, no less.
- Size name and sort order must be unique within the meal.
- Total number of sizes on a meal cannot exceed `MealSizesLimit`.
- Nutrition for the size is calculated automatically from ingredient quantities and their per-unit nutrition data — it is not accepted from the caller.

---

## Errors

|Exception|Condition|
|---|---|
|`NotFoundException`|No meal found with the given `MealId`|
|`MealSizeIngredientMismatchException`|The requested ingredient set doesn't exactly match the meal's ingredients|
|`MealSizesLimitExceededException`|Adding this size would exceed the allowed size limit|
|`DuplicateMealSizeException`|A size with the same name or sort order already exists on this meal|
|`ArgumentException`|Any required field is null, empty, or out of range|

---

## Notes

- Permission check is not yet implemented
- Ingredient mismatch is validated twice: once in the handler via `meal.HasExactIngredients(...)` before the external call, and again inside `meal.AddSize(...)` at the domain level. The handler-level check is an early exit to avoid an unnecessary Food Service round-trip. (may change in the future to a single policy inside the domain e.g. (canAddSize() )
- Nutrition is sourced entirely from the Food BC via `Food.Contracts`. The Menus BC does not store or manage raw food data.
- The `NutritionCalculator` is a domain service responsible for aggregating per-ingredient nutrition weighted by quantity.