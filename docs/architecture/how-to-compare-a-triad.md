# How To Compare A Triad

The triads in this repository are most useful when you compare the same concern across all three styles, not just the final output.

## Recommended Reading Order

1. Run the imperative demo first.
2. Run the plain C# demo second.
3. Run the LanguageExt demo last.

That order keeps the baseline familiar, then shows how far plain C# can go before introducing library abstractions.

## What To Look For

### Data Flow

- Where does data get transformed?
- Is the flow step-by-step and explicit, or spread across branching logic?
- Does the code read as a pipeline or as a procedure?

### Error Handling

- Are failures represented as exceptions, branches, nullable values, or explicit result types?
- Is failure handled close to where it occurs, or composed through the whole flow?
- Does the code accumulate errors or stop at the first one?

### Mutation

- Is state updated in place?
- Are intermediate values reassigned repeatedly?
- Does the code lean on immutable values and transformation instead?

### Side Effects

- Where does output happen?
- Where does file, database, or async work happen?
- Are side effects mixed into the core logic, or pushed to a boundary?

### Reuse and Composition

- Are steps reusable independently?
- Can small pieces be recombined easily?
- Does the code become clearer or more abstract as composition increases?

## What Each Style Usually Emphasizes

### Imperative

Expect to see:

- direct branching
- mutation or reassignment
- loops
- exceptions or early returns
- side effects closer to the main logic

This gives the baseline most imperative C# developers already recognize.

### Plain C# Functional Style

Expect to see:

- smaller pure helper functions
- records, pattern matching, and LINQ where useful
- local result types or explicit pipelines
- fewer implicit side effects
- more separation between computation and rendering

This shows how far core C# and .NET can take the design before using a library like LanguageExt.

### LanguageExt Functional Style

Expect to see:

- `Option`, `Either`, `Validation`, `Seq`, `Reader`, `State`, `Eff`, `Aff`, or similar abstractions
- explicit composition through monadic or applicative flows
- pure rule functions with effects isolated at boundaries
- less boilerplate around common functional patterns

This is where the tradeoff shifts from familiarity to expressive power.

## Questions To Ask While Comparing

1. Which version would be easiest to explain to an imperative teammate today?
2. Which version makes failure handling easiest to reason about?
3. Which version isolates side effects most clearly?
4. Which version would scale best if the workflow became larger?
5. What boilerplate disappears when moving from plain C# to LanguageExt?

## A Good Comparison Mindset

Do not treat the LanguageExt demo as automatically "better."
The real goal is to understand the tradeoff:

- what plain imperative code makes obvious
- what plain C# functional style improves without extra libraries
- what LanguageExt simplifies once the underlying functional idea is already understood
