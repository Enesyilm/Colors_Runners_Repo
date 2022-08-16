using UnityEngine;
using Data.UnityObjects;
using Data.ValueObjects;
using Controllers;
using Keys;
using Enums;
using System.Collections;
using Signals;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public float CurrentScore;

        #endregion

        #region Serialized Variables

        [SerializeField] 
        private PlayerMovementController playerMovementController;
        [SerializeField] 
        private PlayerPhysicsController playerPhysicsController;
        [SerializeField] 
        private PlayerTextController playerTextController;
        [SerializeField]
        private ColorTypes _currentColor;

        #endregion

        #region Private Variables

        private PlayerData Data;
        

        #endregion

        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTaken += OnActivateMovement;
            InputSignals.Instance.onInputReleased += OnDeactivateMovement;
            InputSignals.Instance.onInputDragged += OnGetInputValues;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            PlayerSignal.Instance.onGetColor += OnGetColorType;
            //ScoreSignals.Instance.onUpdateScore += OnUpdateScore;
        }

        private void UnSubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= OnActivateMovement;
            InputSignals.Instance.onInputReleased -= OnDeactivateMovement;
            InputSignals.Instance.onInputDragged -= OnGetInputValues;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            //ScoreSignals.Instance.onUpdateScore -= OnUpdateScore;
        }
        #endregion

        private void Awake()
        {
            Data = GetPlayerData();
            SendPlayerDataToMovementController();
        }

        private void SendPlayerDataToMovementController()
        {
            playerMovementController.SetMovementData(Data.PlayerMovementData);
        }

        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").PlayerData;

        #region Subcribed Methods

        #region Movement Controller
        private void OnActivateMovement()
        {
            playerMovementController.EnableMovement();
        }

        private void OnDeactivateMovement()
        {
            playerMovementController.DisableMovement();
        }

        private void OnGetInputValues(RunnerHorizontalInputParams inputParam)
        {
            playerMovementController.UpdateInputValue(inputParam);
        }
        #endregion

        private void OnPlay()
        {
            playerMovementController.IsReadyToPlay(true);
        }
        private void OnLevelSuccessful()
        {
            playerMovementController.IsReadyToPlay(false);
        }

        private void OnLevelFailed()
        {
            playerMovementController.IsReadyToPlay(false);
        }
        
        private void OnUpdateScore(float totalScore)
        {
            playerTextController.UpdatePlayerScore(totalScore);
            CurrentScore = totalScore;
        }
        public void StopVerticalMovement()
        {
            playerMovementController.StopVerticalMovement();
        }

        private void OnReset()
        {
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
            OnUpdateScore(0);
            playerMovementController.OnReset();
        }
        #endregion

       public ColorTypes OnGetColorType()
       {
           return _currentColor;
       }
    }
}


