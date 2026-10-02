# Development Workflow

This document describes how development is organized in the SmartCity repository and why these conventions were chosen.

SmartCity is primarily an educational project. The goal is not only to implement functionality, but also to make the evolution of the code easy to follow.

Because of that, Git history is treated as part of the learning material.

## Main Branch

The primary branch is:

`main`

`main` should always contain the latest completed and working version of the project.

There is no `develop` branch.

A separate development branch would not provide much value for this project because development is organized around small, isolated pull requests. Each pull request is based directly on the latest `main`.

## Pattern Branches

Each GoF design pattern is implemented in its own branch and pull request.

Pattern branches use the following format:

`pattern/<pattern>/<feature>`

Examples:

- `pattern/factory-method/vehicle-creation`
- `pattern/strategy/route-planning`
- `pattern/state/delivery-lifecycle`
- `pattern/observer/order-notifications`

The first part describes the purpose of the branch, the second part identifies the pattern, and the final part identifies the functionality in which the pattern is demonstrated.

Each new pattern branch must be created from the latest version of `main`.

## Other Branches

Changes unrelated to introducing a design pattern may use branches such as:

- `fix/<description>`
- `docs/<description>`
- `chore/<description>`
- `refactor/<description>`

Examples:

- `fix/route-validation`
- `docs/update-architecture`
- `chore/update-project-configuration`

## One Pattern per Pull Request

As a general rule:

**one pattern = one branch = one pull request**

Keeping patterns isolated makes it easier to understand what architectural problem is being addressed and how the pattern changes the implementation.

It also makes individual patterns easier to review later.

## Pattern Implementation Workflow

Most pattern pull requests should demonstrate the transition from a normal implementation to an implementation using the selected design pattern.

The typical sequence is:

1. Implement the required functionality without intentionally introducing the target pattern.
2. Refactor that functionality using the selected pattern.
3. Document the pattern and explain why it was introduced.

For example:

```text
feat: add route planning
refactor: apply Strategy to route planning
docs: document Strategy pattern
```

The first implementation should still be reasonable production-style code.

We should not intentionally create bad or artificial code simply to make the design pattern appear necessary.

The purpose is to observe how a normal implementation starts producing a design problem and then understand how the pattern addresses that problem.

## Why We Preserve Before and After

A major goal of SmartCity is to make architectural changes visible.

For this reason, the commit that introduces the functionality and the commit that introduces the pattern should normally remain separate.

This allows us to compare:

```text
before pattern
      ↓
architectural problem
      ↓
pattern introduced
      ↓
after pattern
```

Looking at the Git diff between these commits should make it possible to understand what the pattern actually changed.

This is more useful for learning than seeing only the final implementation.

## Commit Convention

SmartCity uses Conventional Commit-style prefixes.

Common types include:

- `feat:` — new functionality
- `fix:` — bug fix
- `refactor:` — structural changes without intentionally changing behavior
- `docs:` — documentation changes
- `test:` — test changes
- `chore:` — repository or maintenance work
- `build:` — build system or dependency changes
- `ci:` — CI/CD changes
- `perf:` — performance improvements

Examples:

```text
feat: add vehicle creation
refactor: apply Factory Method to vehicle creation
docs: document Factory Method pattern
```

Each commit should represent one meaningful logical change.

## Commit History

Temporary development commits are acceptable while working locally.

Examples may include:

```text
fix
wip
fix tests
oops
try again
```

However, these commits should normally be cleaned up before merging the pull request.

They should be squashed or fixed up into the logical commit they belong to.

The final pull request history should contain a small number of meaningful commits rather than every intermediate correction.

For example, this:

```text
feat: add route calculation
fix route calculation
fix tests
rename stuff
wip
fix again
refactor strategy
docs
```

should preferably become:

```text
feat: add route planning
refactor: apply Strategy to route planning
docs: document Strategy pattern
```

## Why Pattern Pull Requests Are Not Squash-Merged

Pattern pull requests should normally not be squash-merged.

In many production repositories, squash merging is useful because it keeps the `main` branch history compact.

SmartCity has a different goal.

The individual commits are intentionally part of the educational material.

In particular, we want to preserve the transition:

```text
feat
  ↓
refactor
  ↓
docs
```

Squashing the entire pull request into a single commit would remove the ability to easily compare the implementation before and after introducing the pattern.

For that reason, pattern pull requests should normally use a regular merge commit.

## Merge Strategy

Pattern pull requests should normally be merged into `main` using:

**Create a merge commit**

This keeps the commits from the pattern branch intact while also clearly grouping them under one pull request.

A simplified history might look like:

```text
*   Merge PR #3 - State — Delivery Lifecycle
|\
| * docs: document State pattern
| * refactor: apply State to delivery lifecycle
| * feat: add delivery lifecycle
|/
*   Merge PR #2 - Strategy — Route Planning
|\
| * docs: document Strategy pattern
| * refactor: apply Strategy to route planning
| * feat: add route planning
|/
```

This structure makes it easy to navigate the project history and inspect one pattern at a time.

## Pull Request Naming

Pattern pull requests use the following format:

`[Pattern] <Pattern Name> — <Feature>`

Examples:

```text
[Pattern] Factory Method — Vehicle Creation
[Pattern] Strategy — Route Planning
[Pattern] State — Delivery Lifecycle
```

## Pull Request Description

Each pattern pull request should contain at least a short summary of:

- the functionality introduced;
- the design problem encountered;
- the pattern applied;
- what changed after the refactoring;
- the learning goal of the pull request.

The description does not need to duplicate the full pattern documentation.

Detailed explanations belong under `docs/patterns/`.

## Pattern Documentation

Each implemented GoF pattern receives its own Markdown file under:

`docs/patterns/`

For example:

```text
docs/
  patterns/
    factory-method.md
    strategy.md
    state.md
```

Pattern documentation should generally include:

- Intent
- Problem
- Structure
- Participants
- SmartCity implementation
- Before
- After
- Why this pattern
- Trade-offs
- When not to use
- Interview notes

The documentation should explicitly mention the SmartCity classes that participate in the pattern.

When useful, Mermaid diagrams should be included to visualize the structure.

## Tests

Tests should verify behavior rather than the existence of a design pattern.

The refactoring commit should ideally preserve the behavior established by the feature commit.

This is useful because the tests help demonstrate that the architecture changed while the expected result remained the same.

Additional tests may be introduced when the pattern enables or requires additional scenarios.

## Verification Before Merge

Before a pull request is considered complete:

```text
dotnet build SmartCity.sln
dotnet test SmartCity.sln
```

should succeed.

The branch should also be reviewed for:

- accidental temporary files;
- unnecessary abstractions;
- unused code;
- temporary debugging code;
- meaningless intermediate commits.

## Starting the Next Pattern

After a pattern pull request is merged:

1. switch to `main`;
2. update it from the remote repository;
3. optionally delete the merged branch;
4. create the next pattern branch from the updated `main`.

This ensures every pattern builds on top of the latest version of SmartCity.

## Guiding Principle

The goal is not to place all 23 GoF patterns into the project as quickly as possible.

The goal is to understand:

- what problem each pattern solves;
- what the code looks like before the pattern;
- how the structure changes after introducing it;
- what advantages the pattern provides;
- what complexity it introduces;
- when the pattern would be unnecessary.

A pattern should therefore be introduced because it improves a concrete SmartCity scenario, not simply because the project still needs to demonstrate that pattern.
