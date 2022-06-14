using Krem.JetPack.ScriptableORM;
using UnityEngine;

namespace Krem.DrawPad.Models
{
    [System.Serializable]
    public class DrawPadSettingsModel : Model 
    {
        [Header("Data")]
        [SerializeField] private int _maxAvailableSegments = 20;
        [SerializeField] private float _segmentHeight = 74;
        [SerializeField] private float _segmentCapSize = 10;
        [SerializeField] private float _segmentDrawAngleOffset = -90;
        [SerializeField] private GameObject _segmentPrefab;
        [SerializeField] private Color _backgroundColor;
        [SerializeField] private Color _drawBackgroundColor;
    
        public int MaxAvailableSegments { get => _maxAvailableSegments; set => SetField(ref _maxAvailableSegments, value); }
        public float SegmentHeight => _segmentHeight;
        public float SegmentCapSize => _segmentCapSize;
        public float SegmentDrawAngleOffset => _segmentDrawAngleOffset;
        public GameObject SegmentPrefab => _segmentPrefab;
        public Color BackgroundColor => _backgroundColor;
        public Color DrawBackgroundColor => _drawBackgroundColor;
    }
}