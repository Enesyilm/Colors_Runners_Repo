using System;
//using Controllers;
using Data.UnityObjects;
using Data.ValueObjects;
using Enums;
using Commands;
using Extentions;
//using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("CurrentLevelPrefab")] public LevelData Data;

        #endregion

        #region Serialized Variables

        [Space] [SerializeField] private GameObject levelHolder;
        
        

        #endregion

        #region Private Variables

         private int _levelID;
         private LevelLoaderCommand levelLoader;
         private ClearActiveLevelCommand levelClearer;

        #endregion

        #endregion

        private void Awake()
        {
            _levelID = GetActiveLevel();
            Data = GetLevelData();
            GetCommandComponents();
        }

        private void GetCommandComponents()
        {
            levelLoader = GetComponent<LevelLoaderCommand>();
            levelClearer = GetComponent<ClearActiveLevelCommand>();
        }

        private int GetActiveLevel()
        {

            return SaveSignals.Instance.onGetSaveData.Invoke(SaveTypes.Level);
        }

        private LevelData GetLevelData()
        {
            var newLevelData = _levelID % Resources.Load<CD_Level>("Data/CD_Level").LevelData.Count;
            return Resources.Load<CD_Level>("Data/CD_Level").LevelData[newLevelData];
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnInitializeLevel;
            CoreGameSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
            CoreGameSignals.Instance.onGetLevelID += OnGetLevelID;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnInitializeLevel;
            CoreGameSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
            CoreGameSignals.Instance.onGetLevelID -= OnGetLevelID;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            OnInitializeLevel();
        }

        private void OnNextLevel()
        {
            _levelID++;
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            SaveSignals.Instance.onChangeSaveData?.Invoke(SaveTypes.Level,_levelID);
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
        }

        private void OnRestartLevel()
        {
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            SaveSignals.Instance.onChangeSaveData?.Invoke(SaveTypes.Level,_levelID);
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
        }

        private int OnGetLevelID()
        {
            return _levelID;
        }


        private void OnInitializeLevel()
        {
            var newLevelData = _levelID % Resources.Load<CD_Level>("Data/CD_Level").LevelData.Count;
            Debug.Log("NewlevelDAta"+newLevelData);
            levelLoader.InitializeLevel(newLevelData, levelHolder.transform);
        }

        private void OnClearActiveLevel()
        {
            levelClearer.ClearActiveLevel(levelHolder.transform);
        }
    }
}