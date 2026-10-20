# PrintProduction — Internship Worksheet

**Day 8: WinForms Printer Management**

---

## 1. Today's learning objectives

Yesterday you created the WinForms application shell and showed the printer catalog in a grid. Today you will let users manage printers through the interface.

By the end of the day, you should be able to:

- Build a focused add/edit form for printer details.
- Support both `SheetFedPrinter` and `ContinuousFeedPrinter`.
- Connect UI actions to `PrinterCatalog` operations.
- Validate user input and show actionable validation messages.
- Persist successful changes through the persistence layer.
- Refresh the UI without making the grid the source of truth.

Today's work continues F11 and exercises F05 through the desktop interface.

## 2. Exercise 1 — Plan the printer interaction

**Estimated time: 30 minutes**

Before coding, sketch the printer list and add/edit flow. Identify:

- Fields shared by both printer types.
- Fields specific to sheet-fed and continuous-feed printers.
- Which inputs should be drop-downs, checkboxes or numeric inputs.
- How the user can tell whether a save, update or delete succeeded.

Choose a form layout that exposes only the fields relevant to the selected printer type.

## 3. Exercise 2 — Add and edit printers

**Estimated time: 90 minutes**

### Task 2.1 — Create the add/edit dialog

Add controls for common printer fields, including ID, name, manufacturer, model, technology, status, color/duplex support and media grammage limits.

Allow the user to choose a printer type. Show the matching type-specific fields:

- Sheet-fed: supported sheet dimensions and sheets-per-minute speed.
- Continuous-feed: supported web-width range, meters-per-minute speed and optional maximum roll diameter.

### Task 2.2 — Validate input

Check required text and identifier values, positive dimensions/speeds, and sensible minimum/maximum ranges. Show field-level or dialog-level messages that tell the user what needs correction.

Do not duplicate domain validation rules if the model or catalog already enforces them. Do not use silent defaults to turn invalid input into a successful save.

### Task 2.3 — Wire catalog operations

Implement:

- **Add:** reject a duplicate ID and keep the dialog open with an explanation.
- **Edit:** load the selected printer's values; preserve its stable ID.
- **Save:** update the catalog only after validation succeeds.
- **Cancel:** close without changing the catalog.

After a successful save, refresh the grid from the catalog and persist the updated data.

## 4. Exercise 3 — Delete and search printers

**Estimated time: 60 minutes**

Add a delete action that:

- Requires an explicit confirmation.
- Removes the selected printer by ID.
- Reports when no printer is selected or removal fails.
- Refreshes the grid and persists the change only after successful removal.

If time permits, add a search field using the existing catalog search behavior. Keep filtering/search logic out of the form where the catalog already provides it.

## 5. Exercise 4 — Verify, review and document

**Estimated time: 45 minutes**

Run:

```bash
dotnet build PrintProduction.sln
dotnet run --project PrintProduction.WinForms/PrintProduction.WinForms.csproj
```

Manually verify:

- Adding one printer of each type.
- Editing a printer without changing its ID.
- Rejecting duplicate IDs and invalid numeric values.
- Cancelling an edit without changing saved data.
- Confirming and cancelling deletion.
- Restarting the app and checking that successful changes persist.

Review the changes, commit with a message such as `Add WinForms printer management`, and update README.md with the supported printer actions.

## 6. Final reflection

1. How did the form make the different printer types understandable?
2. Which errors should block saving, and how did the UI communicate them?
3. Why should the grid be refreshed from catalog data after a change?

## 7. Completion checklist

- [ ] Added and edited both printer types through WinForms.
- [ ] Validated required fields, dimensions and speeds.
- [ ] Preserved IDs during edits and rejected duplicates.
- [ ] Added confirmed deletion and clear failure messages.
- [ ] Persisted only successful catalog changes.
- [ ] Built and manually tested the WinForms application.
- [ ] Updated README.md and committed the work.

## 8. Looking ahead — Day 9

Tomorrow you will extend the same UI pattern to media and print jobs. Keep the controls appropriate to each domain type and prioritize a clear, working flow over visual complexity.

## 9. Learning resources

- [Windows Forms controls](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/)
- [Data validation in Windows Forms](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/input-keyboard/validation)
