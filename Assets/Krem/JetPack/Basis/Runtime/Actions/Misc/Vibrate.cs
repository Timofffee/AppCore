using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.JetPack.Basis.Actions
{
    [NodeGraphGroupName("Jet Pack/Basis/Misc")]
    public class Vibrate : CoreAction 
    {
        protected override bool Action()
        {
            Handheld.Vibrate();
            
            return true;
        }
    }
}