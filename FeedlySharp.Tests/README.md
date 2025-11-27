# FeedlySharp Tests

This directory contains comprehensive tests for the FeedlySharp SDK.

## Test Structure

### Unit Tests (No API Credentials Required)
- **Client_Tests.cs** - Client instantiation and configuration tests
- **Model_Tests.cs** - Model creation and property tests
- **Validation_Tests.cs** - Argument validation and null checks
- **StreamOptions_Tests.cs** - StreamOptions query string generation
- **ErrorHandling_Tests.cs** - Error and exception handling
- **FeedlyOptions_Tests.cs** - Options configuration tests
- **JsonSerialization_Tests.cs** - JSON serialization/deserialization tests

### Integration Tests (Require API Credentials)
- **Authentication_Tests.cs** - Authentication and authorization
- **Entry_Tests.cs** - Entry API operations
- **Profile_Tests.cs** - Profile API operations
- **Collection_Tests.cs** - Collection API operations
- **Stream_Tests.cs** - Stream API operations
- **Feed_Tests.cs** - Feed API operations
- **Marker_Tests.cs** - Marker API operations
- **Mix_Tests.cs** - Mix API operations
- **Preference_Tests.cs** - Preference API operations
- **Priority_Tests.cs** - Priority API operations
- **Note_Tests.cs** - Notes API operations
- **Tag_Tests.cs** - Tags API operations
- **Subscription_Tests.cs** - Subscriptions API operations
- **Search_Tests.cs** - Search API operations
- **ThreadSafety_Tests.cs** - Concurrent request handling

## Running Tests

### All Tests
```bash
dotnet test
```

### Unit Tests Only (No API Credentials Required)
```bash
dotnet test --filter "FullyQualifiedName!~Requires"
```

### Integration Tests Only (Require API Credentials)
Set the `FEEDLY_VARS` environment variable:
```bash
export FEEDLY_VARS="access_token;refresh_token;user_id"
dotnet test --filter "FullyQualifiedName~Requires"
```

## Test Coverage

- ✅ All API endpoints have tests
- ✅ Argument validation for all methods
- ✅ Error handling and exception tests
- ✅ Model creation and serialization
- ✅ Thread safety tests
- ✅ Edge case handling

## Notes

- Tests marked with `[Fact(Skip = "Requires valid API credentials")]` require valid Feedly API credentials
- Unit tests can run without API credentials
- Integration tests require setting the `FEEDLY_VARS` environment variable
