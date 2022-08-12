using System;
using Managers.Abstracts;

using Managers.Abstracts.Concreates;
using Cinemachine;
using UnityEngine;
using Signals;

namespace Managers
{
    public class CameraStateManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        #endregion

        #region Private Variables

        private Vector3 _initialPosition;
        
        private CameraBaseState _currentState;
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

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnSetCameraTarget;
            CoreGameSignals.Instance.onLevelInitialize+=OnStartState;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnSetCameraTarget;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        

        private void Start()
        {
            _currentState = _startState;
            _startState.EnterState(this);
        }

        private void Update()
        {
            _currentState.UpdateState(this);
        }
        private void GetInitialPosition()
        {
            _initialPosition = transform.localPosition;
        }

        private void OnMoveToInitialPosition()
        {
            transform.localPosition = _initialPosition;
        }
        
        private void OnSetCameraTarget()
        {
            //var playerManager = FindObjectOfType<PlayerManager>().transform;player manager gelince kullanilacak
            //virtualCamera.Follow = playerManager;
            //virtualCamera.LookAt = playerManager;
        }
        private void OnStartState()
        {
            _currentState = _startState;
            _currentState.EnterState(this);
            
        }

        private void OnReset()
        {
            virtualCamera.Follow = null;
            virtualCamera.LookAt = null;
            OnMoveToInitialPosition();
        }
        
    }
}