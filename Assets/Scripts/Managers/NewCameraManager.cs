using System;
using Enums;
using Signals;
using UnityEngine;

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

        #region Public Variables

        

        #endregion
        #endregion

        #region SubscribeEvents

        
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            NewCameraSignals.Instance.onChangeCameraState+=OnChangeCameraState;
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