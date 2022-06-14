using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.LevelProgressUI.Components;
using Krem.LevelProgressUI.Components.UI;

namespace Krem.LevelProgressUI.Actions
{
    [NodeGraphGroupName("LevelProgressUI")] 
    public class ChangeLevelProgressPanelActiveStateTo : CoreAction
    {
        [ActionParameter] public bool State = false;
        
        [InjectComponent] private LevelProgressPanel _levelProgressPanel;
        
        protected override bool Action()
        {
            _levelProgressPanel.Active = State;
        
            return true;
        }
    }
}