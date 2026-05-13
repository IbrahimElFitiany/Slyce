# Use Case: Create Restaurant Application

## Purpose

Allows a new restaurant owner to submit an onboarding application. Validates uniqueness of company email and brand name before persisting the application.

---

## Flow

1. Check that no existing application shares the same `CompanyEmail` , throw if duplicate.
2. Check that no existing application shares the same `BrandName`, throw if duplicate.
3. Build the `RestaurantApplication` aggregate using the provided fields, mapping raw primitives to value objects (`Email`, `PhoneNumber`, `Address`, `Coordinates`).
4. Persist via the repository and commit the unit of work.
5. Return the new application's `Id`.

---

## Business Rules

- `CompanyEmail` must be unique across all existing applications.
- `BrandName` must be unique across all existing applications.

---

## Errors

| Exception            | Condition                                                   |
| -------------------- | ----------------------------------------------------------- |
| `DuplicateException` | An application with the same `CompanyEmail` already exists. |
| `DuplicateException` | An application with the same `BrandName` already exists.    |


---

## Notes

- Value object construction (`Email`, `PhoneNumber`, `Address`, `Coordinates`) performs its own validation, any violation surfaces as an `ArgumentException` from within the domain layer.

- Both duplicate checks log a warning before throwing to aid in observability without requiring an exception filter.