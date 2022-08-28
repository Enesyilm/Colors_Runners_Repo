using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Data.ValueObjects;
using Keys;
using DG.Tweening;
using Managers;
using Signals;


namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables
        #region Serialized Variables
        [SerializeField]private new Rigidbody rigidbody;
        [SerializeField]private PlayerManager playerManager;

        #endregion

        #region Private Variables

        private PlayerMovementData _movementData;
        
        
        private bool _isReadyToMove, _isReadyToPlay;
        private float _inputValue;
        private float _inputXValue;
        private float _inputZValue;
        private Vector2 _clampValues;
        private bool _sidewaysEnable = false;
        private bool _isRunner =true;//true
        
        #endregion
        #endregion


        
        public void SetMovementData(PlayerMovementData playerMovementData)
        {
            _movementData = playerMovementData;
        }
        
        public void EnableMovement()
        {
            _isReadyToMove = true;
            _isReadyToPlay = true;
        }

        public void DisableMovement()
        {
            _isReadyToMove = false;
        }

        public void UpdateInputValue(RunnerHorizontalInputParams inputParam)
        {
            _inputValue = inputParam.XValue;
            _clampValues = inputParam.ClampValues;
        }
        public void UpdateIdleInputValue(IdleInputParams idleInputParams) //Yeni
        {
            Debug.Log("Movement Data");
            _isRunner = false;
            _inputXValue = idleInputParams.IdleXValue;
            _inputZValue = idleInputParams.IdleZValue;
        }



        public void IsReadyToPlay(bool state)
        {
            _isReadyToPlay = state;
        }

        private void FixedUpdate()
        {
            Debug.Log("ÝsreadyToplay " + _isReadyToPlay + "ÝsReadToMove" + _isReadyToMove + "_sidewaysEnable" + _sidewaysEnable);
            if (_isReadyToPlay)
            {
                if (_isReadyToMove && _sidewaysEnable)
                {
                    Move(); //revize ettim
                }
                else
                {
                    StopSideways();
                }
            }
            else
                Stop();
        }

        private void RunnerMove()
        {
            var velocity = rigidbody.velocity;
            
            velocity = new Vector3(_inputValue * _movementData.SidewaysSpeed,velocity.y,
                _movementData.ForwardSpeed);
            rigidbody.velocity = velocity;
            Vector3 position;
            position = new Vector3(
                Mathf.Clamp(rigidbody.position.x, _clampValues.x,
                    _clampValues.y),
                (position = rigidbody.position).y,
                position.z);
            rigidbody.position = position;
        }

        private void IdleMove() //Yeni
        {
            var velocity = rigidbody.velocity;
            Debug.Log("ÝDLE ÇALIÞIYOR");
            velocity = new Vector3(_inputXValue * _movementData.IdleSidewaysSpeed, velocity.y,
                _inputZValue * _movementData.IdleForwardSpeed);
            rigidbody.velocity = velocity;
            Debug.Log("VELOCÝTY"+velocity);
            if (_inputXValue != 0 && _inputZValue != 0)
            {
                playerManager.ChangeAnimation(Enums.PlayerAnimationTypes.Run);
                Quaternion toRotation = Quaternion.LookRotation(new Vector3(_inputXValue, 0, _inputZValue));
                transform.rotation = toRotation;
            }

        }

        private void Move() //yeni
        {
            Debug.Log("ÝSRUnner"+_isRunner);
            if (_isRunner)
            {
                RunnerMove();
            }
            else
            {
                Debug.Log("MOVEIDLE");
                IdleMove();
            }
        }

        public void ChangeGameState() //new
        {//state deðiince inputu deðiþ
            _isRunner = false;
            _isReadyToPlay = true;
            _sidewaysEnable = true;
            
        }

        private void StopSideways()
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, _movementData.ForwardSpeed);
            rigidbody.angularVelocity = Vector3.zero;
        }

        public void Stop()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        public void OnReset()
        {
            Stop();
            _isReadyToPlay = false;
            _isReadyToMove = false;
        }

        public void SetSidewayEnabled(bool isSidewayEnabled)
        {
            _sidewaysEnable = isSidewayEnabled;
        }

        public  void ChangeVerticalMovement(float _verticalSpeed)
        {
             _movementData.ForwardSpeed = _verticalSpeed;
        }
        public void RepositionPlayerForDrone(GameObject _other)
        {
           transform.DOMove(new Vector3(_other.transform.position.x, transform.position.y, _other.transform.position.z+_other.transform.localScale.z),3f);
        }
        public void DisableStopVerticalMovement()
        {
            _movementData.ForwardSpeed = 0;
            rigidbody.angularVelocity =Vector3.zero;
        }

        public void StopAllMovement()
        {
            Debug.Log("StopAllMovement");
            _isReadyToPlay = false;
        }
    }
}


