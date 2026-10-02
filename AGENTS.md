# SmartCity Repository Guidelines

## Project Purpose

SmartCity is an educational .NET project used to study all 23 Gang of Four design patterns through incremental implementation and refactoring.

Each design pattern should be introduced through a dedicated pull request.

The Git history is part of the learning material: for most patterns, the repository should preserve the implementation before and after applying the pattern.

---

## Branching

The default branch is:

`main`

Each design-pattern pull request must use a branch named:

`pattern/<pattern>/<feature>`

Examples:

- `pattern/factory-method/vehicle-creation`
- `pattern/strategy/route-planning`
- `pattern/state/delivery-lifecycle`
- `pattern/observer/order-notifications`

For non-pattern work, use:

- `fix/<description>`
- `docs/<description>`
- `chore/<description>`
- `refactor/<description>`

Do not create a `develop` branch.

---

## Git Workflow Automation

When starting a new pattern task, and if the current environment allows Git branch operations:

1. Inspect the current repository state.
2. Ensure there are no unsafe uncommitted changes that could be overwritten.
3. Switch to `main`.
4. Pull the latest changes from `origin` using fast-forward only.
5. Create a new branch from the updated `main`.
6. Use the required branch naming convention:

   `pattern/<pattern>/<feature>`

7. Perform the requested implementation.
8. Create meaningful commits according to the repository commit policy.
9. Push the branch to `origin` and set the upstream branch.
10. If the environment supports creating pull requests, create the pull request using the repository PR conventions.
11. Stop after the branch and pull request are ready for review.

Typical Git flow:

```bash
git status
git switch main
git pull --ff-only
git switch -c pattern/strategy/route-planning

# implementation and commits

git push -u origin pattern/strategy/route-planning
```

If the current environment does not allow branch creation or push operations:

- verify that the current branch follows the expected naming convention;
- perform only the operations that are safely supported;
- report what still needs to be done manually.

If the target branch already exists locally or remotely, do not recreate, overwrite, or force-update it.

Inspect the existing branch first and continue only when doing so is safe.

If `main` contains local uncommitted changes, cannot be updated using fast-forward, or switching branches could overwrite local work, stop and report the issue instead of discarding changes.

---

## Git Safety

Do not use destructive Git operations unless explicitly requested by the user.

In particular, do not automatically use:

- `git reset --hard`
- `git clean -fd`
- `git push --force`
- `git push --force-with-lease`
- branch deletion containing unmerged work
- commands that discard local modifications
- rewriting already merged history

Never push directly to `main`.

Never automatically merge a pull request.

The expected automation boundary is:

```text
update main
    ↓
create branch
    ↓
implement
    ↓
commit
    ↓
push
    ↓
create PR when supported
    ↓
STOP FOR REVIEW
```

The user performs or explicitly approves the final merge.

---

## Commit Convention

Use Conventional Commits.

Common commit types:

- `feat:` — new functionality
- `refactor:` — structural change without changing intended behavior
- `docs:` — documentation
- `test:` — tests
- `fix:` — bug fix
- `chore:` — repository or maintenance work
- `build:` — build or dependency changes
- `ci:` — CI/CD changes
- `perf:` — performance improvements

Commit messages should be concise and describe one logical change.

Examples:

```text
feat: add vehicle creation
refactor: apply Factory Method to vehicle creation
docs: document Factory Method pattern
```

---

## Pattern Pull Request Workflow

Normally, each design-pattern PR should preserve the evolution of the implementation.

Expected sequence:

### 1. Implement the functionality without the target pattern

Example:

`feat: add vehicle creation`

The initial implementation should be a reasonable solution.

Do not intentionally write poor, artificial, or unnecessarily coupled code merely to justify introducing the pattern later.

### 2. Refactor using the selected pattern

Refactor the existing functionality using the selected GoF pattern without unnecessarily changing externally observable behavior.

Example:

`refactor: apply Factory Method to vehicle creation`

The purpose of this commit is to make the architectural change clearly visible when compared with the previous commit.

### 3. Add pattern documentation

Example:

`docs: document Factory Method pattern`

Tests may be added or updated as part of the appropriate commit or as a separate `test:` commit when justified.

---

## Commit History

Keep meaningful educational commits separate.

Do not squash the entire pattern pull request.

