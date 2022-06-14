using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.RocketFactory.Components;

namespace Krem.RocketFactory.Actions
{
    [NodeGraphGroupName("Rocket Factory")] 
    public class SetRocketActiveStateTo : CoreAction
    {
        [ActionParameter] public bool state = false;
        
        [InjectComponent] private Rocket _rocket;
        protected override bool Action()
        {
            _rocket.Active = state;
        
            return true;
        }
    }
}