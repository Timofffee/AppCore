using App.ArmyClash.Components.Battle;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;

namespace App.ArmyClash.Actions.Battle
{
    [NodeGraphGroupName("ArmyClash/Battle")] 
    public class SetDamageDealerStateTo : CoreAction 
    {
        [ActionParameter] public bool State = true;
        
        public InputComponent<DamageDealer> DamageDealer;

        protected override bool Action()
        {
            DamageDealer.Component.Active = State;
        
            return true;
        }
    }
}