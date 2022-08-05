using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.JetPack.Basis.Components.Links;
using UnityEngine;

namespace Krem.SliderCarousel.Components
{
    [NodeGraphGroupName("Slider Carousel")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TransformLink))]
    public sealed class SliderItem : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] private TransformLink _transformLink;

        [Header("Data")]
        public Vector3 size = Vector3.one;
        public Vector3 scale = Vector3.one;
        public Quaternion rotation = Quaternion.identity;
        
        public TransformLink TransformLink => _transformLink;
    }
}