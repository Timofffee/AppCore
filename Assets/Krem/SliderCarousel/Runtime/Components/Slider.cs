using System.Collections.Generic;
using System.Linq;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.SliderCarousel.Components
{
    [NodeGraphGroupName("Slider Carousel")]
    [DisallowMultipleComponent]
    public class Slider : CoreComponent
    {
        [Header("Data")]
        [SerializeField] private List<SliderItem> _items;
        
        [Header("Settings")]
        public float dragSensitivity = 1f;
        public float changeIndexThreshold = 300f;
        public float autoMoveSpeed = 1f;
        public float instantPlacementThreshold = 0.2f;
        public bool looped = false;

        [Header("States")]
        [SerializeField] private bool _active;
        [SerializeField] private int _selectedItemIndex;

        [Header("Ports")]
        [BindInputSignal(nameof(CallRefreshItemsList))] public InputSignal RefreshItemsList;
        public OutputSignal OnSelectedItemChanged;
        public OutputSignal OnRefreshItemsList;

        public List<SliderItem> Items => _items;
        public bool Active { get => _active; set => _active = value; }
        public int SelectedItemIndex
        {
            get => _selectedItemIndex;
            set
            {
                if (_selectedItemIndex == value)
                {
                    return;
                }

                _selectedItemIndex = value;
                
                if (value < 0 || value >= Items.Count)
                {
                    if (!looped)
                    {
                        _selectedItemIndex = _selectedItemIndex < 0 ? 0 : _selectedItemIndex;
                        _selectedItemIndex = _selectedItemIndex >= Items.Count ? Items.Count - 1 : _selectedItemIndex;
                        
                        return;
                    }
                    
                    _selectedItemIndex = _selectedItemIndex < 0 ? Items.Count - 1 : _selectedItemIndex;
                    _selectedItemIndex = _selectedItemIndex >= Items.Count ? 0 : _selectedItemIndex;
                }
                
                CurrentItem = Items[_selectedItemIndex];
                
                OnSelectedItemChanged.Invoke();
            }
        }
        public SliderItem CurrentItem { get; set; }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }
        
        public void CallRefreshItemsList()
        {
            _items = GetComponentsInChildren<SliderItem>().ToList();

            if (_items.Count == 0)
            {
                CurrentItem = null;

                return;
            }
            
            CurrentItem = Items[SelectedItemIndex];
            
            OnRefreshItemsList.Invoke();
        }

        public void Prev()
        {
            if (_active == false)
            {
                return;
            }
            
            SelectedItemIndex--;
        }

        public void Next()
        {
            if (_active == false)
            {
                return;
            }
            
            SelectedItemIndex++;
        }
    }
}