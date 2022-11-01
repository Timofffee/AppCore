using App.ArmyClash.Components.Unit;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.JetPack.Basis.Components.Links;
using UnityEngine;

namespace App.ArmyClash.Actions.Unit
{
    [NodeGraphGroupName("ArmyClash/Unit")] 
    public class ApplyColorModifier : CoreAction 
    {
        [InjectComponent] protected UnitModelProvider _unitModelProvider;
        [InjectComponent] protected UnitShapeModifier _unitShapeModifier;
        [InjectComponent] protected UnitColorModifier _unitColorModifier;
        protected override bool Action()
        {
            _unitModelProvider.ApplyModelModifier(_unitColorModifier.CurrentUnitColorModifierModel);
            
            MeshRenderer meshRenderer =
                _unitShapeModifier.CurrentShapeInstance.GetComponent<MeshRendererLink>().MeshRenderer;

            meshRenderer.sharedMaterial = _unitColorModifier.CurrentUnitColorModifierModel.Material;
        
            return true;
        }
    }
}