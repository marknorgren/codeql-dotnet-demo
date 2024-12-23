# justfile

# Clean and build the solution
build:
	dotnet clean
	dotnet build

# Initialize a CodeQL database
init_codeql_db:
	codeql database create codeql_db --language=csharp --command="dotnet build CodeQLDemo.sln /v:detailed" --source-root=. --overwrite

# Run CodeQL analysis
analyze:
	codeql database analyze codeql_db --format=sarif-latest --output=codeql-results.sarif codeql/csharp-queries:codeql-suites/csharp-security-and-quality.qls

# Full setup and analysis
all: build init_codeql_db analyze 