# Use Case: Get Meal By ID

## Purpose

Retrieves a single reviewed meal with its full details — sizes, ingredients per size, and nutrition — by meal ID. Results are cached in Redis to minimize database load, and the cache is automatically invalidated when the meal's size composition changes.

---

## Flow

1. Construct the cache key as `meal:{MealId}`.
2. Attempt to read the meal from the distributed cache.
3. **Cache hit:** Deserialize the cached JSON and return it.
4. **Cache miss:**
    - Query the database for a meal matching the given ID where `Reviewed = true`.
    - Project directly into the response shape — sizes ordered by `SortOrder`, ingredients joined to their names, and full nutrition data per size.
    - If no matching meal is found, throw `NotFoundException`.
    - Serialize the result and write it to the cache with a 2-hour absolute expiration.
    - Return the result.

---

## Cache Invalidation

Cache entries are invalidated by domain events raised from the `MenuMeal` aggregate. The `ClearCacheDomainEventHandler` listens for the following events and clears the corresponding `meal:{MealId}` key:

|Domain Event|Trigger|
|---|---|
|`MealSizeAddedDomainEvent`|A new size is added via `AddSize()`|
|`MealSizeRemovedDomainEvent`|A size is removed via `RemoveSize()`|

---

## External Dependencies

|Dependency|Interface|Purpose|
|---|---|---|
|Database|`MenusDbContext`|Source of truth for meal data on cache miss|
|Distributed Cache|`IDistributedCache`|Redis-backed cache for served responses|
|Cache Service|`ICacheService`|Abstraction used by the domain event handler to clear specific cache keys|

---

## Business Rules

- Only meals with `Reviewed = true` are returned. Unreviewed meals are invisible to this query.
- Sizes are returned ordered by `SortOrder` ascending.
- Ingredient names are resolved by joining `IngredientQuantities` against the meal's `Ingredients` collection — the Menus BC does not store food names independently.
- Nutrition data is read directly from the persisted `MealSize.Nutrition` value object — it is not recalculated at query time.
- Cache entries expire after 2 hours regardless of write activity.

---

## Errors

| Exception                     | Condition                                                                                                   |
| ----------------------------- | ----------------------------------------------------------------------------------------------------------- |
| `NotFoundException`           | No reviewed meal with the given ID exists                                                                   |
| `Exception` (deserialization) | Cache contained a value that could not be deserialized — indicates a cache poisoning or schema mismatch bug |

---

## Notes

- The query handler lives in the Infrastructure layer and accesses `MenusDbContext` directly, bypassing the repository. This is intentional — the read side projects into response records and does not need aggregate loading.
-  `AsSplitQuery()` is called explicitly on the query to avoid the cartesian explosion from joining Sizes, IngredientQuantities, and Ingredients in a single query.
- The `ICacheService` abstraction wraps `IDistributedCache` for the domain event handler to remain infrastructure-agnostic. The query handler uses `IDistributedCache` directly since it lives in Infrastructure already.