using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.JetPack.HyperControls.Interfaces;
using UnityEngine;

namespace Krem.JetPack.HyperControls.Components
{
    [NodeGraphGroupName("Jet Pack/Hyper Controls")]
    public abstract class Axis2D : CoreComponent, IAxis2D
    {
        public virtual Vector2 Axis { get; set; } = Vector2.zero;
    }
}