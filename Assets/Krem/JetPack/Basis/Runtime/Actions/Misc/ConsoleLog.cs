using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.JetPack.Basis.Actions
{
    [NodeGraphGroupName("Jet Pack/Basis/Misc")]
    public class ConsoleLog : CoreAction
    {
        [ActionParameter] public string ConsoleText = "";

        protected override bool Action()
        {
            Debug.Log(ConsoleText);
            
            return true;
        }
    }
}
