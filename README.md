# CaseManager
The Case Manager liblary is a helper class for the CDD (case-driven development) best practice. 

## Create cases
```C#
var cloudCase = new Case(new CloudScope(), new CloudHandler());
var onPremCase = new Case(new OnPremScope(), new OnPremHandler());
var newCustomerCase = new Case(new FreshTenantScope(), new FreshTenantHandler());
```
## Use the case manager helper class
```C#
var caseManager = new CaseManager (cloudCase, onPremCase, newCustomerCase);
caseManager.ExecuteInScopeCases(); // executes handler that fits to current scope
```
