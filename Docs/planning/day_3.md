# PrintProduction — Internship Worksheet

**Day 3: Inheritance, Enumerations and Specialized Classes**

---

## 1. Today's learning objectives

Yesterday, you created a .NET solution with two projects, learned basic Git commands, and implemented your first `Printer` and `Media` classes.

Today you will extend these classes to represent different types of printing equipment and paper.

By the end of the day, you should be able to:

- Explain inheritance and why it is useful.
- Distinguish between a base class and a derived class.
- Use the `:` syntax to inherit from another class.
- Call a base class constructor using `base(...)`.
- Understand the `protected` access modifier.
- Define and use C# enumerations (`enum`).
- Create specialized printer and media objects.
- Understand basic polymorphism.
- Commit your changes using Git.

## 2. Exercise 1 — Review and preparation

**Estimated time: 30 minutes**

### Task 1.1 — Review yesterday's work

Open the PrintProduction solution and verify that both projects build.

```bash
dotnet build
dotnet run --project playground/PrintProduction.Playground
```

Review the `Printer` and `Media` classes.

Answer these questions:

1. What is the difference between a class and an object?
2. What does a constructor do?
3. What is the purpose of a method?
4. Why does the Playground reference the `PrintProduction.Domain` library?
5. Why do we keep console output outside the library?

### Task 1.2 — Check Git

```bash
git status
git log --oneline
```

Make sure your work from Day 2 has been committed.

---

## 3. Exercise 2 — Understanding inheritance

**Estimated time: 60 minutes**

### Background

A printing company can operate different kinds of printers.

For example:

- A sheet-fed printer prints on individual sheets of paper.
- A continuous-feed printer prints on paper supplied from a roll.

Both printer types share common characteristics:

- Name
- Manufacturer
- Model
- Color printing capability

However, they also have different characteristics.

A sheet-fed printer has a maximum sheet width and height, while a continuous-feed printer has a maximum paper web width and a speed measured in meters per minute.

Instead of implementing all common properties twice, we can use **inheritance**.

A derived class inherits accessible members from a base class and can introduce additional members.

### Task 2.1 — Examine the class hierarchy

```text
              Printer
                 |
        +--------+---------+
        |                  |
  SheetFedPrinter   ContinuousFeedPrinter
```

The `Printer` class is the base class.

`SheetFedPrinter` and `ContinuousFeedPrinter` are derived classes.

### Task 2.2 — Create SheetFedPrinter

In the `Printers` folder, create `SheetFedPrinter.cs`.

```csharp
namespace PrintProduction.Domain.Printers;

public class SheetFedPrinter : Printer
{
    public double MaxSheetWidthMm { get; set; }
    public double MaxSheetHeightMm { get; set; }

    public SheetFedPrinter(
        string name,
        string manufacturer,
        double maxWidth,
        double maxHeight)
        : base(name, manufacturer)
    {
        MaxSheetWidthMm = maxWidth;
        MaxSheetHeightMm = maxHeight;
    }
}
```

The `:` after `SheetFedPrinter` indicates inheritance.

The expression `base(name, manufacturer)` calls the constructor of the `Printer` class.

### Task 2.3 — Experiment in Playground

```csharp
using PrintProduction.Domain.Printers;

SheetFedPrinter printer = new SheetFedPrinter(
    "Sheet Printer 01",
    "Canon",
    330,
    488
);

Console.WriteLine(printer.Name);
Console.WriteLine(printer.GetDescription());
Console.WriteLine(printer.MaxSheetWidthMm);
```

Notice that `Name` and `GetDescription()` are inherited from `Printer`.

### Questions

1. What is a base class?
2. What is a derived class?
3. Which properties are inherited by `SheetFedPrinter`?
4. What does `base(...)` do?
5. Why is inheritance preferable to duplicating all common properties?
6. Does every printer need a maximum sheet width?

### Important concept — protected

C# provides several access modifiers.

- `public`: Accessible from other classes.
- `private`: Accessible only inside the declaring class.
- `protected`: Accessible inside the declaring class and derived classes.

You do not need to use `protected` today. First understand what it means and why derived classes sometimes need access to members of their base class.

---

## 4. Exercise 3 — Implement ContinuousFeedPrinter

