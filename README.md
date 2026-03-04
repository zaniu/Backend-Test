# Introduction 
The code has been intentionally written in a messy and suboptimal way to serve as a "test exercise."
This solution is purely for practice and can be modified in any way you see fit. During a potential interview, we will discuss the changes you made and the reasoning behind them.

# Expectations
Here’s what we’d like you to work on:

1. Refactor the code
	Identify and fix any issues you notice to demonstrate how you would design and structure the solution.

2. Add a missing endpoint
	Implement the "Get Report" endpoint (refer to GetPurchaseReportById in PurchasesController). We expect the output to follow a format similar to the example below:

```csv
      CustomerName:,John Doe,,
      ProductId,Count,ProductName,Price
      1,1,Bread,4
      3,2,Cheese,6
      4,1,TV,200
``` 

There is no strict time limit for reworking the solution. If you believe certain improvements are worth making but would take too much time, feel free to provide a brief description of what you’d like to do and why, if given more time.

# Evaluation
The result of this exercise will serve as a starting point for the technical interview.
During the interview, you will be asked technical questions, and we will review your solution to this test together.
This home test and the interview are components of the overall evaluation process. We will consider your experience and other relevant factors throughout.

# Architecture Decisions

## 1) Runtime support: migration to .NET 10
The project is intentionally migrated to .NET 10 because the original target runtime was out of support.

- This keeps the solution aligned with a currently supported .NET version.
- It reduces risk related to security updates and maintenance over time.

Rationale: runtime support lifecycle is a high-impact engineering concern, so this was prioritized early.

## 2) Unified API contract (success + errors)
The API intentionally uses a custom, consistent response envelope for both successful and failed requests.

- Success responses use the contracts in [Backend-Test/Contracts](Backend-Test/Contracts).
- Error responses are centralized in [Backend-Test/Middleware/ExceptionHandlingMiddleware.cs](Backend-Test/Middleware/ExceptionHandlingMiddleware.cs).

Rationale: this keeps client handling predictable and avoids multiple response shapes across endpoints.

Tradeoff: this differs from the default ASP.NET `ProblemDetails` shape. In this exercise, consistency for clients was prioritized.

## 3) In-memory data scope (`Data.cs`)
`Data.cs` is treated as recruiter-provided fixture data ("NOT PART OF TEST. MERELY TO SUPPLY DATA").

- The code already introduces repository abstractions to separate application logic from storage details.
- Replacing in-memory data with real persistence (e.g. EF Core + database) is intentionally deferred to keep scope focused on the exercise goals.

Rationale: avoid overengineering while preserving a clean migration path to a persistence layer.

## 4) Nullable reference types decision (PoC/MVP scope)
Nullable reference types are intentionally left disabled in this exercise.

- Goal for this stage was to deliver a working PoC/MVP quickly, without introducing broad nullable-related boilerplate changes across the whole codebase.
- Confidence is primarily enforced through automated tests (unit + integration) rather than a full nullable-migration pass.

Rationale: this keeps scope focused and implementation velocity high for the assignment, while still keeping quality control through strong test coverage.