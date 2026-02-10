# ModularShop
Modular monolith e-commerce backend built with ASP.NET Core, combining CRUD and rich domain modules, CQRS and event-driven integration.

## Table of contents

1. [Introduction](#introduction)
2. [Modules](#modules)
3. [Architectural Approach](#architectural-approach)
4. [Technical Highlights](#technical-highlights)
5. [Sources](#sources)

---

## 🧭 Introduction

As applications grow, traditional layered monoliths often become tightly coupled, harder to understand, and increasingly difficult to evolve.

Microservices are frequently introduced as a solution, but they also bring significant operational and cognitive overhead that is not always justified.

A modular monolith offers a middle ground — combining the simplicity of a single deployment unit with the clarity, separation, and autonomy of well-defined modules.

This project demonstrates how to structure a modular monolith in ASP.NET Core, where different modules can evolve independently, apply different architectural styles, and communicate through events while remaining part of one cohesive system.

---

## 🧩 Modules

### Catalog
- Simple CRUD module
- Manages `Product` entities
- Publishes product-related integration events

### Orders
- Rich domain model with business invariants
- CQRS (separate command and query models)
- Stores `ProductSnapshot` built from Catalog events
- Demonstrates eventual consistency between modules

### Identity
- User registration and authentication
- JWT-based authorization

---

## 🏗️ Architectural Approach

- Modular monolith (single deployment, multiple isolated modules)
- Clear module boundaries
- No direct references between domain models across modules
- Communication between modules via Integration Events
- CQRS applied only where business complexity justifies it (Orders)

Patterns are applied pragmatically — not every module uses the same approach.

---

## ⚙️ Technical Highlights

- Automatic module discovery via `IModule` + bootstrapper
- Module-level service registration
- Module-owned endpoints
- Centralized exception handling with module-specific mapping
- EF Core with separate schemas per module

## 📚 Sources

This project was inspired by the following resources:

- [Confab repository](https://github.com/devmentors/Confab) by devmentors