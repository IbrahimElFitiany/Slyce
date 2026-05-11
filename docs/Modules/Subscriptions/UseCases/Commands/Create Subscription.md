## Use Case: Create Subscription

## Purpose

Allows a customer to subscribe to a one or more meals from a specific restaurant branch, specifying delivery days, a time slot, chosen meal sizes and quantities, and a billing cycle. The subscription captures an address snapshot and a price snapshot at the time of creation.

---

## Flow

1. Fetch `BranchForSubscription` from the Restaurants contract — provides the restaurant ID, branch coordinates, and weekly schedule.
2. Fetch the customer's delivery address by ID from the Customers contract.
3. Resolve all requested meal size IDs against the restaurant via the Menus contract — returns price and meal metadata per size.
4. Assert that the number of returned sizes equals the number of distinct requested size IDs, guaranteeing all sizes belong to the target restaurant.
5. Parse the requested `TimeSlot` into a `DeliveryTimeFrame` and construct a `BranchSchedule` from the branch's schedule map.
6. Run eligibility checks via `SubscriptionEligibilityService`:
    - Delivery time frame must fall within the branch's operating hours on all requested subscription days.
    - Customer's delivery address must be within the configured delivery radius of the branch.
7. Construct the `Subscription` aggregate — captures address snapshot, delivery days, time frame, billing cycle, start date, and a `SubscriptionMeal` per requested size with price snapshot.
8. Persist via repository and commit the unit of work.
9. Log and return the new subscription ID.

---

### External Dependencies

|Dependency|Interface|Purpose|
|---|---|---|
|Restaurants BC|`IRestaurantServices.GetBranchForSubscriptionAsync`|Fetch branch coordinates, restaurant ID, and weekly schedule|
|Customers BC|`ICustomerServices.GetCustomerAddressByIdAsync`|Fetch delivery address coordinates and snapshot fields|
|Menus BC|`IMenuQueryServices.GetMealSizesByRestaurantAsync`|Validate size IDs belong to restaurant; fetch price and meal metadata|
|Domain Service|`SubscriptionEligibilityService`|Enforce schedule and radius eligibility rules|
|Persistence|`ISubscriptionRepository`, `IUnitOfWork`|Persist the new subscription aggregate|

---

### Business Rules

- All requested meal size IDs must belong to the target restaurant — no cross-restaurant sizes.
- Delivery time slot must fall within the branch's operating hours on every requested delivery day.
- Customer's delivery address must be within the configured delivery radius of the branch.
- Address is captured as a snapshot at subscription time — later address changes do not affect existing subscriptions.
- Meal price is captured as a snapshot at subscription time (`priceAtSubscription`) — price changes do not affect active subscriptions.
- Billing cycle and start date are caller-supplied; no default is imposed by the domain.

---

### Errors

|Exception|Condition|
|---|---|
|`NotFoundException`|No branch found with the given `BranchId`|
|`NotFoundException`|One or more meal size IDs do not exist or do not belong to the restaurant|
|`NotFoundException`|No customer address found for the given `DeliveryAddressId`|
|`ScheduleMismatchException`|Requested delivery days or time slot fall outside branch operating hours|
|`CustomerOutOfRadiusException`|Customer address exceeds the configured delivery radius for the branch|
|`ArgumentException`|Any required field is null, empty, or out of range|

---

### Notes