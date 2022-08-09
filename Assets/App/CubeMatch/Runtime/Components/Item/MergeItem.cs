using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace App.CubeMatch.Components.Item
{
    [NodeGraphGroupName("Cube Match/Item")]
    [DisallowMultipleComponent]
    public class MergeItem : CoreComponent
    {
        public int ItemId;
        [SerializeField, NotNull] private GameObject _hideFX;
        [SerializeField, NotNull] private MeshRenderer _meshRenderer;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private float _colorAnimationTime = .2f;
        
        public OutputSignal OnSelectRequest;

        private Material _itemMaterial;
        private Color _originalColor;
        private float _elapsedTime = 0f;

        private void Awake()
        {
            _itemMaterial = _meshRenderer.material;
            _originalColor = _itemMaterial.color;
        }

        public void SelectRequest()
        {
            StartCoroutine(InstantiateHideFX());
            StartCoroutine(AnimateColor());
            
            OnSelectRequest.Invoke();
        }
        
        protected IEnumerator InstantiateHideFX()
        {
            yield return new WaitForSeconds(.4f);

            Instantiate(_hideFX, transform.position, Quaternion.identity , null);
            Destroy(this.gameObject);
        }

        protected IEnumerator AnimateColor()
        {
            while (_elapsedTime < _colorAnimationTime)
            {
                yield return new WaitForEndOfFrame();
                _itemMaterial.color = Color.Lerp(_originalColor, _selectedColor, 1 - Mathf.Abs(2*_elapsedTime/_colorAnimationTime - 1));

                _elapsedTime += Time.deltaTime;
            }
        }
    }
}