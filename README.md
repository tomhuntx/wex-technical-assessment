# wex-technical-assessment

## Overview

This application is a technical assessment by Wex as completed by Tom Hunter.

It is an ASP.NET Core Web API that manages credit cards and purchase transactions, with support for currency conversion using external exchange rate data.

It demonstrates:

- RESTful API design
- PostgreSQL integration via Entity Framework Core
- Currency conversion logic with historical constraints
- Clean service-layer architecture
- Use of Docker

Note that the currency default is assumed to be USD for all cards and transactions for the purpose of this exercise.

---

## Features

### Create and manage Cards

- Store cards with a defined credit limit
- Each card is assigned a unique identifier (UUID)

### Store Purchase Transactions

- Associate transactions with a card
- Includes:
  - Description
  - Transaction date
  - Amount

### Currency Conversion

- Convert transactions into a specified currency
- Uses exchange rates:
  - On or before the transaction date
  - Within a 6-month window

### Available Balance Calculation

- Calculates remaining balance:
  ```
  Credit Limit - Sum of Transactions
  ```
- Optionally converts balance into another currency using the latest exchange rate

---

## Running the Application with Docker

This application uses **PostgreSQL via Docker** to provide a production-like database environment.

### Prerequisites

- .NET SDK installed
- Docker Desktop installed and running

---

### Step 1 — Navigate to project root

Ensure you are in the folder containing:

- `CardApi.csproj`
- `docker-compose.yml`

---

### Step 2 — Start PostgreSQL

Run the following command:

```bash
docker compose up -d
```

This will:

- Pull the PostgreSQL image (if not already available)
- Start a container named `card-api-db`
- Create a persistent volume for database storage

---

### Step 3 — Verify the container is running

```bash
docker ps
```

You should see a running container named `card-api-db`.

---

### Step 4 — Run the API

```bash
dotnet run
```

---

### Step 5 — Access Swagger UI

⚠️ The application must be run using **HTTPS**.

Open your browser and navigate to:

```
http://localhost:5015/swagger/index.html
```

---

### Stopping the database

To stop the PostgreSQL container:

```bash
docker compose down
```

---

## 📡 API Endpoints

---

### 🧾 Cards

#### GET `/Card`

Returns all cards. This would not be a production endpoint without strict security.

---

#### POST `/Card`

Create a new card

**Request Body**

```json
{
  "creditLimit": 1000
}
```

---

#### GET `/Card/{cardId}/balance`

Get available balance for a card

**Query Parameters**

- `targetCurrency` (optional)

**Example**

```
GET /Card/{cardId}/balance?targetCurrency=EUR
```

**Response includes:**

- Available balance
- Currency (if converted)
- Exchange rate used
- Converted balance

---

### 💳 Transactions

#### GET `/Transaction`

Returns all transactions

---

#### POST `/Transaction`

Create a new transaction

**Request Body**

```json
{
  "cardId": "GUID",
  "description": "Purchase",
  "transactionDate": "2024-01-01T00:00:00Z",
  "amount": 100
}
```

---

#### GET `/Transaction/{transactionId}/convert`

Convert a transaction into a specified currency

**Query Parameters**

- `currency` (required)

**Example**

```
GET /Transaction/{transactionId}/convert?currency=AUD
```

**Response includes:**

- Original amount
- Exchange rate used
- Converted amount

---

## Currency Conversion Rules

- Uses exchange rates from the Treasury Reporting Rates API
- See https://fiscaldata.treasury.gov/datasets/treasury-reporting-rates-exchange/treasury-reporting-rates-of-exchange
- Must use a rate:
  - On or before the transaction date
  - Within the previous 6 months
- Must used official treasury currency syntax such as "Australia-Dollar"

- If no valid rate exists → returns error

---
