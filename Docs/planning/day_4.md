# PrintProduction — Internship Worksheet

**Day 4: Collections and the Printer Catalog**

---

## 1. Today's learning objectives

The printer models from Day 3 describe individual printers. Today you will use a collection to manage several printers together.

By the end of the day, you should be able to:

- Explain why a catalog needs a collection.
- Store different printer subclasses in one `List<Printer>`.
- Implement printer catalog operations for adding, listing, retrieving, updating and removing printers.
- Search printers by name, type or manufacturer.
- Use Playground to demonstrate domain behavior without putting console input or output in the domain library.
- Use Git to review and save a focused set of changes.

Today's work corresponds to the printer catalog in requirement F05.

## 2. Exercise 1 — Review the printer models

**Estimated time: 45 minutes**

### Task 1.1 — Review Day 3

Open the solution from the solution root and run:

```bash
dotnet build
dotnet run --project PrintProduction.Playground/PrintProduction.Playground.csproj
```

Review `Printer`, `SheetFedPrinter` and `ContinuousFeedPrinter` in `PrintProduction.Domain`.

### Task 1.2 — Compare the model with the requirements

Check that the common `Printer` model has the information needed by the project requirements:

- A stable identifier that distinguishes catalog entries.
- Manufacturer and model.
- Printing technology.
- Color and duplex capabilities.
- Status.
- Any common media constraints.

Check that `SheetFedPrinter` and `ContinuousFeedPrinter` hold the dimensions and speed appropriate to each type.

If a required property is missing, add it before implementing the catalog. If you add an identifier, initialize it consistently in the base class and derived-class constructors. A catalog entry's identifier must not change during an update.

### Questions

1. Which properties belong to every printer, and which belong only to one printer type?
2. Why does a catalog need a stable identifier in addition to a display name?
3. How can one collection hold both a `SheetFedPrinter` and a `ContinuousFeedPrinter`?

## 3. Exercise 2 — Create `PrinterCatalog`

**Estimated time: 90 minutes**

### Background

A catalog is a domain object responsible for managing a group of related objects. It should work independently of the Playground or any future user interface.

### Task 2.1 — Add the catalog class

In `PrintProduction.Domain`, create a `Printers` folder if it does not already exist. Add a `PrinterCatalog` class in the printer namespace.

Store printers in a private collection:

```csharp
private readonly List<Printer> _printers = new();
```

Add the following public operations. You may adjust return types if you can explain the choice:

```csharp
bool Add(Printer printer)
IReadOnlyList<Printer> GetAll()
Printer? GetById(string id)
bool Update(Printer printer)
bool Remove(string id)
```

Use the identifier required by the model to find entries. Prevent duplicate identifiers. Return a result that lets the caller know whether an add, update or removal succeeded.

### Task 2.2 — Define the operation behavior

Before coding each method, write down what it should do:

- **Add:** add a new printer only if its identifier is not already present.
- **GetAll:** provide a read-only view of the catalog entries, for example by returning `_printers.AsReadOnly()`.
- **GetById:** return the matching printer, or `null` when it is not found.
- **Update:** replace the matching entry while preserving its identifier; report whether an entry was found.
- **Remove:** remove the matching entry; report whether it was found.

Keep these operations in `PrintProduction.Domain`. Do not use `Console.ReadLine()` or `Console.WriteLine()` in the catalog.

### Questions

1. Why is `_printers` private?
2. What should `Add` do when an identifier already exists?
3. What should `Update` do when no matching identifier exists?
4. Why should the catalog return a result for update and removal?

## 4. Exercise 3 — Search and demonstrate the catalog

**Estimated time: 90 minutes**

### Task 3.1 — Add search operations

Add operations to find printers by:

- Name or model.
- Manufacturer.
- Printer type (`SheetFedPrinter` or `ContinuousFeedPrinter`).

Text searches should ignore letter case. Type searches should use the actual derived types rather than comparing a manually typed class-name string.

You may add a general search method or separate focused methods. Keep the method names and behavior clear.

### Task 3.2 — Exercise the catalog in Playground

In `PrintProduction.Playground/Program.cs`:

1. Create a `PrinterCatalog`.
2. Add at least two printers, including one of each derived type.
3. Display all printers using a loop in Playground.
4. Retrieve one printer by identifier.
5. Update a printer and show the result.
6. Search by manufacturer and by printer type.
7. Remove a printer and show whether removal succeeded.
8. Try adding a duplicate identifier and retrieving a missing identifier.

The Playground is responsible for displaying the results. The catalog is responsible for managing the data.

### Expected result

The demonstration shows the catalog's behavior for both normal and unsuccessful operations. It does not depend on a graphical interface.

## 5. Exercise 4 — Review, commit and document

**Estimated time: 45 minutes**

### Task 4.1 — Verify the solution

Run:

```bash
dotnet build
dotnet run --project PrintProduction.Playground/PrintProduction.Playground.csproj
```

Check that:

- Both printer types can be stored in the same catalog.
- Duplicate identifiers are rejected.
- Retrieve, update and remove work for existing and missing identifiers.
- Searches find the expected printers regardless of text case.
- Console interaction remains in Playground.

### Task 4.2 — Review and commit

```bash
git status
git diff
```

Review the changes and commit them with a message such as:

```bash
git add .
git commit -m "Add printer catalog operations"
```

### Task 4.3 — Update README.md

Briefly document `PrinterCatalog`, its supported operations and how the Playground demonstrates it.

## 6. Final reflection

1. Why is a `List<Printer>` useful when there are multiple printer types?
2. How does a stable identifier help update or remove the correct printer?
3. Why should the catalog not print messages to the console?
4. Which catalog operation was most difficult to implement, and why?

## 7. Completion checklist

- [ ] Compared the printer model with requirement F01.
- [ ] Added a stable printer identifier if it was missing.
- [ ] Implemented `PrinterCatalog` in `PrintProduction.Domain`.
- [ ] Implemented add, list, retrieve, update and remove operations.
- [ ] Implemented searches by name/model, manufacturer and type.
- [ ] Demonstrated successful and unsuccessful operations in Playground.
- [ ] Built and ran the solution.
- [ ] Reviewed and committed the changes.
- [ ] Updated README.md.

## 8. Looking ahead — Day 5

Tomorrow you will apply the same catalog ideas to media. You will manage both `SheetMedia` and `RollMedia`, and filter media by type, dimensions and grammage.

## 9. Learning resources

- [Collections in C#](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/collections)
- [LINQ in C#](https://learn.microsoft.com/en-us/dotnet/csharp/linq/)
