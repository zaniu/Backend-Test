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
Decision: migrate the solution to .NET 10 because the original runtime was out of support.

Why:
- keeps the project on a supported runtime,
- reduces maintenance and security risk.

Tradeoff: runtime upgrade can introduce small compatibility work, but this is lower risk than staying on an unsupported target.

## 2) Unified API contract (success + errors)
Decision: use a custom response envelope for both successful and failed API responses.

Implementation:
- success contracts are in [Backend-Test/Contracts](Backend-Test/Contracts),
- error responses are centralized in [Backend-Test/Middleware/ExceptionHandlingMiddleware.cs](Backend-Test/Middleware/ExceptionHandlingMiddleware.cs).

Why: keeps client integration predictable and avoids mixed response shapes.

Tradeoff: this differs from default ASP.NET `ProblemDetails`; consistency was prioritized for this exercise.

## 3) In-memory data scope (`Data.cs`)
Decision: keep `Data.cs` as recruiter-provided fixture data ("NOT PART OF TEST. MERELY TO SUPPLY DATA").

Why:
- repository abstractions already separate business logic from storage details,
- replacing in-memory storage with DB persistence is a straightforward next step.

Tradeoff: in-memory storage is not production-grade, but avoids overengineering for assignment scope.

## 4) Nullable reference types decision (PoC/MVP scope)
Decision: keep nullable reference types disabled for this phase.

Why:
- prioritize PoC/MVP delivery speed,
- avoid broad nullable-migration churn across the codebase,
- rely on automated unit/integration tests for current quality control.

Tradeoff: nullable analysis benefits are postponed to a dedicated hardening phase.

## 5) Exception-first flow in handlers + centralized middleware mapping
Decision: for this PoC/MVP stage, handlers throw domain/application exceptions and [Backend-Test/Middleware/ExceptionHandlingMiddleware.cs](Backend-Test/Middleware/ExceptionHandlingMiddleware.cs) converts them into HTTP responses, instead of using an explicit Result pattern everywhere.

Why:
- keeps application/handler code direct and readable,
- reduces controller boilerplate (no repetitive result-to-response mapping in each action),
- preserves one central place for HTTP error-shape policy.

Tradeoff:
- exception-based flow is less explicit than `Result<T>` in method signatures,
- this is acceptable for current scope where delivery speed and simplicity are prioritized.

Future direction: if domain flows become significantly more branching/complex, evaluate a Result pattern for selected paths where explicit failure typing adds clear value.

## 6) No Specification pattern (yet)
Decision: do not introduce Specification pattern at current size/complexity.

Why:
- current query rules are simple and readable in handlers/repositories,
- introducing Specification now would add abstractions and indirection without immediate payoff.

Tradeoff: query/filter logic can become duplicated over time as use-cases grow.

When to introduce it:
- repeated filtering/business predicates appear across multiple handlers,
- query composition starts growing (combinable predicates, sorting/paging variants),
- repository methods begin to proliferate due to predicate permutations.

At that point, Specification pattern becomes a strong candidate to improve reuse and testability.