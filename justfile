# List available commands
default:
	@just --list

# Clean up any existing CodeQL artifacts
clean:
	rm -rf codeql_db codeql-results.sarif

# Create CodeQL database
codeql-init:
	codeql database create codeql_db --language=csharp --source-root=. --overwrite

# Run basic security analysis
codeql-analyze:
	codeql database analyze codeql_db codeql/csharp-queries:codeql-suites/csharp-security-extended.qls --format=sarif-latest --output=codeql-results.sarif

# Run all steps in sequence
codeql-all: clean codeql-init codeql-analyze
	@echo "Analysis complete. Check codeql-results.sarif for findings." 