The history should make it easy to compare the implementation before and after introducing the pattern.

A typical final history should look like:

```text
feat: add route planning
refactor: apply Strategy to route planning
docs: document Strategy pattern
```

Temporary development commits are acceptable during implementation, for example:

- `fix`
- `wip`
- `fix again`
- `oops`
- temporary Codex corrections

Before the pull request is considered ready, clean these up using fixup, squash, or interactive rebase so that the final PR contains a small number of meaningful commits.

Do not rewrite already merged history.

---

## Pull Requests

One GoF pattern should normally correspond to:

**one branch = one pull request = one pattern**

PR titles should follow:

`[Pattern] <Pattern Name> — <Feature>`

Examples:

- `[Pattern] Factory Method — Vehicle Creation`
- `[Pattern] Strategy — Route Planning`
- `[Pattern] State — Delivery Lifecycle`

The PR description should contain at least:

### Summary

Briefly describe the functionality introduced and the pattern applied.

### Changes

Summarize the important implementation changes.

### Pattern

Name the GoF pattern introduced by the pull request.

### Learning Goal

Explain what architectural problem this PR demonstrates and what should be learned by comparing the implementation before and after the refactoring.

Do not duplicate the full pattern documentation in the PR description.

Detailed pattern explanations belong under `docs/patterns/`.

---

## Merge Policy

Pattern pull requests should normally be merged into `main` using a regular merge commit.

Do not squash-merge pattern pull requests because the before/after commits are intentionally part of the educational history.

Do not automatically merge pull requests.

A pull request should be left ready for manual review and merge.

After a PR has been merged, the next pattern task should start from the updated `main`.

Typical next-task setup:

```bash
git switch main
git pull --ff-only
git switch -c pattern/<pattern>/<feature>
```

The merged branch may be deleted when appropriate, but do not delete branches containing unmerged work.

---

## Pattern Documentation

Each implemented pattern should have a dedicated Markdown file under:

`docs/patterns/`

Use filenames such as:

- `factory-method.md`
- `strategy.md`
- `state.md`
- `observer.md`

Each document should normally contain:

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

Include a Mermaid diagram when it improves understanding.

Explicitly mention the SmartCity classes participating in the pattern.

The documentation should describe both:

1. what the pattern means in general;
2. how and why it is used specifically in SmartCity.

---

## Tests

Tests should verify behavior rather than the existence of a design pattern.

Where possible, the feature commit should establish the expected behavior before the pattern is introduced.

The refactoring commit should preserve that behavior.

Additional tests may be introduced when the pattern enables or requires additional scenarios.

Avoid tests that only assert implementation details such as the existence of a particular interface or class unless there is a concrete architectural reason.

---

## Verification

Before considering a code task complete:

- run `dotnet build SmartCity.sln`;
- run `dotnet test SmartCity.sln`;
- fix failures caused by the changes;
- inspect `git status`;
- inspect the final commit history when commits were created;
- ensure no temporary debugging code or accidental files remain;
- ensure the working tree is clean when the task includes committing changes.

If branch operations were performed, also verify:

```bash
git branch --show-current
git status
```

If the task includes pushing, verify that the branch has the expected upstream remote branch.

---

## Design Guidelines

Avoid introducing abstractions only because they might be useful in future pattern exercises.

Prefer the simplest reasonable implementation for the current task.

A pattern should solve a concrete design problem in the current SmartCity functionality.

Do not introduce a pattern merely because it still needs to be demonstrated somewhere in the project.

The goal is to understand:

- what problem the pattern solves;
- what the implementation looks like before the pattern;
- how the structure changes after applying it;
- what advantages it provides;
- what complexity it introduces;
- when the pattern would be unnecessary.

---

## Codex Responsibility Boundary

Codex may, when supported by the environment:

- inspect repository state;
- update local `main`;
- create a correctly named working branch;
- implement requested changes;
- run builds and tests;
- create meaningful commits;
- clean up temporary local commits before review;
- push the working branch;
- create a pull request.

Codex must not:

- push directly to `main`;
- automatically merge a pull request;
- discard unknown local changes;
- force-push without explicit user approval;
- rewrite already merged history;
- delete branches containing unmerged work.

The expected end state of a pattern task is:

**branch pushed and pull request ready for human review.**
