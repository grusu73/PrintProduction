# PrintProduction — Internship Worksheet

**Day 1: HR Onboarding, VS Code and Project Specifications**

---

## 1. Today's learning objectives

Today you will get oriented with the internship, prepare your development environment and understand the problem PrintProduction is intended to solve.

By the end of the day, you should be able to:

- Identify the people, communication channels and resources that support your internship.
- Open the project workspace in Visual Studio Code and confirm the required tools are available.
- Explain the purpose and initial scope of PrintProduction.
- Distinguish a customer requirement from a resource used to fulfill it.
- Describe the main domain concepts and their relationships without writing code.
- Record assumptions, open questions and simple acceptance criteria.

Today's focus is orientation and understanding. You are not expected to implement the domain model yet.

## 2. Exercise 1 — HR and team onboarding

**Estimated time: 45 minutes**

### Background

An internship starts with understanding how to work safely and effectively with the people and organization around you. Company procedures vary, so confirm details with HR or your mentor instead of assuming a policy.

### Task 1.1 — Meet the people supporting your internship

With your HR contact or mentor, identify:

- Your mentor and the person to contact if they are unavailable.
- The team you are joining and how your work fits into it.
- The preferred channels for questions, updates and urgent issues.
- The expected working hours, schedule and process for reporting an absence.
- How to request feedback and how often progress is reviewed.

Write down names, roles and approved contact methods in your own onboarding notes. Do not put passwords, access tokens or other secrets in project files.

### Task 1.2 — Confirm workplace and information-handling guidance

Ask HR or your mentor where to find the applicable guidance for:

- Workplace safety, building access and emergency procedures.
- Acceptable use of company devices, accounts and software.
- Confidential information and approved storage locations.
- Handling customer or production data, if it is made available to you.
- Requesting access to systems, repositories or documentation.

Record where the guidance is located and who can answer questions. Follow the organization's actual policies; this worksheet does not replace them.

### Questions

1. Who is your first point of contact for project questions?
2. Which channel should you use for routine updates, and which for urgent issues?
3. Where can you find the organization's safety and information-handling guidance?
4. What should you do if you are unsure whether information can be shared or stored?

**Expected result:** You know how to get help, where to find the relevant guidance and how to request project access.

## 3. Exercise 2 — Prepare the VS Code environment

**Estimated time: 60 minutes**

### Background

Visual Studio Code is the editor used for the PrintProduction exercises. The .NET SDK provides the command-line tools used to create, build and run the projects on Day 2.

### Task 2.1 — Open the workspace

1. Open Visual Studio Code.
2. Open the `Project_David` workspace folder.
3. Open the integrated terminal using **Terminal > New Terminal**.
4. Confirm that the `Docs` folder and `print_production_requirements.md` are visible in the Explorer.

### Task 2.2 — Check the development tools

In the integrated terminal, run:

```bash
dotnet --info
dotnet --list-sdks
git --version
```

Confirm that the .NET SDK is installed and note the version or any error to discuss with your mentor. Git is checked here so setup issues can be resolved before Day 2; repository initialization is part of Day 2.

### Task 2.3 — Check C# editor support

Open the Extensions view and confirm that the C# tooling recommended by your mentor or project setup is installed and enabled. Open one existing `.cs` file, if available, and check that C# syntax highlighting appears.

If a tool or extension is missing, record the name and ask your mentor or IT contact before installing software that requires approval. Do not change machine-wide settings or install unapproved tools.

### Questions

1. What is the difference between Visual Studio Code and the .NET SDK?
2. What does the integrated terminal let you do?
3. Which SDK version is installed on your machine?
4. Who should you contact if you do not have permission to install a required tool?

**Expected result:** The workspace opens in VS Code, C# files are recognized and the required tool versions or setup issues are recorded.

## 4. Exercise 3 — Read the PrintProduction project specification

**Estimated time: 75 minutes**

### Background

PrintProduction is a small domain model for planning industrial printing work. The project specification is in [`Docs/print_production_requirements.md`](print_production_requirements.md).

The initial model describes:

- **Printer** and specialized printer types as production resources.
- **Media** and specialized roll or sheet media as consumable resources.
- **PrintJob** as a description of what a customer wants produced.
- **ProductionPlan** as the decision that connects a job with a printer and media.

A job describes the requested result. A printer and media describe available resources. A production plan connects the request to selected resources and records production estimates and status.

### Task 3.1 — Summarize the problem and project boundary

