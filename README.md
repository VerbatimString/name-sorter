# Name Sorter

![CI](https://github.com/VerbatimString/name-sorter/actions/workflows/ci.yml/badge.svg)

A program which gets a name list separated by new lines which then sorts names by last name. If last names are matching, it iteratively compares given names (first and middle names). Finally, it prints the result and overwrites it to `sorted-names-list.txt` in the current folder.

## Run
Requires the .NET 10 SDK.
```
dotnet run --project src/NameSorter -- ./unsorted-names-list.txt
```

## Test
```
dotnet test
```

CI (GitHub Actions) builds, tests and runs the app on Linux and Windows on every push.

## Design

<img width="100%" alt="architecture" src="https://github.com/user-attachments/assets/adbba4e3-596d-4111-bd1a-0a245dc0d0e9" />
<p align="center"><em><code>NameSorterApp.Run()</code> drives the whole flow: read -> parse -> sort -> write. <code>Program</code> only creates the objects and calls <code>Run()</code>.</em></p>

- `PersonName`: 1–3 given names + a last name; invalid names can't be created.
- `PersonNameParser`: turns a line of text into a `PersonName`.
- `LastNameThenGivenNamesComparer`: the sort rule, used with the built-in `OrderBy`.
- `INameSource` / `INameWriter`: input and output behind interfaces, so new sources or outputs can be added without changing existing code, and the app can be tested without files.
- `NameSorterApp`: the main orchestrator. read -> parse -> sort -> write. `Program` wires it up and handles errors.

## Decisions
- Any line/name in the input that can't be parsed stops the program and nothing is written. Because I thought this kind of program would be written in cases like database migration so we don't want any data loss preferably.
- Case-insensitive, ordinal comparison, so results are the same on every machine.
- If all shared given names match, fewer given names comes first ('Leo Gardner' before 'Leo James Gardner').

## AI usage
I've worked with C# extensively in game development, and used Claude to get up to speed quickly on the conventions and libraries more common in application development. I also used it to draft initial versions of the unit tests from precise prompts, since they're straightforward and don't need much context. I reviewed and reworked everything, and can explain all of the code.
