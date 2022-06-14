using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.LevelProgressUI.Components.Game;
using UnityEngine;

namespace Krem.LevelProgressUI.Components.UI
{
    [NodeGraphGroupName("LevelProgressUI/UI")]
    [DisallowMultipleComponent]
    public class LevelProgressPanel : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected RectTransform _mainProgress;

        [Header("Ports")]
        [BindInputSignal(nameof(Refresh))] public InputSignal CallRefresh;
        public OutputSignal OnRefreshed; 

        private List<PlayerPointer> _playerPointers;
        private List<Pointer> _uiPointers = new List<Pointer>();
        private Vector3 _startPosition;
        private Vector3 _finishPosition;
        private float _levelLength;

        public bool Active { get; set; } = false;
        public RectTransform MainProgress => _mainProgress;
        public List<Pointer> UIPointers => _uiPointers;
        
        public void Refresh()
        {
            _playerPointers = FindObjectsOfType<PlayerPointer>().ToList();
            
            UIPointers.ForEach(pointer =>
            {
                Destroy(pointer.gameObject);
            });
            
            UIPointers.Clear();
            
            _playerPointers.ForEach(playerPointer =>
            {
                GameObject pointerInstance = GameObject.Instantiate(playerPointer.PointerPrefab.gameObject, _mainProgress);
                Pointer uiPointer = pointerInstance.GetComponent<Pointer>();
                uiPointer.PlayerPointerLink = playerPointer;
                
                _uiPointers.Add(uiPointer);
            });
            
            _startPosition = FindObjectOfType<LevelStart>().Transform.position;
            _finishPosition = FindObjectOfType<LevelFinish>().Transform.position;
            _levelLength = _finishPosition.y - _startPosition.y;
            
            OnRefreshed.Invoke();
        }

        public float GetPointerRectPosition(Vector3 worldPosition)
        {
            return Mathf.Clamp(1 - (_finishPosition.y - worldPosition.y) / _levelLength, 0 ,1f) * _mainProgress.rect.width;
        }
    }
}