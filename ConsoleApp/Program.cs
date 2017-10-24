using System;
using System.Threading;

namespace CaseManager
{
    public static class Program
    {
        public static void Main()
        {
            var cloudCase = new Case(new CloudScope(), new CloudHandler());
            var onPremCase = new Case(new OnPremScope(), new OnPremHandler());
            var newCustomerCase = new Case(new FreshTenantScope(), new FreshTenantHandler());

            var caseManager = new CaseManager(cloudCase, onPremCase, newCustomerCase);
            var t = caseManager.ExecuteInScopeCasesAsync();
            Thread.Sleep(5000);
        }
    }

    public class CloudScope : BaseScope, IScopeDefinition
    {
        public bool IsInScope
        {
            get
            {

                return // what defines my case being in scope
                    this.IsOnline &&
                    this.IsListExisting;
            }
        }
    }

    public class CloudHandler : ICaseHandler
    {
        void ICaseHandler.Execute()
        {
            // actual implementation of case
            Thread.Sleep(3000);
            Console.WriteLine("Cloud Case executed.");
            return;
        }
    }

    public class OnPremScope : BaseScope, IScopeDefinition
    {
        public bool IsInScope
        {
            get
            {
                return // what defines my case being in scope
                    true;
            }
        }
    }

    public class OnPremHandler : ICaseHandler
    {
        void ICaseHandler.Execute()
        {
            // actual implementation of case
            Thread.Sleep(3000);
            Console.WriteLine("OnPrem Case executed.");
            return;
        }
    }


    public class FreshTenantScope : BaseScope, IScopeDefinition
    {
        public bool IsInScope
        {
            get
            {

                return // what defines my case being in scope
                    this.IsListInOldSchema &&
                    this.IsListExisting;
            }
        }
    }


    public class FreshTenantHandler : ICaseHandler
    {
        void ICaseHandler.Execute()
        {
            // actual implementation of case
            Console.WriteLine("Fresh Case executed.");
            return;
        }
    }

    public class BaseScope
    {
        protected bool IsOnline { get; set; }
        protected bool IsListExisting { get; set; }
        protected bool IsListInOldSchema { get; set; }
        protected bool HasListConent { get; set; }
        public BaseScope()
        {

            // properties needed to define scope
            this.IsOnline = true == true ? true : false; // reuse stuff e.g. Utilities.IsSPOnline()
            this.IsListExisting = true == true ? true : false;
            this.IsListInOldSchema = true == false ? true : false; //HasListOldSchema()
            this.HasListConent = true == false ? true : false;
        }
    }
}