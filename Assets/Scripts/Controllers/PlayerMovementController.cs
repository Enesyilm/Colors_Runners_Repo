using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.ValueObjects;
using Keys;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        #endregion

        #region Serialized Variables

        [SerializeField] private new Rigidbody rigidbody;
        #endregion

        #region Private Variables

        private PlayerMovementData _movementData;
        private bool _isReadyToMove, _isReadyToPlay;
        private float _inputValue;
        private Vector2 _clampValues;
        #endregion

        #endregion

        public void SetMovementData(PlayerMovementData playerMovementData)
        {
            _movementData = playerMovementData;
        }

        private void Awake()
        {
            _isReadyToMove = true;
            _isReadyToPlay = true;
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

        public void IsReadyToPlay(bool state)
        {
            _isReadyToPlay = state;
        }

        private void FixedUpdate()
        {
            if (_isReadyToPlay)
            {
                if (_isReadyToMove)
                {
                    Move();
                }
                else
                {
                    StopSideways();
                }
            }
            else
                Stop();
        }

        private void Move()
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

        private void StopSideways()
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, _movementData.ForwardSpeed);
            rigidbody.angularVelocity = Vector3.zero;
        }

        private void Stop()
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

        public void StopVerticalMovement()
        {
            _movementData.ForwardSpeed = 0;
            rigidbody.angularVelocity =Vector3.zero;
        }
    }
}


