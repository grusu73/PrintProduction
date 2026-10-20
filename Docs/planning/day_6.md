# PrintProduction — Internship Worksheet

**Day 6: JSON Serialization and Catalog Persistence**

---

## 1. Today's learning objectives

The catalogs currently manage data only while the application is running. Today you will use JSON serialization to save catalog data to files and restore it when the application starts.

By the end of the day, you should be able to:

- Explain serialization and deserialization.
- Save and load printer, media and print-job catalog data using `System.Text.Json`.
- Preserve derived printer and media types when reading JSON.
- Keep file I/O separate from domain models and catalog rules.
- Handle missing and invalid JSON files with clear, predictable behavior.
- Verify persistence by saving, restarting and loading the Playground.

Today's work addresses requirement F08. Keep the work focused on JSON persistence; production planning is not today's objective.

## 2. Exercise 1 — Review the data to persist

**Estimated time: 30 minutes**

### Task 1.1 — Review the existing solution

From the solution root, run:

```bash
dotnet build PrintProduction.sln
dotnet run --project PrintProduction.Playground/PrintProduction.Playground.csproj
```

Review `PrinterCatalog`, `MediaCatalog`, `PrintJob`, `Printer`, `SheetFedPrinter`, `ContinuousFeedPrinter`, `Media`, `SheetMedia` and `RollMedia`.

### Task 1.2 — Identify serialization concerns

Write down which data must survive a save/load round trip. Include:

- Printer and media subtype-specific properties.
- Enum values such as printer status, technology and media type.
- A print job's media requirement and finishing requirements.
- Stable IDs, which must be preserved rather than regenerated on load.

Consider how `System.Text.Json` should represent the derived printer and media types when the catalogs expose their base types.

### Questions

1. What is the difference between serialization and deserialization?
2. Why might deserializing a `List<Printer>` lose the derived printer type unless polymorphism is configured?
3. Which classes should know about JSON files, and which should remain unaware of file paths?

## 3. Exercise 2 — Add a persistence boundary

**Estimated time: 60 minutes**

### Task 2.1 — Choose a reusable location for file I/O

Keep JSON file operations out of `PrinterCatalog`, `MediaCatalog` and the domain models. Add a small persistence layer that references `PrintProduction.Domain` and can be used by both the Playground and the WinForms application planned for later this week.

For this worksheet, use a `PrintProduction.Persistence` class library with a focused JSON storage class or repository. Do not add a database or a general-purpose framework.

### Task 2.2 — Define save/load behavior

Implement clear operations for writing and reading the printer, media and print-job data. The storage API may work with catalog types or plain collections, as long as the catalogs remain responsible for catalog rules.

Decide and document:

- Where the application data files are stored. Prefer an application data directory rather than the current working directory.
- Whether missing files mean an empty catalog on first run.
- How malformed or unreadable files are reported. Do not silently replace invalid data with an empty catalog.
- Whether files are written in an easy-to-inspect, indented JSON format.

If the solution does not yet have a print-job catalog, add the smallest catalog needed to preserve and retrieve jobs; keep its behavior consistent with the other catalogs.

### Task 2.3 — Configure JSON serialization

Use `System.Text.Json`. Configure polymorphic serialization/deserialization so that sheet-fed and continuous-feed printers, and sheet and roll media, are restored as their original derived types.

Keep JSON configuration in the persistence layer. Avoid adding file paths or serializer options to domain model classes.

## 4. Exercise 3 — Demonstrate persistence in Playground

**Estimated time: 90 minutes**

Update the Playground to demonstrate the complete save/load flow:

1. Create representative printer, media and print-job entries, including both printer types and both media types.
2. Save the data to the configured application data location.
3. Load it into catalogs on a subsequent application run.
4. Display loaded entries and verify subtype-specific properties and IDs.
5. Demonstrate the first-run behavior when a file does not yet exist.
6. Demonstrate how an invalid JSON file is reported without silently erasing or replacing it.

The Playground should display errors and results; the persistence layer should perform file operations and communicate failures explicitly.

## 5. Exercise 4 — Verify, review and document

**Estimated time: 45 minutes**

### Task 4.1 — Verify the solution

Run:

```bash
dotnet build PrintProduction.sln
dotnet run --project PrintProduction.Playground/PrintProduction.Playground.csproj
```

Check that:

- Saved JSON contains the expected catalog data.
- A second run loads the same IDs, values, enum values and derived types.
- Missing files follow the documented first-run behavior.
- Invalid JSON produces an understandable error and is not treated as a successful empty load.
- Domain catalogs and models do not perform file I/O.

### Task 4.2 — Review and commit

```bash
git status
git diff
```

Review the changes and commit them with a message such as:

```bash
git add .
git commit -m "Add JSON catalog persistence"
```

### Task 4.3 — Update README.md

Document where catalog data is stored, which data is persisted, how to run the persistence demonstration, and how missing or invalid files are handled. Do not add real customer data or secrets to the repository.

## 6. Final reflection

1. What information would have been lost if a derived object had loaded as only its base type?
2. Why is file I/O kept outside the domain catalogs?
3. How did you verify the data survived an application restart?
4. What should the application tell a user when a JSON file is corrupt?

## 7. Completion checklist

- [ ] Reviewed the domain data and serialization requirements.
- [ ] Added a reusable persistence boundary outside the domain models/catalogs.
- [ ] Saved and loaded printers, media and print jobs using JSON.
- [ ] Preserved printer/media derived types and stable IDs.
- [ ] Demonstrated successful persistence and missing/invalid-file behavior.
- [ ] Built and ran the solution.
- [ ] Updated README.md and committed the work.

## 8. Looking ahead — Day 7

Tomorrow you will create the separate `PrintProduction.WinForms` project and build the first desktop application shell. The UI will reuse the domain and persistence projects rather than putting business rules in forms.

## 9. Learning resources

- [How to serialize and deserialize JSON in .NET](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/overview)
- [Polymorphic serialization in System.Text.Json](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/polymorphism)
