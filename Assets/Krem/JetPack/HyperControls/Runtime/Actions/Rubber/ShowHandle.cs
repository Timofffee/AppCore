using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.JetPack.HyperControls.Components.Rubber;

namespace Krem.JetPack.HyperControls.Actions.Rubber
{
    [NodeGraphGroupName("Jet Pack/Hyper Controls/Rubber")]
    public class ShowHandle : CoreAction
    {
        [InjectComponent] private RubberControl _rubberControl;
        
        protected override bool Action()
        {
            _rubberControl.Handle.gameObject.SetActive(true);
        
            return true;
        }
    }
}