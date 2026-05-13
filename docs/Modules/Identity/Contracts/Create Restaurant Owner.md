# Contract Method: Create Restaurant Owner

## Purpose

Creates a new restaurant owner account inside the Identity module and triggers the initial password setup flow.

---

## Flow

1. Create the `User` aggregate using `User.CreateRestaurantOwner(...)`.
2. Inside the aggregate factory:
    - Instantiate a `User` with type `RestaurantOwner`.
    - Raise `RestaurantOwnerCreatedDomainEvent`.
3. Add the created user to the repository.
4. Persist changes using the unit of work.
5. After persistence, handle the raised domain event:
    - Generate a secure random password setup token.
    - Hash the token using SHA256.
    - Store the hashed token in cache.
    - Send the password setup email containing the raw token.

---

## External Dependencies

| Dependency    | Interface       | Purpose                                                                                   |
| ------------- | --------------- | ----------------------------------------------------------------------------------------- |
| Cache         | `ICacheService` | Stores the hashed password setup token temporarily for verification during password setup |
| Email Service | `IEmailSender`  | Sends the password setup email to the restaurant owner                                    |

---

## Business Rules

- The email must be valid.
- The created user must have the `RestaurantOwner` role
- A password setup token must never be stored in plain text.
- Only the hashed token is persisted in cache.
- The raw token is only sent through email.
- Creating a restaurant owner automatically triggers the password setup flow.

---

## Errors

| Exception                   | Condition                                |
| --------------------------- | ---------------------------------------- |
| `ArgumentException`         | Invalid first name, last name, or email  |
| `InvalidOperationException` | User creation violates domain invariants |

---

## Notes

- The cache key format is: `identity:reset-password-token:{hashedToken}`.
- The cache stores the `UserId` associated with the token.
- The plain token is never persisted.
- The password setup email is triggered asynchronously through a domain event handler.
- The flow currently assumes eventual consistency between persistence, caching, and email delivery.
- The frontend password setup URL is currently hardcoded and should eventually move to configuration.
