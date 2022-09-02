using System;
using System.Threading.Tasks;
//using Controllers;
using Data.UnityObjects;
using Data.ValueObjects;
using Enums;
using Commands;
using Extentions;
using Managers.Abstracts.Concreates;
//using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

       

        #endregion

        #region Serialized Variables

        [Space] [SerializeField] private GameObject levelHolder;
        [Space] [SerializeField] private GameObject idleLevelHolder;
        
        

        #endregion

        #region Private Variables

         private int _levelID;
         private int _idleLevelID;
         private LevelLoaderCommand levelLoader;
         private IdleLevelLoaderCommand idleLevelLoader;
         private ClearActiveLevelCommand levelClearer;

        #endregion

        #endregion

        private void Awake()
        {
            
            _idleLevelID = GetActiveIdleLevel();
            GetCommandComponents();
        }

        

        private void GetCommandComponents()
        {
            levelLoader = GetComponent<LevelLoaderCommand>();
            idleLevelLoader = GetComponent<IdleLevelLoaderCommand>();
            levelClearer = GetComponent<ClearActiveLevelCommand>();
        }

        private int GetActiveLevel()
        {

            return SaveSignals.Instance.onGetIntSaveData.Invoke(SaveTypes.Level);
        }
        private int GetActiveIdleLevel()
        {
            return _idleLevelID;
        }
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnInitializeLevel;
            CoreGameSignals.Instance.onLevelIdleInitialize += OnInitializeIdleLevel;
            CoreGameSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onGetLevelID += OnGetLevelID;
            CoreGameSignals.Instance.onGetIdleLevelID += OnGetIdleLevelID;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnInitializeLevel;
            CoreGameSignals.Instance.onLevelIdleInitialize -= OnInitializeIdleLevel;
            CoreGameSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onGetLevelID -= OnGetLevelID;
            CoreGameSignals.Instance.onGetIdleLevelID -= OnGetIdleLevelID;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            _levelID = GetActiveLevel();
            OnInitializeLevel();
            OnInitializeIdleLevel();
        }

        private void OnNextLevel()
        {
            _levelID++;
            SaveSignals.Instance.onChangeSaveData?.Invoke(SaveTypes.Level,_levelID);
            CoreGameSignals.Instance.onReset?.Invoke();
        }
        private void OnNextIdleLevel()
        {
            _idleLevelID++;
            CoreGameSignals.Instance.onClearActiveIdleLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            SaveSignals.Instance.onChangeSaveData?.Invoke(SaveTypes.IdleLevel,_idleLevelID);
            CoreGameSignals.Instance.onLevelIdleInitialize?.Invoke();
        }

        private async  void OnReset()
        {
            await Task.Delay(50);
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            SaveSignals.Instance.onChangeSaveData?.Invoke(SaveTypes.Level,_levelID);
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
            //await Task.Delay(50);
            CoreGameSignals.Instance.onLevelIdleInitialize?.Invoke();
        }

        private int OnGetLevelID()
        {
            return _levelID;
        }
        private int OnGetIdleLevelID()
        {
            return _idleLevelID;
        }


        private void OnInitializeLevel()
        {
            var newLevelData = _levelID % Resources.Load<CD_Level>("Data/CD_Level").LevelData.LevelAmount;
            levelLoader.InitializeLevel(newLevelData, levelHolder.transform);
        }
        private void OnInitializeIdleLevel()
        {
            var newLevelData = _idleLevelID % Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelListData.IdleLevelData.Count;
            idleLevelLoader.InitializeIdleLevel(newLevelData, idleLevelHolder.transform);
        }

        private void OnClearActiveLevel()
        {
            OnClearActiveIdleLevel();
            levelClearer.ClearActiveLevel(levelHolder.transform);
        }
        private void OnClearActiveIdleLevel()
        {
            levelClearer.ClearActiveLevel(idleLevelHolder.transform);
        }
    }
}