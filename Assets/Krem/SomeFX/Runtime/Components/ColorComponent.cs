using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.SomeFX.Components
{
    [NodeGraphGroupName("Some FX")]
    public class ColorComponent : CoreComponent
    {
        public Color color;
    }
}