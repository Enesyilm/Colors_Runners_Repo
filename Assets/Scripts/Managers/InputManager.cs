using UnityEngine;
using Data.UnityObjects;
using Data.ValueObjects;
using Keys;
using Signals;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Extentions;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public InputData Data;

        #endregion

        #region Serialized Variables

        [SerializeField] private bool isReadyForTouch, isFirstTimeTouchTaken;

        #endregion

        #region Private Variables

        private PlayerInputSystem _playerInput;
        private bool _isTouching; //ref type
        private float _currentVelocity; //ref type
        private Vector2? _mousePosition; //ref type
        private Vector3 _moveVector; //ref type
        private Vector3 _playerMovementValue;

        #endregion

        #endregion

        private void Awake()
        {
            Data = GetInputData();
            _playerInput = new PlayerInputSystem();
            _playerMovementValue = Vector3.zero;

        }

        private InputData GetInputData() => Resources.Load<CD_Input>("Data/CD_Input").InputData;

        #region Event Subscriptions

        private void OnEnable()
        {
            _playerInput.Runner.MouseDelta.Enable();
            
            SubscribeEvents();

            
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onEnableInput += OnEnableInput;
            InputSignals.Instance.onDisableInput += OnDisableInput;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;

            _playerInput.Runner.MouseDelta.started += OnPlayerInputMouseDeltaStart;
            _playerInput.Runner.MouseDelta.performed += OnPlayerInputMouseDeltaPerformed;
            _playerInput.Runner.MouseDelta.canceled += OnPlayerInputMouseDeltaCanceled;
        }

        private void UnSubscribeEvents()
        {
            InputSignals.Instance.onEnableInput -= OnEnableInput;
            InputSignals.Instance.onDisableInput -= OnDisableInput;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;

            _playerInput.Runner.MouseDelta.started -= OnPlayerInputMouseDeltaStart;
            _playerInput.Runner.MouseDelta.performed -= OnPlayerInputMouseDeltaPerformed;
            _playerInput.Runner.MouseDelta.canceled -= OnPlayerInputMouseDeltaCanceled;
        }

        private void OnDisable()
        {
            _playerInput.Runner.MouseDelta.Disable();
            UnSubscribeEvents();
            
        }

        #endregion
        #region Mouse Drag Methods
        
        void OnPlayerInputMouseDeltaStart(InputAction.CallbackContext context)
        {
            _playerMovementValue = new Vector3(context.ReadValue<Vector2>().x, 0f, 0f);
            Vector2 mouseDeltaPos = new Vector2(context.ReadValue<Vector2>().x, 0f);

            if (mouseDeltaPos.x > Data.HorizontalInputSpeed)
                _moveVector.x = Data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
            else if (mouseDeltaPos.x < -Data.HorizontalInputSpeed)
                _moveVector.x = -Data.HorizontalInputSpeed / 10f * -mouseDeltaPos.x;
            else
                _moveVector.x = Mathf.SmoothDamp(_moveVector.x, 0f, ref _currentVelocity,
                    Data.ClampSpeed);
            
            InputSignals.Instance.onInputDragged?.Invoke(new RunnerHorizontalInputParams()
            {
                XValue = _moveVector.x, 
                ClampValues = new Vector2(Data.ClampSides.x, Data.ClampSides.y) 
            });
        }

        void OnPlayerInputMouseDeltaPerformed(InputAction.CallbackContext context)
        {
            _playerMovementValue = new Vector3(context.ReadValue<Vector2>().x, 0f, 0f);
        }

        void OnPlayerInputMouseDeltaCanceled(InputAction.CallbackContext context)
        {
            _playerMovementValue = new Vector3(context.ReadValue<Vector2>().x, 0f, 0f);
        }

        //private bool IsPointerOverUIElement()
        //{
        //    var eventData = new PointerEventData(EventSystem.current);
        //    eventData.position = Input.mousePosition;
        //    var results = new List<RaycastResult>();
        //    EventSystem.current.RaycastAll(eventData, results);
        //    return results.Count > 0;
        //}
        #endregion
        #region Subscribed Methods

        private void OnEnableInput()
        {
            isReadyForTouch = true;
        }

        private void OnDisableInput()
        {
            isReadyForTouch = false;
        }

        private void OnPlay()
        {
            isReadyForTouch = true;
        }

        private void OnReset()
        {
            _isTouching = false;
            isReadyForTouch = false;
            isFirstTimeTouchTaken = false;
        }
        #endregion
    }
}