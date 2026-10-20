# PrintProduction — Internship Worksheet

**Day 5: Media Catalogs and Filtering**

---

## 1. Today's learning objectives

Yesterday you created a printer catalog that stores several printer types and supports basic catalog operations. Today you will apply the same ideas to printing media.

By the end of the day, you should be able to:

- Explain how a base-type collection can store different derived media types.
- Implement a media catalog for adding, listing, retrieving, updating and removing media.
- Filter sheet and roll media by type and dimensions.
- Find media within a grammage range.
- Demonstrate printer and media catalogs together in Playground.
- Review and document the first five days of project work.

Today's work completes the catalog milestone in requirements F05 and F06.

## 2. Exercise 1 — Review the media models

**Estimated time: 45 minutes**

### Task 1.1 — Review Day 3

From the solution root, run:

```bash
dotnet build
dotnet run --project PrintProduction.Playground/PrintProduction.Playground.csproj
```

Review `Media`, `SheetMedia` and `RollMedia` in `PrintProduction.Domain`.

### Task 1.2 — Compare the model with the requirements

Check that the common `Media` model has a stable identifier, a name and grammage. Confirm that its derived types have the appropriate dimensions and costs:

- `SheetMedia`: width, height and cost per sheet.
- `RollMedia`: width, length and cost per meter.

If the model does not have a stable identifier, add one to `Media` and initialize it consistently. The identifier must be unique within the catalog and must not change during an update.

### Questions

1. Which properties belong to all media, and which belong to sheets or rolls?
2. Why should a roll not be required to have a sheet height?
3. How does a stable identifier help the media catalog?

## 3. Exercise 2 — Create `MediaCatalog`

**Estimated time: 90 minutes**

### Task 2.1 — Add the catalog class

In `PrintProduction.Domain`, create a `MediaCatalog` class in the media namespace. Store media in a private collection using the base type:

```csharp
private readonly List<Media> _mediaItems = new();
```

Add operations with behavior equivalent to `PrinterCatalog`. For `GetAll`, return a read-only view of the entries, for example with `_mediaItems.AsReadOnly()`:

```csharp
bool Add(Media media)
IReadOnlyList<Media> GetAll()
Media? GetById(string id)
bool Update(Media media)
bool Remove(string id)
```

The catalog must accept both `SheetMedia` and `RollMedia`. Reject duplicate identifiers and report whether updates and removals succeed.

### Task 2.2 — Demonstrate CRUD operations

In Playground, create a catalog and add at least two sheet media and two roll media. Demonstrate listing, retrieval, update and removal, including an unsuccessful lookup or removal.

Keep catalog logic in `PrintProduction.Domain` and display output from Playground.

## 4. Exercise 3 — Filter and search media

**Estimated time: 90 minutes**

### Task 3.1 — Filter by media type

Add operations to retrieve:

- All sheet media.
- All roll media.

Use the actual types (`SheetMedia` and `RollMedia`) when filtering. A base collection can contain objects of either derived type.

### Task 3.2 — Filter by dimensions

Add filters that match the requirements:

- Find sheet media by width and height.
- Find roll media by width.

Use millimeters consistently. Decide whether the dimension match should be exact, and document that decision in a short README note or method description.

### Task 3.3 — Filter by grammage

Add a filter that returns media whose grammage is between a minimum and maximum value, inclusive.

Try a few ranges in Playground, including one that returns no results. Avoid adding console output or user prompts to the catalog.

### Questions

1. Why do sheet and roll filters need different dimension rules?
2. What should happen when the minimum grammage is greater than the maximum?
3. Why is it useful to test a filter that returns an empty result?
4. Which filtering behavior belongs in the catalog, and which belongs in Playground?

## 5. Exercise 4 — Combined catalog demonstration

**Estimated time: 60 minutes**

Update the Playground demonstration so it presents a small print-production inventory:

1. Create a `PrinterCatalog` with a sheet-fed printer and a continuous-feed printer.
2. Create a `MediaCatalog` with sheet media and roll media.
3. List all printers and media.
4. Search for one printer by manufacturer.
5. Show sheet media matching dimensions and roll media matching a width.
6. Show media within a grammage range.
7. Demonstrate one missing lookup and one unsuccessful removal.

Keep this as a clear demonstration, not a full interactive menu. The upcoming print-job and production-planning work will build on these catalogs.

## 6. Exercise 5 — Review, commit and document

**Estimated time: 45 minutes**

### Task 5.1 — Verify the solution

Run:

```bash
dotnet build
dotnet run --project PrintProduction.Playground/PrintProduction.Playground.csproj
```

Check that:

- Both `SheetMedia` and `RollMedia` can be stored in one catalog.
- Add, list, retrieve, update and remove work by identifier.
- Duplicate identifiers are rejected.
- Type, dimension and grammage filters return the expected results.
- Both catalogs remain independent of the Playground and presentation technology.

### Task 5.2 — Review and commit

```bash
git status
git diff
```

Review the changes and commit them with a message such as:

```bash
git add .
git commit -m "Add media catalog and filters"
```

### Task 5.3 — Update README.md

Document:

- What `PrinterCatalog` and `MediaCatalog` manage.
- The operations and filters each catalog supports.
- How to build the solution and run the Playground.
- One example of a printer search and one media filter.

## 7. Final reflection

1. What do the printer and media catalogs have in common?
2. Why does the media catalog store the base type rather than separate lists for each media subtype?
3. What is one benefit of keeping catalog operations in the domain project?
4. Which requirement or domain rule became clearer while implementing these catalogs?
5. What would you like to understand before beginning print jobs and production planning?

## 8. Completion checklist

- [ ] Compared the media model with requirement F02.
- [ ] Added a stable media identifier if it was missing.
- [ ] Implemented `MediaCatalog` in `PrintProduction.Domain`.
- [ ] Implemented add, list, retrieve, update and remove operations.
- [ ] Implemented filters by type, dimensions and grammage.
- [ ] Demonstrated both catalogs together in Playground.
- [ ] Built and ran the solution.
- [ ] Reviewed and committed the changes.
- [ ] Updated README.md.

## 9. Looking ahead — Day 6

The next milestone introduces print jobs and production planning. You will model customer requirements separately from the printer and media selected to fulfill them.

## 10. Learning resources

- [Collections in C#](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/collections)
- [LINQ in C#](https://learn.microsoft.com/en-us/dotnet/csharp/linq/)
