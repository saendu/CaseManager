using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseManager
{
    
    //TODO: Think of the concept
    // - do we only want to allow one case to be true?
    // - OR do we want more than one case to be true?
    
    public class Case
    {
        private readonly IScopeDefinition _scopeDefinition; 
        private readonly ICaseHandler _caseHandler;
        private static readonly object ThisLock = new object();

        public Case(IScopeDefinition scopeDefinition, ICaseHandler caseHandler)
        {
            _scopeDefinition = scopeDefinition;
            _caseHandler = caseHandler;
        }

        public bool IsInScope => _scopeDefinition.IsInScope;

        public void ExecuteIfInScope() {
            if(this.IsInScope) this._caseHandler.Execute();
        }
    }

    public class CaseManager {
        private List<Case> _caseList = new List<Case>(); 

        public CaseManager(List<Case> caseList)
        {
            _caseList = caseList; 
        }
        
        public CaseManager(params Case[] args)
        {
            args.ToList().ForEach(c => {
                try
                {
                    // Maybe we want to change this to only allow one true at a time
                    // var onlyOneTrue = c.IsInScope && notLareadyTrueInList; 
                    // if(c.IsInScope) _caseList.Add(c);
                    _caseList.Add(c);
                }
                catch (Exception)
                {
                    throw new Exception("Multiple cases match current scope");
                }
                
            });
            
        }

        public async Task ExecuteInScopeCasesAsync() {
            var casesInScope = _caseList.Where(c => c.IsInScope).ToList();
            await Task.Run(() => Parallel.ForEach(casesInScope, c => c.ExecuteIfInScope())); // potential long running job
        }
    }

    public interface ICaseHandler {
        // TODO: protect this
        void Execute();
    }

    public interface IScopeDefinition {
        bool IsInScope { get; }
    }
}
