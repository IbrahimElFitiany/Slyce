![Sufra Logo](https://i.postimg.cc/wBXh5dnN/logo.png)
# 🥗 Slyce — Smart Prepared Meals Ordering & Nutrition Platform

Slyce is a modular, AI-powered food ordering platform that connects users with local chefs and restaurants to deliver meals tailored to their **fitness goals**, **dietary preferences**, and **macronutrient targets**.  
Built as a modular monolith in .NET 9, each module follows Clean Architecture principles to ensure high cohesion, low coupling, and clear separation of concerns — making the system scalable and ready for future microservice evolution.

---

## 📖 Problem Statement

> People trying to maintain a healthy diet or hit fitness goals often struggle to plan meals because it’s time-consuming and confusing to calculate macros and calories.

Slyce solves this by providing **ready-to-eat meals from local chefs and restaurants**, perfectly matched to your fitness goals and nutritional needs — so you can save time, stay healthy, and skip the hassle of meal prep.

---

## 🎯 Target Audience

- **Hardcore gym-goers (bodybuilders)** — want precise macro tracking and meal consistency.  
- **Busy professionals** — want healthy, goal-aligned meals without calorie counting.  
- **Beginners** — want guided, simple meal plans that just work.
- 
---
## 🧱 System Architecture

Slyce follows a **Modular Monolith Architecture** — each domain encapsulated in its own module.

[![Slyce Architecture Diagram](https://github.com/user-attachments/assets/629fdcd7-d53c-442d-87c9-87c885c7a7b8)](https://ibb.co/MxhY07fQ)

---

## 🧠 Architecture Components

| Component | Description |
|------------|-------------|
| **Keycloak** | Identity Provider for authentication & authorization (OAuth2 + OpenID Connect). |
| **PostgreSQL** | Modular schema-per-module database for strong data isolation. |
| **Redis** | Used for caching and fast access to frequently used data. |
| **Slyce API (.NET 9)** | Modular backend containerized with Docker. |
| **Client (Web/Mobile)** | Future frontend consuming the API. |

---

## 🚀 Tech Stack

| Layer | Technologies |
|--------|--------------|
| **Backend** | .NET 9 (C#), ASP.NET Core, API Versioning |
| **Database** | PostgreSQL |
| **Caching** | Redis |
| **Authentication** | Keycloak |
| **ORM** | Entity Framework Core |
| **Logging** | Serilog |
| **Architecture** | Clean Architecture + Modular Monolith |

---


*Slyce — Eat Smart. Train Hard. And Never Skip a Meal*
