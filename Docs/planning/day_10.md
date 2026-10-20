# PrintProduction — Internship Worksheet

**Day 10: WinForms Integration, Testing and Demonstration**

---

## 1. Today's learning objectives

Today you will stabilize and demonstrate the WinForms application. Focus on end-to-end behavior, understandable feedback and a clean project handoff rather than adding large new features.

By the end of the day, you should be able to:

- Verify the UI, domain and persistence layers work together.
- Test successful operations and important failure cases.
- Improve confusing labels, messages and empty/error states.
- Confirm that saved data remains available after restart.
- Document how to build and run the solution.
- Present the project's architecture and explain its main design decisions.

Today's work closes the optional desktop UI milestone in F11 and reinforces T04–T05.

## 2. Exercise 1 — Run the end-to-end checklist

**Estimated time: 90 minutes**

Build and launch the application:

```bash
dotnet build PrintProduction.sln
dotnet run --project PrintProduction.WinForms/PrintProduction.WinForms.csproj
```

Use a small set of clearly identified sample data. Avoid adding real customer information.

Test the following flows:

1. Start with no saved data and confirm the first-run state is clear.
2. Add a sheet-fed printer and a continuous-feed printer.
3. Add sheet media and roll media.
4. Add or view a print job and its customer requirements.
5. Edit records and confirm their IDs remain stable.
6. Attempt duplicate IDs, missing required fields and invalid numeric values.
7. Cancel edits and deletion confirmations and check that no data changes.
8. Confirm a deletion and verify the record is removed.
9. Close and restart the application and verify successful changes were saved.
10. Use an invalid JSON file in a safe test location and verify the failure is reported without destructive fallback behavior.

Record any issue you find, its reproduction steps and whether it blocks the main demonstration.

## 3. Exercise 2 — Improve reliability and usability

**Estimated time: 60 minutes**

Fix issues that block normal use or make the result ambiguous. Review:

- Clear action names and consistent navigation.
- Readable grid columns and sensible selection behavior.
- Validation messages that identify the correction needed.
- A visible distinction between empty data and failed loading.
- Save/load feedback that does not claim success when an operation fails.
- Keyboard focus and tab order in forms, where practical.
- No business or persistence logic duplicated in UI event handlers.

Do not add advanced production planning or a new UI framework today. Note optional follow-up work instead.

## 4. Exercise 3 — Verify build, run and repository state

**Estimated time: 45 minutes**

Run the full solution build and launch the WinForms application again after fixes. Also run the Playground to confirm its demonstration still builds and runs:

```bash
dotnet build PrintProduction.sln
dotnet run --project PrintProduction.Playground/PrintProduction.Playground.csproj
dotnet run --project PrintProduction.WinForms/PrintProduction.WinForms.csproj
```

If automated tests exist, run them and record the results. Review `git status` and the changes before committing. Do not add generated binaries or local application data to the repository.

## 5. Exercise 4 — Final documentation and presentation

**Estimated time: 60 minutes**

Ensure README.md explains:

- The purpose of Domain, Persistence, Playground and WinForms.
- How to build the solution and run each application.
- Where JSON data is stored and how load/save errors are handled.
- Which desktop views and operations are implemented.
- Any known limitations or optional features not completed.

Prepare a short demonstration:

1. Show the project structure and explain the reference direction.
2. Add or edit a printer and a media entry.
3. Show how a print job captures requirements.
4. Restart the application and show persisted data.
5. Explain one validation rule and one failure case.

Commit the completed work with a meaningful message, for example `Complete WinForms catalog workflow`.

## 6. Final reflection

1. Which layer contains business rules, persistence, and user interaction?
2. What was the most useful end-to-end test?
3. Which limitation would you address next, and why?
4. What design decision can you now explain confidently?

## 7. Completion checklist

- [ ] Built the complete solution successfully.
- [ ] Tested printer, media and job UI flows that were implemented.
- [ ] Tested invalid inputs, cancellation, deletion and persistence across restart.
- [ ] Verified load failures are visible and do not appear as successful empty loads.
- [ ] Ran the Playground and WinForms application.
- [ ] Updated README.md with setup, run instructions and limitations.
- [ ] Reviewed and committed the final work.
- [ ] Prepared and delivered a short project demonstration.

## 8. After the internship

Possible future improvements include automated unit tests, compatibility validation, production-plan calculations, richer job editing, and improved search/filtering. Choose follow-up work based on user needs and the time available.

## 9. Learning resources

- [Windows Forms overview](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/overview/)
- [System.Text.Json overview](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/overview)
