# GitHub Actions Workflows

This directory contains GitHub Actions workflows for CI/CD and package publishing.

## Workflows

### CI (`ci.yml`)
Runs on every push and pull request to main/master/develop branches.

**Features:**
- Builds the project
- Runs all tests
- Uploads test results and coverage reports
- Uses Codecov for coverage reporting

### Pull Request (`pr.yml`)
Runs on pull requests to main/master/develop branches.

**Features:**
- Cross-platform testing (Ubuntu, Windows, macOS)
- Build verification
- Test execution
- Code formatting verification

### Release (`release.yml`)
Publishes NuGet packages when:
- A tag matching `v*.*.*` is pushed (e.g., `v3.0.0`)
- Manual workflow dispatch with version input

**Features:**
- Builds and tests the project
- Creates NuGet package
- Publishes to NuGet.org
- Uploads artifacts

**Required Secrets:**
- `NUGET_API_KEY` - Your NuGet.org API key

### CodeQL Analysis (`codeql.yml`)
Security analysis using GitHub's CodeQL.

**Features:**
- Automated security scanning
- Runs on pushes, PRs, and weekly schedule
- C# code analysis

## Setting Up NuGet Publishing

1. Get your NuGet API key from https://www.nuget.org/account/apikeys
2. Add it as a secret in your GitHub repository:
   - Go to Settings → Secrets and variables → Actions
   - Click "New repository secret"
   - Name: `NUGET_API_KEY`
   - Value: Your NuGet API key
3. Create a release tag:
   ```bash
   git tag v3.0.0
   git push origin v3.0.0
   ```
   Or use the workflow dispatch feature in the Actions tab.

## Dependabot

Dependabot is configured to automatically update:
- NuGet packages (weekly)
- GitHub Actions (weekly)

See `.github/dependabot.yml` for configuration.
