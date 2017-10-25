# CaseManager
The Case Manager liblary is a helper class for the CDD (case-driven development) based on the strategy design pattern.

It allows you to structure your code in cases that work as a strategy implementation. It contains of two things:
- Scope:
The scope decides if the current case is in scope and if the handler should be executed. 
- Handler:
The handler is the concrete implementation of a current scope.

## Create cases
```C#
var cloudCase = new Case(new CloudScope(), new CloudHandler());
var onPremCase = new Case(new OnPremScope(), new OnPremHandler());
var newCustomerCase = new Case(new FreshTenantScope(), new FreshTenantHandler());
```
## Case manager
```C#
var caseManager = new CaseManager (cloudCase, onPremCase, newCustomerCase);
caseManager.ExecuteInScopeCases(); // executes handler that are in scope
```
