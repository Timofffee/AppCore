using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.LevelProgressUI.Components.Game;
using UnityEngine;

namespace Krem.LevelProgressUI.Components.UI
{
    [NodeGraphGroupName("LevelProgressUI/UI")]
    [DisallowMultipleComponent]
    public class Pointer : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected RectTransform _rectTransform;

        public RectTransform RectTransform => _rectTransform;
        public PlayerPointer PlayerPointerLink { get; set; }
    }
}