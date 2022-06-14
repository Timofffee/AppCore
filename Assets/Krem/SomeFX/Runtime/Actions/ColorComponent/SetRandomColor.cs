using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.SomeFX.Actions.ColorComponent
{
    [NodeGraphGroupName("Some FX/ColorComponent")] 
    public class SetRandomColor : CoreAction
    {
        public InputComponent<Components.ColorComponent> ColorComponent;
        
        [ActionParameter] public Vector2 HueRange = Vector2.zero;
        [ActionParameter] public Vector2 SaturationRange = Vector2.zero;
        [ActionParameter] public Vector2 LightnessRange = Vector2.zero;
        
        protected override bool Action()
        {
            ColorComponent.Component.color = Color.HSVToRGB(
                Random.Range(HueRange.x, HueRange.y),
                Random.Range(SaturationRange.x, SaturationRange.y),
                Random.Range(LightnessRange.x, LightnessRange.y)
            );
        
            return true;
        }
    }
}