Read [`print_production_requirements.md`](print_production_requirements.md), then write a short project brief in your own words. Include:

- The problem the system is intended to help with.
- The information a user needs to record about a printer, media item and print job.
- The role of a production plan.
- One thing that belongs in the initial project scope.
- One advanced topic that the requirements explicitly defer, such as detailed JDF support, imposition or complex finishing chains.

Do not treat details that are not specified as settled requirements. Mark them as questions or assumptions for your mentor.

### Task 3.2 — Build an initial domain glossary

Complete this table using the project specification:

| Concept | What it represents | Example information |
|---|---|---|
| Printer | A machine available for production | Manufacturer, model, capabilities |
| ContinuousFeedPrinter | A printer that uses a roll or web | Supported web width, speed |
| SheetFedPrinter | A printer that uses individual sheets | Supported sheet size, speed |
| Media | Material used by a printer | Name, grammage, cost |
| RollMedia | Media supplied from a roll | Width, length, cost per meter |
| SheetMedia | Media supplied as sheets | Width, height, sheet count, cost per sheet |
| PrintJob | The customer's requested product | Copies, pages, page size, color and sides |
| ProductionPlan | A proposed way to produce a job | Selected printer and media, estimates, status |

Add or correct descriptions based on the requirements. You do not need to define C# classes or write code today.

### Task 3.3 — Sketch relationships and trace one example

Draw a simple diagram or write a short explanation showing that:

- A `PrintJob` has a media requirement and may have finishing requirements.
- A `ProductionPlan` connects the job to a selected `Printer` and `Media`.
- The plan can record quantities, estimates and a production status.

Use this example to trace the concepts:

> A customer requests 5,000 copies of a 24-page, A4, color brochure printed duplex on 120 gsm coated media. A production plan selects a compatible printer and a specific media item.

List what the specification tells you and what still needs to be decided. For example, it identifies the requested product and media characteristics, but does not yet define how printer compatibility or the exact media quantity is calculated.

### Task 3.4 — Write initial requirements and open questions

Write at least three requirements in a way that can be checked. For example:

- A print job records the requested number of copies and pages.
- A production plan refers to one print job, one printer and one media item.
- A roll media item records its width and length.

For each requirement, write a simple acceptance criterion that describes observable evidence. Then record at least three questions or decisions that need clarification, such as identifier format, validation rules, supported units or estimation formulas.

### Questions

1. Why should a `PrintJob` describe the requested product rather than a specific printer?
2. What does `ProductionPlan` connect?
3. What is the difference between a requirement and a resource?
4. Why should unknown behavior be recorded as an open question instead of silently assumed?
5. Which details are deliberately outside the initial project scope?

**Expected result:** A short project brief, a glossary, a relationship sketch, checkable requirements and a list of open questions.

## 5. Day 1 handoff

Bring these notes to Day 2:

- HR and team contact information, plus locations of relevant company guidance.
- Development tool versions and any setup issues that need help.
- Your project brief and domain glossary.
- Your relationship sketch and initial requirements.
- Open questions and assumptions to review with your mentor.

Keep onboarding notes in an approved location. Do not add confidential HR information, credentials or customer data to the source repository.

## 6. Final reflection

Answer these questions in your own words:

1. What is PrintProduction intended to help represent or plan?
2. How are a customer request and a production resource different?
3. Why does a production plan need to connect jobs, printers and media?
4. Which part of the development environment was unfamiliar?
5. What information do you still need from your mentor or HR contact?
6. What is one requirement you can already describe clearly?

## 7. Completion checklist

- [ ] Met the relevant HR, mentor and team contacts.
- [ ] Know where to find the organization's applicable workplace and information-handling guidance.
- [ ] Opened the workspace in Visual Studio Code.
- [ ] Checked the installed .NET SDK and Git versions.
- [ ] Recorded any setup issue and identified who can help resolve it.
- [ ] Read `print_production_requirements.md`.
- [ ] Drafted a project brief and domain glossary.
- [ ] Sketched the main domain relationships.
- [ ] Wrote initial checkable requirements and open questions.
- [ ] Kept credentials and confidential onboarding information out of project files.

## 8. Looking ahead — Day 2

Tomorrow you will create the .NET solution, class library and console Playground. You will initialize Git, add a project reference and begin implementing the first `Printer` and `Media` classes.

**Remember:** Day 1 is about understanding the people, tools and problem. Day 2 turns that understanding into a working project structure.