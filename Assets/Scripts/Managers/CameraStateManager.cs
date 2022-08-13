using System;
using Managers.Abstracts;
using DG.Tweening;
using Managers.Abstracts.Concreates;
using Cinemachine;
using Enums;
using UnityEngine;
using Signals;
using UnityEngine.Events;

namespace Managers
{
    public class CameraStateManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        public Transform initPosition;
        

        #endregion
        #region Serialized Variables

        [SerializeField] 
        private CinemachineVirtualCamera virtualCamera;

       

        #endregion

        #region Private Variables

        private Vector3 _initialPosition;
        private Transform _playerManager;
        
        public CameraBaseState _currentState;
        private StartState _startState=new StartState();
        private StartOfIdleState _startOfIdleState=new StartOfIdleState();
        private RunnerState _runnerState=new RunnerState();
        private IdleState _idleState=new IdleState();

        #endregion

        #endregion

        #region Event Subscription
        private void Awake()
        {
            
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
            GetInitialPosition();
            
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void Start()
        {
            OnCameraInitialization();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnCameraInitialization;
            CameraSignals.Instance.onChangeCameraStates += OnChangeCameraState;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnCameraInitialization;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        
        private void Update()
        {
            //currentState.UpdateState(this);
        }

        #region Reset Methods
        private void GetInitialPosition()
        {
            _initialPosition = transform.localPosition;
        }
        private void OnMoveToInitialPosition()
        {
            transform.localPosition = _initialPosition;
        }
        

        #endregion
        
        private void OnCameraInitialization()
        {
            ChangeState(_startState);
        }

        #region Subscribed Methods
        #region States

        private void OnChangeCameraState(CameraStates _currentState)
        {
            switch (_currentState)
            {
                case CameraStates.Idle:
                    ChangeState(_idleState);
                    break;
                case CameraStates.Runner:
                    ChangeState(_runnerState);
                    break;
                case CameraStates.StartState:
                    ChangeState(_startState);
                    break;
                case CameraStates.StartOfIdle:
                    ChangeState(_startOfIdleState);
                    break;
                
            }
        }
        private void ChangeState(CameraBaseState _cameraState)
        {
           
            var _playerManager =GameObject.FindWithTag("Player").transform;
            _currentState = _cameraState;
            _currentState.EnterState(this,virtualCamera,_playerManager);
            
        }
        #endregion
        private void OnReset()
        {
            virtualCamera.Follow = null;
            virtualCamera.LookAt = null;
            OnMoveToInitialPosition();
        }
        

        #endregion
        
        
    }
}