using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.JetPack.Basis.Actions
{
    [NodeGraphGroupName("Jet Pack/Basis/Misc")] 
    public class ChangeTimescaleTo : CoreAction
    {
        [ActionParameter] public float timeScale = 1f;
        
        protected override bool Action()
        {
            Time.timeScale = timeScale;
        
            return true;
        }
    }
}