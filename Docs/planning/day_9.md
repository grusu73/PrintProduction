# PrintProduction — Internship Worksheet

**Day 9: WinForms Media and Print Job Views**

---

## 1. Today's learning objectives

Yesterday you implemented printer management in WinForms. Today you will extend the interface to media and print jobs while continuing to reuse the existing domain and persistence layers.

By the end of the day, you should be able to:

- Display sheet and roll media in a catalog view.
- Create and edit media using type-appropriate fields.
- Display print-job requirements without confusing them with production selections.
- Validate and persist successful changes.
- Reuse consistent navigation, feedback and form patterns.

Today's focus is the UI. Do not attempt to implement a full automated production planner.

## 2. Exercise 1 — Add the media catalog view

**Estimated time: 90 minutes**

### Task 1.1 — Display media

Add a Media view reachable from the main navigation. Display the media catalog in a `DataGridView`, including ID, name, media type, grammage and appropriate dimensions/cost information.

Present sheet-specific and roll-specific values clearly. Do not show misleading values for properties that do not apply to a media subtype.

### Task 1.2 — Add and edit media

Add an add/edit dialog that supports:

- Sheet media: width, height, optional sheets per pack and cost per sheet.
- Roll media: width, length and cost per meter.
- Shared values: ID, name, material category and grammage.

Validate required values, positive dimensions/lengths and non-negative costs. Preserve IDs during edits and reject duplicates. Display errors clearly and do not persist invalid input.

### Task 1.3 — Delete and persist media

Add confirmed deletion by ID. After successful add, update or removal, refresh the view and save through the persistence layer.

If time permits, add a simple search/filter using existing `MediaCatalog` operations, such as type or grammage range.

## 3. Exercise 2 — Add the print-job view

**Estimated time: 75 minutes**

### Task 2.1 — Display jobs

Add a Print Jobs view and display saved jobs in a grid or readable list. Include job ID, name, copies, page count, finished dimensions, color mode, print sides and a concise summary of media and finishing requirements.

Keep the job requirement side distinct from any chosen printer or media. A print job describes what is requested; it is not a production plan.

### Task 2.2 — Create or update a job

If time allows, add a focused form to create and edit print jobs, including:

- Job identity, name, copies and page dimensions.
- Color mode and simplex/duplex requirement.
- Media category, grammage and optional dimensions/preferred media ID.
- Zero or more finishing operations.

Validate required fields, positive counts and dimensions, and ensure the finishing operations can be reviewed before saving. Persist changes only after successful validation.

If a complete job editor would compromise the working media UI, finish the job list and add/edit only the essential job fields.

## 4. Exercise 3 — Verify the UI flow

**Estimated time: 45 minutes**

Run:

```bash
dotnet build PrintProduction.sln
dotnet run --project PrintProduction.WinForms/PrintProduction.WinForms.csproj
```

Verify that:

- Media and jobs are reachable from navigation.
- Sheet and roll media are displayed and edited appropriately.
- Invalid values are rejected with a useful message.
- Successful changes survive application restart.
- Job requirements are not presented as already selected production resources.
- Existing printer features still work.

## 5. Exercise 4 — Review, document and commit

Update README.md with the available WinForms views and actions. Review the changed files and commit with a message such as `Add WinForms media and job views`.

## 6. Final reflection

1. How does the UI distinguish sheet media from roll media?
2. Why are media requirements part of a job rather than a selected media catalog item?
3. Which view or form would benefit most from another usability improvement?

## 7. Completion checklist

- [ ] Added a media catalog view and `DataGridView`.
- [ ] Added media forms for sheet and roll media, with validation.
- [ ] Added confirmed media deletion and JSON persistence.
- [ ] Added a print-job list; added job editing if time permits.
- [ ] Kept print-job requirements distinct from production decisions.
- [ ] Re-tested printer management and persistence.
- [ ] Updated README.md and committed the work.

## 8. Looking ahead — Day 10

Tomorrow you will integrate the UI flows, test normal and failure cases, correct usability issues and prepare a short demonstration of the completed application.

## 9. Learning resources

- [Windows Forms controls](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/)
- [DataGridView control overview](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/datagridview-control-overview-windows-forms)
