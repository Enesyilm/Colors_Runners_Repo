using System;
using Enums;
using Signals;
using UnityEngine;
using Cinemachine;

namespace Managers
{
    public class NewCameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField]
        private Animator animator;

        #endregion

        #region Private Variables

        private Transform _playerManager;
        private CinemachineStateDrivenCamera _cinemachineStateDrivenCamera;

        #endregion
        #endregion

        #region SubscribeEvents

        public void GetReferences()
        {
            _cinemachineStateDrivenCamera= gameObject.GetComponent<CinemachineStateDrivenCamera>();
           
        }

        private  void Awake()
        {
            GetReferences();
        }
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            NewCameraSignals.Instance.onChangeCameraState+=OnChangeCameraState;
            CoreGameSignals.Instance.onGameInit+=OnGameInit;
        }

        private void OnGameInit()
        {
            OnFindPlayer();
            AssignData();
        }

        private void AssignData()
        {
            _cinemachineStateDrivenCamera.Follow = _playerManager;
        }
        private void OnFindPlayer()
        {
            _playerManager=GameObject.FindWithTag("Player").transform;  
        }

        private void UnSubscribeEvents()
        {
            NewCameraSignals.Instance.onChangeCameraState-=OnChangeCameraState;
        }
        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        #endregion

        public void OnChangeCameraState(CameraStates _currentcameraStates)
        {
            animator.Play(_currentcameraStates.ToString());
        }
    }
}