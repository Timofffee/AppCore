﻿using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.JetPack.HyperControls.Components.Rubber;
using UnityEngine;

namespace Krem.JetPack.HyperControls.Actions.Rubber
{
    [NodeGraphGroupName("Jet Pack/Hyper Controls/Rubber")]
    public class PointerUp : CoreAction
    {
        [InjectComponent] private RubberControl _rubberControl;

        protected override bool Action()
        {
            _rubberControl.Handle.anchoredPosition = _rubberControl.Body.anchoredPosition;
            
            _rubberControl.Axis = Vector2.zero;

            return true;
        }
    }
}