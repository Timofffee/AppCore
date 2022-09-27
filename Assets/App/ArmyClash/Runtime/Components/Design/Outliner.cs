using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace App.ArmyClash.Components.Design
{
    [NodeGraphGroupName("ArmyClash/Design")]
    [DisallowMultipleComponent]
    public class Outliner : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected GameObject _outlineObject;

        public GameObject OutlineObject => _outlineObject;

        public void Show()
        {
            OutlineObject.SetActive(true);
        }

        public void Hide()
        {
            OutlineObject.SetActive(false);
        }
    }
}