**Estimated time: 60 minutes**

You will now implement another derived class independently.

### Requirements

Create `ContinuousFeedPrinter.cs` in the `Printers` folder.

The class must inherit from `Printer`.

Add the following properties:

| Property | Type | Description |
|---|---|---|
| MinWebWidthMm | double | Minimum supported paper width |
| MaxWebWidthMm | double | Maximum supported paper width |
| MaxSpeedMetersPerMinute | double | Maximum printing speed |

Implement a constructor that initializes the inherited printer properties and the three new properties.

### Test your implementation

Create a continuous-feed printer in the Playground.

Use these example values:

- Name: Continuous Printer 01
- Manufacturer: Canon
- Minimum web width: 200 mm
- Maximum web width: 560 mm
- Maximum speed: 150 m/min

Display the printer description and its specialized properties.

### Bonus challenge

Add a method called:

```csharp
public bool SupportsWebWidth(double widthMm)
```

The method should return `true` if the requested width is within the supported range.

Test the method with widths of 180 mm, 400 mm and 600 mm.

### Questions

1. What members does `ContinuousFeedPrinter` inherit?
2. What makes it different from `SheetFedPrinter`?
3. Why is the method `SupportsWebWidth()` specific to continuous-feed printers?
4. What happens if you forget to call the base constructor?

---

## 5. Exercise 4 — Introduction to enumerations

**Estimated time: 45 minutes**

### Background

Some properties can have only a limited number of valid values.

For example, a printer may use inkjet or toner technology.

We could represent the technology as a string, but that would allow mistakes such as `"Inkjet"`, `"inkjet"` or `"InkJet"`.

C# provides enumerations to represent a fixed set of named values.

### Task 4.1 — Create an enumeration

In the `Printers` folder, create `PrintTechnology.cs`:

```csharp
namespace PrintProduction.Domain.Printers;

public enum PrintTechnology
{
    Inkjet,
    Toner
}
```

### Task 4.2 — Extend Printer

Add the following property to `Printer`:

```csharp
public PrintTechnology Technology { get; set; }
```

For today's exercise, set the technology after constructing an object:

```csharp
printer.Technology = PrintTechnology.Inkjet;
```

Display the technology in the Playground.

### Task 4.3 — Add another enum

Create a `DeviceStatus` enum containing:

- Available
- Busy
- Offline
- Maintenance

Add a `Status` property to `Printer`.

Set different statuses for your printer objects.

### Questions

1. What is an enumeration?
2. Why use an enum instead of a string?
3. Can a `PrintTechnology` property be assigned an arbitrary string?
4. What happens if you try to assign a value from `DeviceStatus` to `Technology`?
5. When might a printer change its status?

---

## 6. Exercise 5 — Extend the Media model

**Estimated time: 75 minutes**

### Background

Just as printers can be specialized, printing media can have different forms.

Two common types are:

- SheetMedia — individual sheets with width and height.
- RollMedia — continuous paper supplied from a roll.

Both share a name and paper grammage, but their dimensions and costs are different.

### Important design change

Yesterday, the `Media` class represented individual sheets.

Today we will refactor it into a general base class.

Move the `WidthMm`, `HeightMm` and `GetAreaSquareMeters()` members from `Media` into the new `SheetMedia` class, because those members describe sheets rather than all media types.

Keep common properties such as `Name` and `GrammageGsm` in `Media`.

### Task 5.1 — Refactor Media

Modify the `Media` class so that it contains:

```csharp
namespace PrintProduction.Domain.Media;

public class Media
{
    public string Name { get; set; }
    public double GrammageGsm { get; set; }

    public Media(string name, double grammageGsm)
    {
        Name = name;
        GrammageGsm = grammageGsm;
    }

    public string GetDescription()
    {
        return $"{Name}, {GrammageGsm} gsm";
    }
}
```

### Task 5.2 — Create SheetMedia

Create a class called `SheetMedia` that inherits from `Media`.

Add:

- `WidthMm`
- `HeightMm`
- `CostPerSheet`

Implement a constructor and move the `GetAreaSquareMeters()` method into this class.

Update the Playground to create the A4 and A3 examples using `SheetMedia`.

### Task 5.3 — Create RollMedia

