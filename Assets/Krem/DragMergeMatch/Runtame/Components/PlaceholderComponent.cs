using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Krem.DragMergeMatch.Components
{
    [NodeGraphGroupName("Drag Merge Match")]
    [DisallowMultipleComponent]
    public class PlaceholderComponent : CoreComponent
    {    
        [Header("Data")]
        [SerializeField] protected PlaceableComponent _attachedPlaceable;
        [SerializeField] protected string _guid;
        
        [Header("States")]
        [SerializeField] protected bool _active = true;

        [Header("Ports")]
        [BindInputSignal(nameof(HighlightRequest))] public InputSignal CallHighlightRequest;
        public OutputSignal OnHighlightRequest;
        public OutputSignal OnOverflow;
        public OutputSignal OnItemPlaced;
        public OutputSignal OnPlaceholderUpdate;
        
        private Transform _transform;
        private Camera _mainCamera;

        public PlaceableComponent AttachedPlaceable => _attachedPlaceable;
        public bool Active
        {
            get => _active;
            set => _active = value;
        }
        public PlaceableComponent OverflowItem { get; private set; }
        public string Guid => _guid;
        public Transform Transform => _transform;
        public Camera MainCamera => _mainCamera;
        
        private void Awake()
        {
            _transform = transform;
            
            if (string.IsNullOrEmpty(Guid))
            {
                _guid = System.Guid.NewGuid().ToString();
            }
            
            Canvas canvas = gameObject.GetComponentInParent<Canvas>();
            
            if (canvas == null)
            {
                _mainCamera = Camera.main;
            }
            else
            {
                _mainCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? Camera.main : canvas.worldCamera;
            }
        }

        private void Reset()
        {
            RegenerateGuid();
        }

        public bool IsEmpty()
        {
            if (_attachedPlaceable == null)
                return true;
            
            return false;
        }

        public void Attach(PlaceableComponent placeableInstance)
        {
            _attachedPlaceable = placeableInstance;
            _attachedPlaceable.Transform.parent = this.Transform;
            placeableInstance.placeholderComponent = this;
            

            OnItemPlaced.Invoke();
            OnPlaceholderUpdate.Invoke();
        }

        public void Detach()
        {
            _attachedPlaceable.Transform.parent = this.Transform.parent;
            _attachedPlaceable.placeholderComponent = null;
            _attachedPlaceable = null;
            
            OnPlaceholderUpdate.Invoke();
        }

        public void Overflow(PlaceableComponent placeableComponentInstance)
        {
            OverflowItem = placeableComponentInstance;
            
            OnOverflow.Invoke();
        }

        public void RegenerateGuid()
        {
#if UNITY_EDITOR
            SerializedObject serializedObject = new UnityEditor.SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty(nameof(_guid));
            serializedProperty.stringValue = System.Guid.NewGuid().ToString();
            serializedObject.ApplyModifiedProperties();
#endif
        }

        public void HighlightRequest()
        {
            OnHighlightRequest.Invoke();
        }
    }
}