# Use Case: Approve Restaurant Application

## Purpose

Allows an authorized user to approve a pending restaurant application and create the initial restaurant setup.

---

## Flow

1. Fetch the `RestaurantApplication` aggregate by `ApplicationId`, throw if not found.
2. Approve the application via `application.Approve(userId)`.
3. Create the restaurant owner account through the Identity BC using `IIdentityServices`.
4. Create the `Restaurant` aggregate from the approved application data.
5. Create the main `RestaurantBranch` using the application's main branch location and company mobile number.
6. Add the `Restaurant` and `RestaurantBranch` to their repositories.
7. Persist changes using the unit of work.
8. Log the successful approval and created entities.
9. Return the created `RestaurantId`.

---

## External Dependencies

|Dependency|Interface|Purpose|
|---|---|---|
|Identity BC|`IIdentityServices`|Creates the restaurant owner account in the Identity module after the application is approved.|

---

## Business Rules

- The restaurant application must exist.
- A restaurant owner account must be created before the restaurant is persisted.
- The main branch is initialized using the application’s main branch location and company mobile number.

---

## Errors

| Exception                   | Condition                                                                  |
| --------------------------- | -------------------------------------------------------------------------- |
| `NotFoundException`         | No restaurant application found with the given `ApplicationId`             |
| `InvalidOperationException` | The application is already approved or cannot transition to approved state |
| `ArgumentException`         | Any required value is null, empty, or invalid                              |
| Identity-related exception  | Failed to create the restaurant owner account                              |

---

## Notes

- Permission checks are not yet implemented.
- There is a known consistency issue: owner creation and database persistence occur in separate transactions. If `SaveChangesAsync` fails after the owner account is created, an orphaned identity account may remain.