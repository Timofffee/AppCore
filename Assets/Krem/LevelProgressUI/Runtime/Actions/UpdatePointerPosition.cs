using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.LevelProgressUI.Components.UI;
using UnityEngine;

namespace Krem.LevelProgressUI.Actions
{
    [NodeGraphGroupName("LevelProgressUI")] 
    public class UpdatePointerPosition : CoreAction
    {
        [InjectComponent] private LevelProgressPanel _levelProgressPanel;

        private Vector2 _actualPosition;
        
        protected override bool Action()
        {
            if (_levelProgressPanel.Active == false)
            {
                return false;
            }
            
            _levelProgressPanel.UIPointers.ForEach(uiPointer =>
            {
                Vector2 actualPosition = uiPointer.RectTransform.anchoredPosition;
            
                actualPosition.x = _levelProgressPanel.GetPointerRectPosition(uiPointer.PlayerPointerLink.Transform.position);

                uiPointer.RectTransform.anchoredPosition = actualPosition;
            });
            
            return true;
        }
    }
}