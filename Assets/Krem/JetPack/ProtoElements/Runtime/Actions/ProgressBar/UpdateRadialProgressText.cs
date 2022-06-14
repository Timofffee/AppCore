using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.JetPack.ProtoElements.Components;
using UnityEngine;

namespace Krem.JetPack.ProtoElements.Actions.ProgressBar
{
    [NodeGraphGroupName("Jet Pack/Proto Elements/Progress Bar")]
    public class UpdateRadialProgressText : CoreAction
    {
        [InjectComponent] private RadialProgressBar _progressBar;

        protected override bool Action()
        {
            int newValue = Mathf.RoundToInt(_progressBar.ProgressValue * _progressBar.progressTextMultiplier);
            _progressBar.SetProgressText(newValue.ToString());

            return true;
        }
    }
}