using Krem.AppCore;
using Krem.AppCore.Attributes;

namespace Krem.DrawPad.Actions
{
    [NodeGraphGroupName("DrawPad")] 
    public class ClearShadowSegment : CoreAction 
    {
        [InjectComponent] private Components.DrawPad _drawPad;
        
        protected override bool Action()
        {
            _drawPad.CurrentShadowSegment.gameObject.SetActive(false);
        
            return true;
        }
    }
}