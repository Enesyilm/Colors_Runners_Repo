using System.Collections.Generic;
using Controllers;
using DG.Tweening;
using Enums;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private UIPanelController uiPanelController;

        [SerializeField]
        private RectTransform arrow;
        [SerializeField] private LevelPanelController levelPanelController;
        [SerializeField] private TextMeshProUGUI leveltext;
        [SerializeField] private TextMeshProUGUI totalScore;
        [SerializeField] private IdlePanelController idlePanelController;

        #endregion

        #region Private Variables
        
        private int _currentLevel;
        
        #endregion

        #endregion

        #region Event Subscriptions
        private void Awake()
        {
            UpdateLevelText();
        }
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onUpdateScore+=GetTotalScoreData;
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            //CoreGameSignals.Instance.onGameInit += OnGameInit;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onGameInit += UpdateLevelText;
            CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
        }

       

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
           //CoreGameSignals.Instance.onGameInit -= OnGameInit;
           CoreGameSignals.Instance.onGameInit -= UpdateLevelText;
           CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnOpenPanel(UIPanelTypes panelParam)
        {
            uiPanelController.OpenPanel(panelParam);
        }

        private void OnClosePanel(UIPanelTypes panelParam)
        {
            uiPanelController.ClosePanel(panelParam);
        }
        

        private void OnSetLevelText(int value)
        {
            levelPanelController.SetLevelText(value);
        }

        private void OnPlay()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanelTypes.StartPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.LevelPanel);
        }
        

        private void OnLevelFailed()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.FailPanel);
        }
        private void UpdateLevelText()
        {
            leveltext.text="Level "+(1+CoreGameSignals.Instance.onGetLevelID?.Invoke()).ToString();
            
        }

        // private void OnGameInit()
        // {
        //     UISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.StartPanel);
        // }
        private void OnLevelSuccessful()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanelTypes.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.IdlePanel);
        }
        
        public void Play()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
            
        }
        private void CursorMovementOnRoulette()
        {
            Sequence _sequence = DOTween.Sequence();
            _sequence.Join(arrow.transform.DORotate(new Vector3(0, 0, 30), 1).SetEase(Ease.Linear))
                .SetLoops(-1, LoopType.Yoyo);
            _sequence.Join(arrow.transform.DOLocalMoveX(-200, 1).SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo));
        }

        private void OnChangeGameState(GameStates _gameState)
        {
            switch (_gameState)
            {
                case GameStates.Roulette:
                    OnOpenPanel(UIPanelTypes.RoulettePanel);
                    CursorMovementOnRoulette();
                    break;
                case GameStates.Idle:
                    OnOpenPanel(UIPanelTypes.IdlePanel);
                    OnClosePanel(UIPanelTypes.RoulettePanel);
                    OnClosePanel(UIPanelTypes.LevelPanel);
                    break;
                case GameStates.Runner:
                    OnOpenPanel(UIPanelTypes.StartPanel);
                    break;
                case GameStates.Failed:
                    OnClosePanel(UIPanelTypes.LevelPanel);
                    OnOpenPanel(UIPanelTypes.FailPanel);
                    break;
            }
        }

        public void NextLevel()
        {
            CoreGameSignals.Instance.onChangeGameState.Invoke(GameStates.Runner);
            CoreGameSignals.Instance.onNextLevel?.Invoke();
            UISignals.Instance.onClosePanel?.Invoke(UIPanelTypes.IdlePanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.StartPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.LevelPanel);
        }
        public void RetryLevel()
        {
            //Debug.Log("RetryLevel");

            CoreGameSignals.Instance.onReset?.Invoke();
            UISignals.Instance.onClosePanel?.Invoke(UIPanelTypes.FailPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.StartPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.LevelPanel);

        }
        public void RestartButton()
        {
            Debug.Log("RestartButton");
            UISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.StartPanel);
            CoreGameSignals.Instance.onReset?.Invoke();
        }
        public void EnterIdle()
        {
            
            CoreGameSignals.Instance.onChangeGameState?.Invoke(GameStates.Idle);
            NewCameraSignals.Instance.onChangeCameraState(CameraStates.Idle);
        }

        public void GetTotalScoreData(List<int> ScoreValues)
        {
            string _currentTotalScore=ScoreValues[0].ToString();
            UpdateTotalScore(_currentTotalScore);
        }
        public void UpdateTotalScore(string _currentTotalScore)
        {
            totalScore.text = _currentTotalScore;
        }
    }
}