# CodeQL .NET Demo

This project demonstrates how to use CodeQL to analyze a .NET codebase for security and quality issues.

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [CodeQL CLI](https://github.com/github/codeql-cli-binaries/releases)

## Setup Instructions

1. **Install CodeQL**: Download and install CodeQL from the official GitHub repository. Ensure that the `codeql` command is available in your PATH.

2. **Create the Solution and Projects**: Run the following command to create the .NET solution and projects:
   ```bash
   just create_solution
   ```

3. **Build the Solution**: Build the solution using:
   ```bash
   just build
   ```

4. **Initialize a CodeQL Database**: Create a CodeQL database with:
   ```bash
   just init_codeql_db
   ```

5. **Run CodeQL Analysis**: Analyze the database using:
   ```bash
   just analyze
   ```

6. **Full Setup and Analysis**: To perform all the above steps in one go, run:
   ```bash
   just all
   ```

## Interpreting Results

- Open the `codeql-results.sarif` file to view the analysis results.
- Look for security and quality alerts and address them accordingly. 