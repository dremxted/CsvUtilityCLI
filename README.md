# Sheet Record Duplicator

*Sheet Record Duplicator* is a command-line tool that automates populating a .csv template file with multiple sets of values.

### Usage:
>	`srd <template> <inputs> [options]`

### Arguments:
- `<template>` — A FullName to the template file.
- `<inputs>` — A FullName to the inputs file.

### Options:
- `-o, --output <output>` -- Sets the FullName of the output file.
- `-?, -h, --help` -- Show help and usage information
- `--version` -- Show version information

### Installation

1. Download the latest release from [Releases](https://github.com/dremxted/CsvUtilityCLI/releases).
2. Locate the .nupkg file in the terminal.
3. Run the following command in the terminal:

>	`dotnet tool install srd --global --source ./`