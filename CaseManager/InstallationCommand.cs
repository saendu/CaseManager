namespace CaseManager
{
    public static class MyInstallCommand {
        public static void Install()
        {
            var cloudCase = new Case(new CloudScope(), new CloudHandler());
            var onPremCase = new Case(new OnPremScope(), new OnPremHandler());
            var newCustomerCase = new Case(new FreshTenantScope(), new FreshTenantHandler());
            
            var caseManager = new global::CaseManager.CaseManager (
                cloudCase,
                onPremCase,
                newCustomerCase
            );

            caseManager.ExecuteInScopeCases();
        }
    }

    public class CloudScope : BaseScope, IScopeDefinition {
        
        // TODO: fix scope definition logic
        // TODO: ugly _scopeDefinition.IsInScope
        public bool IsInScope {
            get 
            { 
                
                return // what defines my case being in scope
                    this.IsOnline && 
                    this.IsListExisting;
            }
        }
    }

    public class CloudHandler: ICaseHandler {
        void ICaseHandler.Execute()
        {
            // actual implementation of case
            return;
        }
    }

    public class OnPremScope : BaseScope, IScopeDefinition {
        
        // TODO: fix scope definition logic
        // TODO: ugly _scopeDefinition.IsInScope
        public bool IsInScope {
            get 
            { 
                
                return // what defines my case being in scope
                    this.IsOnline && 
                    this.IsListExisting;
            }
        }
    }
    
    public class OnPremHandler: ICaseHandler {
        void ICaseHandler.Execute()
        {
            // actual implementation of case
            return;
        }
    }

    
    public class FreshTenantScope : BaseScope, IScopeDefinition {
        
        // TODO: fix scope definition logic
        // TODO: ugly _scopeDefinition.IsInScope
        public bool IsInScope {
            get 
            { 
                
                return // what defines my case being in scope
                    this.IsOnline && 
                    this.IsListExisting;
            }
        }
    }
    

    public class FreshTenantHandler: ICaseHandler {
        void ICaseHandler.Execute()
        {
            // actual implementation of case
            return;
        }
    }

    public class BaseScope {
        protected bool IsOnline { get; set; }
        protected bool IsListExisting { get; set; }
        protected bool IsListInOldSchema { get; set; }
        protected bool HasListConent { get; set; }
        public BaseScope() {
            
            // properties needed to define scope
            this.IsOnline = true == true ? true : false; // reuse stuff e.g. Utilities.IsSPOnline()
            this.IsListExisting = true == true ? true : false; 
            this.IsListInOldSchema = true == true ? true : false; //HasListOldSchema()
            this.HasListConent = true == true ? true : false;
        }
    }
}