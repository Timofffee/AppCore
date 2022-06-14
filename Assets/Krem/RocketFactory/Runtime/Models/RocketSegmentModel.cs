using System;
using Krem.JetPack.ScriptableORM;
using UnityEngine;

namespace Krem.RocketFactory.Models
{
    public enum RocketSegmentType
    {
        Head,
        Thin,
        TransitionToWide,
        Wide,
        Tail
    }
    
    [Serializable]
    public class RocketSegmentModel : Model
    {
        [Header("Data")]
        [SerializeField] protected RocketSegmentType _segmentType;
        [SerializeField] protected GameObject _segmentPrefab;
        [SerializeField] protected float _height;
        [SerializeField] protected Quaternion _rotation = Quaternion.identity;

        public RocketSegmentType SegmentType { get => _segmentType; set => SetField(ref _segmentType, value); }
        public GameObject SegmentPrefab { get => _segmentPrefab; set => SetField(ref _segmentPrefab, value); }
        public float Height { get => _height; set => SetField(ref _height, value); }
        public Quaternion Rotation { get => _rotation; set => SetField(ref _rotation, value); }
    }
}
