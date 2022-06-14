using System;
using Krem.JetPack.ScriptableORM;
using UnityEngine;

namespace Krem.DrawPad.Models
{
    [Serializable]
    public class SegmentModel: Model
    {
        public Vector2 position;
        public Quaternion rotation;
        public Vector2 sizeDelta;
        public Vector2 anchoredPosition;
    }
}