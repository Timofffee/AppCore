using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.JetPack.ProtoElements.Components;
using UnityEngine;

namespace Krem.JetPack.ProtoElements.Actions.ProgressBar
{
    [NodeGraphGroupName("Jet Pack/Proto Elements/Progress Bar")]
    public class UpdateHorizontalProgressText : CoreAction
    {
        [InjectComponent] private HorizontalProgressBar _horizontalProgressBar;

        protected override bool Action()
        {
            int newValue = Mathf.RoundToInt(_horizontalProgressBar.ProgressValue * _horizontalProgressBar.progressTextMultiplier);
            _horizontalProgressBar.SetProgressText(newValue.ToString());

            return true;
        }
    }
}