using Controllers;
using DG.Tweening;
using Enums;
using Signals;
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
        [SerializeField] private IdlePanelController idlePanelController;

        #endregion

        #region Private Variables
        
        private int _currentLevel;
        
        #endregion

        #endregion

        #region Event Subscriptions
        private void Awake()
        {
        }
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            CoreGameSignals.Instance.onGameInit += OnGameInit;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
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
                    break;
                case GameStates.Runner:
                    OnOpenPanel(UIPanelTypes.StartPanel);
                    break;
            }
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            CoreGameSignals.Instance.onGameInit -= OnGameInit;
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
            UISignals.Instance.onClosePanel?.Invoke(UIPanelTypes.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.FailPanel);
        }

        private void OnGameInit()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.StartPanel);
        }
        private void OnLevelSuccessful()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanelTypes.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.IdlePanel);
        }
        
        public void Play()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
            
        }

        public void NextLevel()
        {
            CoreGameSignals.Instance.onNextLevel?.Invoke();
            UISignals.Instance.onClosePanel?.Invoke(UIPanelTypes.IdlePanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.StartPanel);
        }

        public void RestartLevel()
        {
            CoreGameSignals.Instance.onRestartLevel?.Invoke();
            UISignals.Instance.onClosePanel?.Invoke(UIPanelTypes.FailPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.StartPanel);
        }
        public void RetryButton()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanelTypes.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.StartPanel);
            CoreGameSignals.Instance.onReset?.Invoke();
        }
        public void EnterIdle()
        {
            
            CoreGameSignals.Instance.onChangeGameState?.Invoke(GameStates.Idle);
            NewCameraSignals.Instance.onChangeCameraState(CameraStates.Idle);
        }
    }
}