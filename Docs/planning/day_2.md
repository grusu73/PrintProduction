# PrintProduction — Internship Worksheet

**Day 2: Project Scaffolding, Git and Introduction to OOP**

---

## 1. Today's learning objectives

Today you will create the foundation of a small software system for managing industrial printing.

By the end of the day, you should be able to:

- Explain the difference between a .NET solution and a project.
- Create a class library and a console application.
- Understand how one project references another.
- Initialize a Git repository and create commits.
- Explain the difference between a class and an object.
- Define properties, constructors and methods.
- Create objects in the Playground using classes from the library.
- Understand why reusable code should be separated from application-specific code.

You are not expected to know all these concepts before starting. The purpose of today's exercises is to learn them through practical examples.

## 2. Exercise 1 — Create the solution

**Estimated time: 60 minutes**

### Background

The PrintProduction application will eventually manage printers, printing media, customer print jobs and production plans.

Instead of putting all the code into one application, we will separate it into two projects.

**PrintProduction.Domain** is a class library containing reusable classes and business logic.

**PrintProduction.Playground** is a console application where you can create objects, experiment with the library and display results.

The Playground depends on the library, but the library must not depend on the Playground.

### Task 1.1 — Create the solution

Open a terminal and execute:

```bash
mkdir PrintProduction
cd PrintProduction
dotnet new sln -n PrintProduction
```

Depending on the installed .NET SDK, the solution file may have the extension `.sln` or `.slnx`.

### Task 1.2 — Create the class library

```bash
dotnet new classlib -n PrintProduction.Domain -o PrintProduction.Domain
```

### Task 1.3 — Create the Playground

```bash
dotnet new console -n PrintProduction.Playground -o PrintProduction.Playground
```

### Task 1.4 — Add the projects to the solution

```bash
dotnet sln add PrintProduction.Domain/PrintProduction.Domain.csproj
dotnet sln add PrintProduction.Playground/PrintProduction.Playground.csproj
```

### Task 1.5 — Add the project reference

Execute:

```bash
dotnet add PrintProduction.Playground/PrintProduction.Playground.csproj reference PrintProduction.Domain/PrintProduction.Domain.csproj
```

Inspect `PrintProduction.Playground.csproj` and locate the `ProjectReference` entry.

### Task 1.6 — Verify the setup

Execute:

```bash
dotnet build
dotnet run --project PrintProduction.Playground
```

### Questions

1. What is a .NET solution?
2. What is the difference between a class library and a console application?
3. What is the purpose of `Program.cs`?
4. What does a project reference do?
5. Why does Playground reference `PrintProduction.Domain` rather than the other way around?

**Expected result:** A solution containing two projects that build successfully.

---

## 3. Exercise 2 — Introduction to Git

**Estimated time: 45 minutes**

### Background

Git is a version control system that allows developers to track changes and save versions of their work.

A commit records a snapshot of selected changes.

### Task 2.1 — Initialize the repository

From the solution root directory:

```bash
git init
dotnet new gitignore
git status
```

Inspect `.gitignore` and identify which files should not be committed.

### Task 2.2 — Create the first commit

```bash
git add .
git commit -m "Create PrintProduction solution"
```

Inspect the history:

```bash
git log --oneline
```

### Task 2.3 — Experiment with Git

Modify the welcome message in the Playground's `Program.cs`.

Execute:

```bash
git status
git diff
```

Inspect your changes, stage them and create another commit.

### Questions

1. What is a Git repository?
2. What does `git status` show?
3. What is the difference between `git add` and `git commit`?
4. Why should commits have meaningful messages?
5. What is the purpose of `.gitignore`?

**Expected result:** At least two commits in the local repository.

---

## 4. Exercise 3 — Classes and objects

**Estimated time: 60 minutes**

### Background

Object-oriented programming organizes software around objects.

Consider a printing company with several printers. Each printer has a name, manufacturer, model and other characteristics.

A **class** defines the structure and behavior of a type of object.

An **object** is one specific instance of a class.

### Task 3.1 — Create the Printer class

Inside the `PrintProduction.Domain` library, create a folder named `Printers`.

Create a file named `Printer.cs`:

```csharp
namespace PrintProduction.Domain.Printers;

public class Printer
{
    public string Name { get; set; } = "";
    public string Manufacturer { get; set; } = "";
    public bool SupportsColor { get; set; }
}
```

### Task 3.2 — Use the class in Playground

Open `PrintProduction.Playground/Program.cs` and write:

```csharp
using PrintProduction.Domain.Printers;

Printer printer1 = new Printer();

printer1.Name = "Printer 01";
printer1.Manufacturer = "Canon";
printer1.SupportsColor = true;

Console.WriteLine(printer1.Name);
```

Create a second printer with different values.

Display the properties of both printers.

### Questions

1. What is the difference between a class and an object?
2. What does `new Printer()` do?
3. Why is the `Printer` class declared `public`?
4. What is the purpose of the `using` statement?
5. Can two objects of the same class have different property values?
6. Which project contains the `Printer` class, and which project creates the objects?

### Experiment

Change the name of the first printer.

Does this change the name of the second printer? Explain your answer.

---

## 5. Exercise 4 — Constructors and methods

**Estimated time: 90 minutes**

### Background

A constructor initializes an object when it is created.

A method defines behavior associated with an object.

### Task 4.1 — Add a constructor

Add this constructor to the `Printer` class:

```csharp
public Printer(string name, string manufacturer)
{
    Name = name;
    Manufacturer = manufacturer;
}
```

Update the Playground:

