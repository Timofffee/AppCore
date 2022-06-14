using System.Diagnostics.CodeAnalysis;
using Krem.AppCore.Attributes;
using Krem.JetPack.Basis.Components.Links;
using Krem.LevelProgressUI.Components.UI;
using UnityEngine;

namespace Krem.LevelProgressUI.Components.Game
{
    [NodeGraphGroupName("LevelProgressUI/Game")]
    [DisallowMultipleComponent]
    public class PlayerPointer : TransformLink
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected Pointer _pointerPrefab;

        public Pointer PointerPrefab => _pointerPrefab;
    }
}