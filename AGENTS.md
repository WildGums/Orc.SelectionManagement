# Orc.SelectionManagement

Orc.SelectionManagement is a library for managing selections in .NET applications. It provides a generic `SelectionManager<T>` that supports both single- and multi-select scenarios, scoped selections, and change notifications via the `SelectionChanged` event.

---

## Critical Rules (Read First)

These rules are **non-negotiable**. Violating them causes broken builds, crashes, or downstream breakage.

### 1. Never Edit Generated Files

Files matching `*.generated.cs` are auto-generated.

- **NEVER** manually edit these files

### 2. ABI / API Stability

This project maintains stable ABI / API. Breaking changes break downstream apps.

| Allowed | Never |
|---------|-------|
| Add new overloads | Modify existing signatures |
| Add new methods | Remove public APIs |
| Add new classes | Change return types |

### 3. Tests Are Mandatory

**Building alone is NOT sufficient.** Run tests before claiming completion (see [Commands](#commands)).

### 4. Branch Protection (COMPLIANCE REQUIRED)

**Direct commits to protected branches are a policy violation.**

| Repository | Protected Branches |
|------------|-------------------|
| Orc.SelectionManagement | `master` |
| Orc.SelectionManagement | `develop` |

**Required workflow:**

1. **Create a feature branch FIRST** — Use naming convention: `feature/issue-NNNN-description`
2. **Make all commits on the feature branch** — Never commit directly to protected branches
3. **Submit a Pull Request** — Changes must be reviewed by a human before merging

```bash
# CORRECT — Always create a feature branch first
git checkout -b feature/issue-1234-fix-description

# NEVER DO THIS — Policy violation
git checkout develop && git commit  # FORBIDDEN

# NEVER DO THIS — Policy violation
git checkout master && git commit  # FORBIDDEN
```

The repository has protected branches that must be respected.

---

## Commands

Single source of truth for all commands:

| Task | Command |
|------|---------|
| **Build** | `dotnet cake --target=build` |
| **Test** | `dotnet cake --target=test` |
| **Build and test** | `dotnet cake --target=buildandtest` |

---

## Architecture & Directories

### Project Overview

```
src/Orc.SelectionManagement          => Core library (selection management logic)
src/Orc.SelectionManagement.Tests    => Unit tests
src/Orc.SelectionManagement.Example  => Example / demo application
```

### Key Types

| Type | Purpose |
|------|---------|
| `SelectionManager<T>` | Generic manager for tracking selected items |
| `ISelectionManager<T>` | Interface for the selection manager |
| `SelectionList<T>` | Internal list backing a scoped selection |
| `SelectionChangedEventArgs<T>` | Event args for the `SelectionChanged` event |

### Directory Guide

| Directory | Editable? | Notes |
|-----------|-----------|-------|
| `*.generated.cs` | No | Leave as-is |
| `deployment/` | No | Deployment / build scripts |
| `src/Orc.SelectionManagement/` | Yes | Core library source |
| `src/Orc.SelectionManagement.Tests/` | Yes | Test project |

---

## Writing Code

### Anti-Patterns (Never Do This)

| Anti-Pattern | Why |
|-------------|-----|
| Modifying method signatures | ABI breaking |
| Manual edits to `*.generated.cs` | Overwritten on regenerate |
| Using default parameters in public APIs | ABI breaking |
| **Skipping failing tests** | **Unacceptable — tests must pass** |

---

## Testing & Debugging

### Running Tests

```bash
dotnet cake --target=test
```

### Tests MUST Pass

> **NON-NEGOTIABLE:** Tests must PASS before claiming completion.
>
> - Do NOT skip failing tests
> - Do NOT claim completion if tests fail
> - Do NOT use `SkipException` to work around failures

### Writing Tests

1. Use NUnit to write tests
2. Group tests in nested classes named after the method under test (e.g., `TheAddMethod`)
3. Combine Pascal / Snake case for test methods (e.g., `Feature_Does_Work`)

```csharp
[TestFixture]
public class TheAddMethod
{
    [Test]
    public void Add_Single_Item_Returns_That_Item_As_Selected()
    {
        var selectionManager = new SelectionManager<int>(NullLogger<SelectionManager<int>>.Instance);

        selectionManager.Add(new[] { 42 });

        Assert.That(selectionManager.GetSelectedItem(), Is.EqualTo(42));
    }
}
```

**Philosophy:** Tests FAIL when wrong, never skip (except missing hardware).

### Debugging Methodology

1. **Establish baseline** — What's the known-good state?
2. **One change at a time** — Verify each change before proceeding
3. **Track changes in a table** — Log what you changed and the result
4. **Platform differences are signals** — If X works and Y fails, the difference IS the answer
5. **Revert if worse** — Don't pile fixes on top of failures

---

## Further Reading

| Topic | Document |
|-------|----------|
| Contributing guidelines | [CONTRIBUTING.md](CONTRIBUTING.md) |
| Documentation portal | https://opensource.wildgums.com |
