using App.ArmyClash.Components.Unit;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace App.ArmyClash.Actions.Unit
{
    [NodeGraphGroupName("ArmyClash/Unit")] 
    public class ApplySizeModifier : CoreAction 
    {
        [InjectComponent] protected UnitModelProvider _unitModelProvider;
        [InjectComponent] protected UnitShapeModifier _unitShapeModifier;
        [InjectComponent] protected UnitSizeModifier _unitSizeModifier;
        protected override bool Action()
        {
            _unitModelProvider.ApplyModelModifier(_unitSizeModifier.CurrentUnitSizeModifierModel);

            Transform transform = _unitShapeModifier.CurrentShapeInstance.transform;
            float size = _unitSizeModifier.CurrentUnitSizeModifierModel.Size;
            transform.localScale = new Vector3(size, size, size);
        
            return true;
        }
    }
}