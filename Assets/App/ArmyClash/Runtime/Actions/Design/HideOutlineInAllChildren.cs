using System.Linq;
using App.ArmyClash.Components.Design;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace App.ArmyClash.Actions.Design
{
    [NodeGraphGroupName("ArmyClash/Design")] 
    public class HideOutlineInAllChildren : CoreAction
    {
        [InjectComponent] private Transform _transform;
        
        protected override bool Action()
        {
            _transform.gameObject.GetComponentsInChildren<Outliner>().ToList().ForEach(outliner =>
            {
                outliner.Hide();
            });
        
            return true;
        }
    }
}