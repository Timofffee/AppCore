using System;
using System.Diagnostics.CodeAnalysis;
using Cinemachine;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.CinemachineManagement.Components
{
    [NodeGraphGroupName("Cinemachine Management")]
    [DisallowMultipleComponent]
    public class CinemachineTargetVelocityOffsetController : CoreComponent
    {    
        [Header("Dependencies")]
        [SerializeField, NotNull] protected CinemachineVirtualCamera _cinemachineVirtualCamera;
        [SerializeField, NotNull] protected CinemachineCameraOffset _cinemachineCameraOffset;

        [Header("Settings")]
        public float distancePerMS = 0.1f;
        public float offsetChangeSpeed = 10f;

        private Rigidbody _targetRigidbody;
        private Vector3 _initialLookAtVector;
        private Vector3 _initialOffset;

        public CinemachineVirtualCamera CinemachineVirtualCamera => _cinemachineVirtualCamera;
        public CinemachineCameraOffset CinemachineCameraOffset => _cinemachineCameraOffset;
        public Rigidbody TargetRigidbody => _targetRigidbody;
        public Vector3 InitialLookAtVector => _initialLookAtVector;
        public Vector3 InitialOffset => _initialOffset;

        private void Awake()
        {
            _targetRigidbody = _cinemachineVirtualCamera.Follow.gameObject.GetComponent<Rigidbody>();

            if (_targetRigidbody == null)
            {
                throw new Exception("Can`t find Rigidbody on Follow target");
            }

            _initialLookAtVector =
                _cinemachineVirtualCamera.transform.position - _cinemachineVirtualCamera.Follow.position;
            _initialLookAtVector = _initialLookAtVector.normalized;

            _initialOffset = _cinemachineCameraOffset.m_Offset;
        }
    }
}