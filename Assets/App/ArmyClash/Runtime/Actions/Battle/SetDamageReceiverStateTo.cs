using App.ArmyClash.Components.Battle;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;

namespace App.ArmyClash.Actions.Battle
{
    [NodeGraphGroupName("ArmyClash/Battle")] 
    public class SetDamageReceiverStateTo : CoreAction 
    {
        [ActionParameter] public bool State = true;
        
        public InputComponent<DamageReceiver> DamageReceiver;

        protected override bool Action()
        {
            DamageReceiver.Component.Active = State;
        
            return true;
        }
    }
}