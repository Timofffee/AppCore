using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.JetPack.ObjectsPool.Interfaces;
using UnityEngine;

namespace Krem.DrawPad.Components
{
    [NodeGraphGroupName("DrawPad")]
    [DisallowMultipleComponent]
    public class DrawPadSegment : CoreComponent, IPoolItem
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] private RectTransform _transform;

        private DrawPad _pool;

        public RectTransform RectTransform => _transform;

        public DrawPad Pool
        {
            get => _pool;
            set => _pool = value;
        }

        public void ReturnToPool()
        {
            Pool.ReturnToPool(this);
        }
    }
}