Implement a `RollMedia` class with:

- `WidthMm`
- `LengthM`
- `CostPerMeter`

Add a constructor that initializes all properties.

In the Playground, create an example roll:

- Name: Standard Roll
- Grammage: 90 gsm
- Width: 520 mm
- Length: 2000 m
- Cost: 0.15 RON/m

Display its description and properties.

### Bonus challenge

Add a method to `RollMedia`:

```csharp
public decimal CalculateCost(double lengthM)
```

It should calculate the cost of consuming a specified length of paper.

Consider how to handle invalid lengths.

### Questions

1. Why did we move `WidthMm` and `HeightMm` out of `Media`?
2. What properties do `SheetMedia` and `RollMedia` share?
3. Why do the specialized classes have different cost properties?
4. What is refactoring?
5. Why should existing Playground examples still work after updating them?

---

## 7. Exercise 6 — Basic polymorphism

**Estimated time: 30 minutes**

### Background

A derived object can also be treated as an object of its base type.

For example:

```csharp
Printer printer = new SheetFedPrinter(
    "Sheet Printer 02",
    "Canon",
    330,
    488
);
```

The variable has type `Printer`, but the actual object is a `SheetFedPrinter`.

This is one aspect of polymorphism.

### Task 6.1 — Experiment

Create two variables of type `Printer`.

Assign a `SheetFedPrinter` object to the first and a `ContinuousFeedPrinter` object to the second.

Call `GetDescription()` on both objects.

### Questions

1. Why can a `SheetFedPrinter` be assigned to a `Printer` variable?
2. Can you access `MaxSheetWidthMm` directly through a variable declared as `Printer`?
3. Why might it be useful to treat different printer types as general printers?

You will explore this concept further when learning about collections.

---

## 8. Exercise 7 — Review and Git commit

**Estimated time: 30 minutes**

### Task 7.1 — Verify the solution

Run:

```bash
dotnet build
dotnet run --project playground/PrintProduction.Playground
```

Verify that:

- Both printer subclasses can be instantiated.
- Inherited properties and methods work.
- Printer technology and status use enums.
- SheetMedia calculates area correctly.
- RollMedia contains its specialized properties.
- All console output remains in Playground.

### Task 7.2 — Review Git changes

```bash
git status
git diff
```

Inspect the changes and create a commit:

```bash
git add .
git commit -m "Add printer and media inheritance"
```

### Task 7.3 — Update README.md

Document the new class hierarchy and explain the difference between sheet-fed and continuous-feed printers.

---

## 9. Final reflection

Answer these questions in your own words:

1. What is inheritance?
2. What is the difference between a base class and a derived class?
3. What does `base(...)` mean?
4. What is an enum and why is it useful?
5. What is polymorphism?
6. Why did we refactor the Media class?
7. What was the most difficult concept today?
8. What would you like to improve tomorrow?

---

## 10. Completion checklist

- [ ] Reviewed the Day 2 project.
- [ ] Implemented `SheetFedPrinter`.
- [ ] Implemented `ContinuousFeedPrinter`.
- [ ] Understood constructor chaining with `base(...)`.
- [ ] Created the `PrintTechnology` enum.
- [ ] Created the `DeviceStatus` enum.
- [ ] Refactored the `Media` base class.
- [ ] Implemented `SheetMedia`.
- [ ] Implemented `RollMedia`.
- [ ] Experimented with basic polymorphism.
- [ ] Built and ran the Playground successfully.
- [ ] Committed all changes to Git.

## 11. Looking ahead — Day 4

Tomorrow you will learn about **.NET collections**, especially `List<T>`.

Instead of creating separate variables for every printer and media object, you will manage collections of printers and media.

You will learn how to add, remove, search and iterate through objects.

You will also begin implementing a simple printer and media catalog.

**Remember:** Understanding why inheritance is useful is more important than memorizing its syntax.

## Learning resources

These Microsoft Learn pages provide optional explanations and examples for today's topics:

- [Classes and objects](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/classes)
- [Inheritance and base classes](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/inheritance)
- [Instance constructors and the `base` keyword](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/instance-constructors)
- [Access modifiers, including `protected`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/access-modifiers)
- [Enumeration types](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum)
- [Polymorphism](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/polymorphism)