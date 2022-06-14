using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.SomeFX.Actions
{
    [NodeGraphGroupName("Some FX")] 
    public class SetForColor : CoreAction 
    {
        public InputComponent<Components.ColorComponent> ColorComponent;
        
        protected override bool Action()
        {
            RenderSettings.fogColor = ColorComponent.Component.color;
        
            return true;
        }
    }
}