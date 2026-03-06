
## Introduction

Slyce is built on a **Modular Monolith** architecture: a single deployable unit that enforces strict logical separation between its internal modules. This strikes a deliberate balance — the operational simplicity of a monolith with the boundary discipline typically associated with microservices.

---

## Structural Philosophy

### Modular Monolith

Each feature domain is encapsulated in its own **self-contained module**. Modules share a process and deployment boundary, but are isolated from each other's internals. No module may reach into another's data layer directly.

This enforces:

- **High cohesion** within modules
- **Low coupling** between modules
- A clear path toward extraction into services, if ever needed

---

## Module Structure — Clean Architecture

Each module is organized into four layers following Clean Architecture:

```
module/
├── domain/               # Core business logic
├── application/          # Use cases
├── infrastructure/       # Data access, external services
└── presentation/         # API controllers, DTOs, request/response mapping
```

### Layer Responsibilities

**Domain** The innermost layer. Contains the business rules and has zero dependencies on external frameworks or other modules. This is where DDD tactical patterns live.

**Application** Orchestrates use cases by coordinating domain objects. Owns the CQRS split. Also implements the **contract layer**, the only surface other modules are allowed to depend on.

**Infrastructure & Presentation** Infrastructure handles persistence and external integrations. Presentation handles inbound communication (HTTP, events, etc.). Both depend inward on the application layer

---

## Inter-Module Communication

Currently, modules interact **synchronously using method calls** through a clearly defined **contract layer**.

Each module that needs to expose functionality to others defines:

- An **abstraction** (interface) in its contract layer
- A concrete implementation wired up internally

Other modules depend **only on the interface**, never on the underlying domain model or data structures of the providing module. This preserves encapsulation and prevents tight coupling between module internals.


---

## DDD Tactical Patterns

The domain layer applies DDD tactical building blocks to model business concepts accurately:

| Pattern          | Role                                                                                                      |
| ---------------- | --------------------------------------------------------------------------------------------------------- |
| **Entity**       | An object with a distinct identity that persists over time                                                |
| **Value Object** | An immutable descriptor defined entirely by its attributes, no identity                                   |
| **Aggregate**    | A cluster of entities and value objects treated as a single consistency boundary, accessed through a root |

Aggregates act as the **consistency gate** — all state changes go through the aggregate root, ensuring business rules are enforced atomically.

---
## CQRS 

Within each module's application layer, reads and writes are handled through separate paths:

### Commands

- Represent **intent to change state**
- Retrieve **full aggregates** from the repository
- Enforce invariants through the domain model before persisting

### Queries

- Represent **requests for data**
- Bypass the domain model entirely
- Use **direct projections** to the read store for optimized, purpose-built read models

This separation keeps write paths rich with domain logic and read paths lean and performant.



