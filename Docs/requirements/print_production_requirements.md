# PrintProduction — Project Requirements

Software Engineering Internship · David · 2 weeks · C# / .NET

## 1. Project Overview

Objective: Develop a simplified print production management system that allows users to manage printing equipment, media, and print jobs, and create basic production plans.

The project should introduce fundamental software engineering concepts through practical implementation.

Implementation approach: Features will be developed incrementally. The domain model and basic catalogs are the primary objectives. Persistence, production planning, and a graphical interface can be implemented partially or fully, depending on progress.

## 2. Functional Requirements

### F01 — Printer Domain Model

- Define a common `Printer` class with identification, manufacturer, model, printing technology, color/duplex capabilities, status, and media constraints.
- Implement `SheetFedPrinter` with supported sheet dimensions and speed in sheets/minute.
- Implement `ContinuousFeedPrinter` with supported web widths and speed in meters/minute.
- Use inheritance and enumerations to represent printer types and capabilities.

Core

### F02 — Media Domain Model

- Define a common `Media` class with identification, name, media type, and grammage.
- Implement `SheetMedia` with width, height, and cost per sheet.
- Implement `RollMedia` with width, length, and cost per meter.
- Provide basic methods for calculating media area and consumption costs.

Core

### F03 — Print Job Domain Model

- Define `PrintJob` with identification, name, number of copies, page count, and page dimensions.
- Specify color/monochrome and simplex/duplex requirements.
- Define media requirements independently of actual media catalog entries.
- Support optional finishing requirements such as folding, trimming, and binding.

Extended

### F04 — Production Plan Domain Model

- Associate a `PrintJob` with a selected `Printer` and `Media`.
- Store estimated media quantity, printing time, and cost.
- Track production status.
- Separate customer requirements from production decisions.

Extended

### F05 — Printer Catalog

- Add, list, retrieve, update, and remove printers.
- Manage both sheet-fed and continuous-feed printers.
- Search printers by name, type, or manufacturer.
- Keep catalog operations independent of the user interface.

Core

### F06 — Media Catalog

- Add, list, retrieve, update, and remove media.
- Manage sheet and roll media.
- Search and filter media by type, dimensions, or grammage.

Core

### F07 — Print Job Catalog

- Create, retrieve, update, and remove print jobs.
- List available jobs.
- Validate required job properties.

Extended

### F08 — Data Persistence

- Save printer, media, and print-job catalogs to JSON files.
- Load catalog data when the application starts.
- Handle missing or invalid files.
- Database persistence is not required.

Extended

### F09 — Compatibility Validation

- Validate whether a printer supports the selected media dimensions and grammage.
- Validate color and duplex capabilities against print-job requirements.
- Report reasons for incompatibility.

Extended

### F10 — Production Planning

- Select a print job, printer, and media.
- Validate compatibility.
- Create a production plan.
- Estimate required media quantity, printing time, and media cost.
- Optionally compare alternative configurations.

Extended

## 3. User Interface Requirements

### F11 — Desktop User Interface

Preferred technology: Windows Forms using Visual Studio.

- Create a separate `PrintProduction.WinForms` project.
- Display the printer catalog using a `DataGridView`.
- Provide forms for adding, editing, and deleting printers.
- Extend the interface to media and print jobs if time permits.
- Display validation errors.
- Keep business logic in the `PrintProduction.Domain` class library.

Optional

### F12 — Playground Application

- Maintain a separate `PrintProduction.Playground` console application.
- Use it to experiment with domain objects and methods.
- Demonstrate catalog and production planning functionality.
- Keep it independent of the graphical interface.

Core

## 4. Technical and Engineering Requirements

### T01 — Solution Architecture

The solution should support the following projects:

```
PrintProduction/
├── PrintProduction.Domain/
├── PrintProduction.Playground/
└── PrintProduction.WinForms/   (optional)
```

- The `PrintProduction.Domain` project contains domain models and business logic.
- The domain project must not depend on Playground or UI projects.
- Applications reference `PrintProduction.Domain`.
- Business logic must remain independent of presentation technology.

### T02 — Software Design

- Use classes, properties, constructors, and methods.
- Apply inheritance where appropriate.
- Use enumerations for predefined values.
- Use .NET collections for catalog management.
- Implement basic input validation.
- Follow consistent naming conventions.

### T03 — Version Control

- Use Git from the beginning.
- Commit completed changes regularly.
- Use meaningful commit messages.
- Maintain a `.gitignore` file.

### T04 — Testing

- Test functionality using Playground.
- Introduce automated unit tests if time permits.
- Test normal and invalid inputs.
- Verify calculations and compatibility rules.

### T05 — Documentation

- Maintain a README describing the project.
- Document build and execution instructions.
- Describe the solution structure.
- Provide simple examples of application usage.

## 5. Implementation Priorities

These priorities represent recommended scope, not a requirement to complete every feature.

| Priority         | Requirements                     | Expected outcome                     |
| ---------------- | -------------------------------- | ------------------------------------ |
| P1 — Essential   | F01, F02, F05, F06, F12, T01–T03 | Working domain model and catalogs    |
| P2 — Recommended | F03, F04, F07–F10, T04–T05       | Basic print production planning      |
| P3 — Optional    | F11                              | Functional desktop UI                |

## 6. Suggested Milestones

| Milestone | Focus | Schedule | Expected outcome |
| --- | --- | --- | --- |
| M1 | Foundation | Days 1–3 | Solution structure, Git, OOP, printer and media models |
| M2 | Catalogs | Days 4–5 | Collections, printer and media catalog operations |
| M3 | Production logic | Days 6–7 | Print jobs, compatibility, planning and JSON persistence |
| M4 | Application extension | Days 8–9 | WinForms UI, depending on progress |
| M5 | Demonstration | Day 10 | Testing, documentation and final presentation |

## 7. Project Completion Criteria

The project can be considered successfully completed when:

- The PrintProduction solution builds without errors.
- The library contains working printer and media models.
- Printer and media catalogs support basic operations.
- The Playground demonstrates the implemented functionality.
- The source code is committed to Git.
- David can explain the architecture and key implementation decisions.

The WinForms interface is a stretch goal, not a mandatory completion criterion.

This is the requirements baseline I would use for David. It keeps the project realistic for two weeks while providing enough scope for further development if he progresses quickly.