```csharp
Printer printer1 = new Printer(
    "Printer 01",
    "Canon"
);
```

Update the second printer as well.

### Task 4.2 — Add a method

Inside the `Printer` class, implement:

```csharp
public string GetDescription()
{
    return $"{Manufacturer} - {Name}";
}
```

In the Playground:

```csharp
Console.WriteLine(printer1.GetDescription());
```

Notice that `GetDescription()` returns a string instead of printing directly to the console.

The library provides information. The Playground decides how that information is displayed.

### Task 4.3 — Add printer behavior

Implement a method named `CanPrintColor()` that returns whether the printer supports color printing.

Call this method from the Playground and display the result.

### Questions

1. What is a constructor?
2. When is a constructor called?
3. What is the difference between a property and a method?
4. What does `return` do?
5. What is the difference between `void` and `string` as a method return type?
6. Why is it useful to keep `Console.WriteLine()` in the Playground rather than the library?

### Challenge

Add a `Model` property to `Printer`.

Modify the constructor to initialize it and update `GetDescription()` to include the model.

---

## 6. Exercise 5 — Independent assignment: Media

**Estimated time: 60 minutes**

You will now implement a second class without copying the entire solution from the previous exercise.

### Requirements

In the `PrintProduction.Domain` library, create a `Media` folder and a class named `Media`.

The class must contain:

| Property | C# type |
|---|---|
| Name | string |
| WidthMm | double |
| HeightMm | double |
| GrammageGsm | double |

Implement:

- A constructor that initializes all four properties.
- A method `GetDescription()` that returns a string describing the media.
- A method `GetAreaSquareMeters()` that returns the area of one sheet in square meters.

### Test your implementation

In the Playground, create the following objects:

**A4 Standard**

- Width: 210 mm
- Height: 297 mm
- Grammage: 80 gsm

**A3 Coated**

- Width: 297 mm
- Height: 420 mm
- Grammage: 120 gsm

Display their descriptions and calculated areas.

### Expected calculations

| Media | Area |
|---|---|
| A4 | 0.06237 m² |
| A3 | 0.12474 m² |

### Bonus challenge

Implement a method:

```csharp
public bool IsLargerThan(Media other)
```

The method should return `true` when the current media has a larger area than the media passed as a parameter.

Test it from the Playground.

### Questions

1. What is the purpose of the `other` parameter?
2. Why does `GetAreaSquareMeters()` return a `double`?
3. How can the Playground use a class from another project?
4. Why does the `Media` class not need to know anything about the console application?

---

## 7. Exercise 6 — Review and commit

**Estimated time: 45 minutes**

### Task 6.1 — Review your implementation

Check that:

- Both projects build successfully.
- The Playground can create objects from the library.
- The `Printer` class contains properties, a constructor and methods.
- The `Media` class works correctly.
- Media areas are calculated correctly.
- No console interaction is required inside the library.

### Task 6.2 — Commit the changes

```bash
git status
git diff
git add .
git commit -m "Implement Printer and Media classes"
```

### Task 6.3 — Update README.md

Document:

- The purpose of PrintProduction.
- The two projects and their responsibilities.
- How to build the solution.
- How to run the Playground.
- The classes implemented so far.

---

## 8. Final reflection

Answer these questions in your own words.

1. What is the difference between a class library and a console application?
2. What is the difference between a class and an object?
3. What is the purpose of a constructor?
4. What is the difference between a property and a method?
5. Why do we separate reusable code from the Playground?
6. What is a project reference?
7. What is the purpose of Git commits?
8. What was the most difficult concept today?
9. What would you like to improve tomorrow?

---

## 9. Completion checklist

- [ ] Created the PrintProduction solution.
- [ ] Created the `PrintProduction.Domain` class library.
- [ ] Created the PrintProduction.Playground console application.
- [ ] Added the project reference.
- [ ] Built and ran the solution successfully.
- [ ] Initialized Git and created commits.
- [ ] Implemented the Printer class.
- [ ] Created Printer objects in the Playground.
- [ ] Added constructors and methods.
- [ ] Implemented the Media class independently.
- [ ] Verified the area calculations.
- [ ] Updated README.md and committed the changes.

## 10. Looking ahead — Day 3

Tomorrow you will extend the domain model with two specialized printer types:

- `ContinuousFeedPrinter`
- `SheetFedPrinter`

You will learn about inheritance and enumerations and understand how specialized classes can reuse properties and behavior from a common base class.

You will also explore the difference between roll media and sheet media.

**Remember:** The objective is to understand the code you write, not simply to make the application compile.

## 11. Learning resources

### .NET solutions and projects

- [Create a .NET class library](https://learn.microsoft.com/en-us/dotnet/core/tutorials/library-with-visual-studio-code) — create a reusable library and use it from an application.
- [.NET CLI reference](https://learn.microsoft.com/en-us/dotnet/core/tools/) — reference for commands such as `dotnet new`, `dotnet sln`, `dotnet add reference` and `dotnet build`.

### Git

- [Git tutorial](https://git-scm.com/docs/gittutorial) — repositories, staging, commits and inspecting changes.
- [gitignore documentation](https://git-scm.com/docs/gitignore) — patterns for excluding generated or local files from version control.

### C# classes and members

- [Classes](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/classes) — classes, objects and creating instances.
- [Properties](https://learn.microsoft.com/en-us/dotnet/csharp/properties) — declaring and using properties.
- [Methods](https://learn.microsoft.com/en-us/dotnet/csharp/methods) — parameters, return values and method calls.
- [Constructors](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/constructors) — initializing new objects.