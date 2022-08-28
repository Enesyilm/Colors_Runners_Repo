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

        [SerializeField] private bool isReadyForTouch;
        [SerializeField] private bool isFirstTimeTouchTaken =false;
        [SerializeField] private FloatingJoystick joystick;

        #endregion

        #region Private Variables

        private PlayerInputSystem _playerInput;
        private bool _isTouching; //ref type
        private float _currentVelocity; //ref type
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
            _playerInput.Runner.Enable();
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onEnableInput += OnEnableInput;
            InputSignals.Instance.onDisableInput += OnDisableInput;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            _playerInput.Runner.MouseDelta.performed += OnPlayerInputMouseDeltaPerformed;
            _playerInput.Runner.MouseDelta.canceled += OnPlayerInputMouseDeltaCanceled;
            _playerInput.Runner.MouseLeftButton.started += OnMouseLeftButtonStart;
            _playerInput.Idle.JoyStick.started += OnPlayerInputIdleJoystickStart; //YENÝ
            _playerInput.Idle.JoyStick.performed += OnPlayerInputIdleJoystickPerformed; //YENÝ
            _playerInput.Idle.JoyStick.canceled += OnPlayerInputIdleJoystickCanceled;//YENÝ
            CoreGameSignals.Instance.onChangeGameStateNew += OnChangeInputState;//yeni
        }

        private void UnSubscribeEvents()
        {
            InputSignals.Instance.onEnableInput -= OnEnableInput;
            InputSignals.Instance.onDisableInput -= OnDisableInput;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            _playerInput.Runner.MouseDelta.performed -= OnPlayerInputMouseDeltaPerformed;
            _playerInput.Runner.MouseDelta.canceled -= OnPlayerInputMouseDeltaCanceled;
            _playerInput.Runner.MouseLeftButton.started -= OnMouseLeftButtonStart;
            _playerInput.Idle.JoyStick.started -= OnPlayerInputIdleJoystickStart;//YENÝ
            _playerInput.Idle.JoyStick.performed -= OnPlayerInputIdleJoystickPerformed;//YENÝ
            _playerInput.Idle.JoyStick.canceled -= OnPlayerInputIdleJoystickCanceled;//YENÝ
            CoreGameSignals.Instance.onChangeGameStateNew -= OnChangeInputState; //yeni
        }

        private void OnDisable()
        {
            _playerInput.Runner.Disable();
            UnSubscribeEvents();
        }
        #endregion
        #region Mouse Drag Methods
        
        void OnPlayerInputMouseDeltaPerformed(InputAction.CallbackContext context)
        {
            InputSignals.Instance.onSidewaysEnable?.Invoke(true);

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

        void OnPlayerInputMouseDeltaCanceled(InputAction.CallbackContext context)
        {
            InputSignals.Instance.onSidewaysEnable?.Invoke(false);
            _playerMovementValue = new Vector3(context.ReadValue<Vector2>().x, 0f, 0f);
        }

        void OnMouseLeftButtonStart(InputAction.CallbackContext cntx)
        {
            if (isFirstTimeTouchTaken == false)
            {
                isFirstTimeTouchTaken = true;
                CoreGameSignals.Instance.onPlay?.Invoke();
            }
        }

        void OnPlayerInputIdleJoystickStart(InputAction.CallbackContext cntx)//YENÝ
        {
            //state Bu ise baþla
           // Debug.Log("JOYSTCÝK STARTED" + cntx.ReadValue<Vector2>());
            _playerMovementValue = new Vector3(cntx.ReadValue<Vector2>().x, 0f, cntx.ReadValue<Vector2>().y);
            InputSignals.Instance.onIdleInputDragged?.Invoke(new IdleInputParams()
            {
                IdleXValue = _playerMovementValue.x,
                IdleZValue = _playerMovementValue.z
            });


        }

        void OnPlayerInputIdleJoystickPerformed(InputAction.CallbackContext cntx)//YENÝ
        {
            //state Bu ise baþla
            _playerMovementValue = new Vector3(cntx.ReadValue<Vector2>().x, 0f, cntx.ReadValue<Vector2>().y);
           // Debug.Log("JOYSTCÝK PERFORMED"+cntx.ReadValue<Vector2>());
            InputSignals.Instance.onIdleInputDragged?.Invoke(new IdleInputParams()
            {
                IdleXValue = _playerMovementValue.x,
                IdleZValue =_playerMovementValue.z
            });
        }

        void OnPlayerInputIdleJoystickCanceled(InputAction.CallbackContext cntx)//YENÝ
        {
            
            _playerMovementValue = Vector3.zero;
            Debug.Log("JOYSTCÝK CANCELED" + cntx.ReadValue<Vector2>());
            InputSignals.Instance.onIdleInputDragged?.Invoke(new IdleInputParams()
            {
                IdleXValue = _playerMovementValue.x,
                IdleZValue = _playerMovementValue.z
            });
        }


        #endregion
        #region Subscribed Methods

        public void OnChangeInputState() //yeni
        {
            _playerInput.Runner.Disable();
            _playerInput.Idle.Enable();
           
        }

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