using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.JetPack.Basis.Components.Links
{
    [NodeGraphGroupName("Jet Pack/Basis/Links")]
    public class RectTransformLink : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected RectTransform _rectTransform;
        
        public RectTransform RectTransform => _rectTransform;
    }
}