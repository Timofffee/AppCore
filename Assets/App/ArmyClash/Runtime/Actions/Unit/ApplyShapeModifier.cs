using App.ArmyClash.Components.Unit;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace App.ArmyClash.Actions.Unit
{
    [NodeGraphGroupName("ArmyClash/Unit")] 
    public class ApplyShapeModifier : CoreAction
    {
        [InjectComponent] protected UnitModelProvider _unitModelProvider;
        [InjectComponent] protected UnitShapeModifier _unitShapeModifier;
        
        protected override bool Action()
        {
            _unitModelProvider.ApplyModelModifier(_unitShapeModifier.CurrentUnitShapeModifierModel);

            _unitShapeModifier.CurrentShapeInstance =
                GameObject.Instantiate(_unitShapeModifier.CurrentUnitShapeModifierModel.ShapePrefab, _unitModelProvider.transform);
        
            return true;
        }
    }
}