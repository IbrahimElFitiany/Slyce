
## Overview

Retrieves a single address belonging to a specific customer, returning full address details including coordinates, label, and contact number.

---
## Flow

```
Controller
  └── GetCustomerAddressQuery(customerId, addressId)
        └── GetCustomerAddressQueryHandler
              └── DB join: Customers → CustomerAddresses
                    ├── Found     → return CustomerAddressResponse
                    └── Not found → throw NotFoundException
```

---

**Handler** — projects directly to `CustomerAddressResponse` at the DB level to avoid loading the full aggregate:

---

## Design Decision: Why a Join?

`CustomerAddress` is an **owned type** of the `Customer` aggregate. EF Core owned types do not have their own `DbSet<T>`, so they cannot be queried directly.

The current approach navigates from `Customers` → `SelectMany(CustomerAddresses)`, which EF Core translates into a single SQL join. This is efficient for the expected access pattern (lookup by customer + address ID).

**Generated SQL (approximate)**

```sql
SELECT a."Id", a."Label", a."ContactNumber", ...
FROM customers."Customers" c
INNER JOIN customers."CustomerAddresses" a ON a."CustomerId" = c."Id"
WHERE c."Id" = @customerId
  AND a."Id" = @addressId
LIMIT 1
```

---

## Future: Performance Escape Hatches

If profiling reveals this join becomes a bottleneck (e.g. very large `CustomerAddresses` tables, high read throughput), two options are available:

### Option 1 — Raw SQL

Bypass EF Core navigation entirely and query the `CustomerAddresses` table directly:

```csharp
var address = (await _customersDbContext.Database
    .SqlQuery<CustomerAddressResponse>(
    $"""
    SELECT
        "Id", "Label", "ContactNumber", "City", "Area",
        "StreetName", "StreetNumber", "Latitude", "Longitude"
    FROM customers."CustomerAddresses"
    WHERE "Id" = {query.AddressId}
      AND "CustomerId" = {query.CustomerId}
    """)
    .ToListAsync(cancellationToken))
    .FirstOrDefault();
```

**Trade-offs**

**Pros**

- No join overhead, hits the table directly
- Full control over query shape

**Cons**

- Schema changes must be reflected manually in the query

### Option 2 — Separate `CustomerAddress` Aggregate

Extract `CustomerAddress` into its own aggregate root with its own `DbSet<CustomerAddress>`. This removes the dependency on the `Customer` aggregate entirely for read operations.

**Trade-offs**

**Pros**
- Direct query, no join
- Scales independently

**Cons**
- Ownership invariant shifts from the aggregate boundary to a domain service — enforced at runtime rather than structurally, so nothing prevents bypassing it
- Higher migration and refactoring cost
---

## Error Handling

|Condition|Behaviour|
|---|---|
|`CustomerId` not found|`NotFoundException` → 404|
|`AddressId` not found or doesn't belong to customer|`NotFoundException` → 404|

> Both cases intentionally return the same 404 to avoid leaking whether a customer ID exists.

---

## Known Limitations

- `Customer-Id` header is currently used for mocking and is **not authenticated**. Any caller can supply an arbitrary GUID