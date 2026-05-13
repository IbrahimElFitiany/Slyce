# Use Case: Set Password

## Purpose

Allows a user to set their password using a secure one-time token sent via email during account creation.

---

## Flow

1. Hash the incoming reset token using SHA256.
2. Build the cache key using the hashed token.
3. Retrieve the associated `UserId` from cache.
4. If no value exists, reject the request.
5. Fetch the user by `UserId`, throw if not found.
6. Hash the new password using `IPasswordHasher`.
7. Update the user password via `user.UpdatePassword(...)`.
8. Persist changes using the unit of work.
9. Remove the used token from cache.

---

## External Dependencies

| Dependency      | Interface         | Purpose                                    |
| --------------- | ----------------- | ------------------------------------------ |
| Cache           | `ICacheService`   | Stores and validates password reset tokens |
| Password Hasher | `IPasswordHasher` | Securely hashes the new user password      |
| User Repository | `IUserRepository` | Fetches user aggregate by ID               |

---

## Business Rules

- The reset token must exist in cache and be valid.
- The token is single-use and must be removed after success.
- Password must always be stored in hashed form.
- Token verification is done via SHA256 hash comparison, not raw token matching.

---

## Errors

| Exception           | Condition                                      |
| ------------------- | ---------------------------------------------- |
| `Exception`         | Token is invalid or expired (missing in cache) |
| `NotFoundException` | User does not exist for the resolved `UserId`  |


---

## Notes
