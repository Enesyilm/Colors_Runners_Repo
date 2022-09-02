using System;
using System.Collections.Generic;
using Data.ValueObjects;
using Enums;
using Managers.Interface;
using Signals;
using UnityEngine;

namespace Managers
{
    public class IdleManager : MonoBehaviour,ISaveable
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        [SerializeField]
        private List<BuildingManager> buildingList;


        #endregion

        #region Private Variables

        private IdleLevelListData _idleLevelListData;
        private int _currentIdleLevelId;
        private IdleLevelData _currentIdleLevelData;

        #endregion

        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveSignals.Instance.onApplicationPause += SendDataToSaveManager;
            //CoreGameSignals.Instance.onNextLevel += SendDataToSaveManager;
            SaveSignals.Instance.onSendDataToManagers += OnGetSaveData;
        }
        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            //CoreGameSignals.Instance.onNextLevel -= SendDataToSaveManager;
            SaveSignals.Instance.onApplicationPause -= SendDataToSaveManager;
            SaveSignals.Instance.onSendDataToManagers -= OnGetSaveData;

        }


        #endregion

        private void Start()
        {
            
        }

        private void OnGetSaveData(SaveData _saveData)
        {
            Debug.Log("OnGetSaveData calisti");
            _currentIdleLevelId=CoreGameSignals.Instance.onGetIdleLevelID.Invoke();
            _idleLevelListData = _saveData.IdleLevelListData;
            SyncDataToBuildings();
        }

        private void SyncDataToBuildings()
        {
            _currentIdleLevelData=_idleLevelListData.IdleLevelData[_currentIdleLevelId];
            for (int i = 0; i < _currentIdleLevelData.IdleBuildingData.Count; i++)
            {
                IdleBuildingData _loadedbuilding=_currentIdleLevelData.IdleBuildingData[i];
                    buildingList[i].IsDepended=_loadedbuilding.IsDepended;
                    buildingList[i].IsCompleted = _loadedbuilding.IsCompleted;
                    buildingList[i].IsSideObjectActive=_loadedbuilding.IsSideObjectActive;
                    if (_loadedbuilding.IsDepended)
                    {
                        buildingList[i].SideTotalAmount= _loadedbuilding.SideObjectData.TotalRequiredAmount;
                    }
                    buildingList[i].PayedAmount = _loadedbuilding.PayedAmount;
                    Debug.Log("buildingList[i].LoadPayedAmount;"+_loadedbuilding.PayedAmount);
                    buildingList[i].TotalAmount = _loadedbuilding.TotalRequiredAmount;
                    if (_loadedbuilding.IsDepended&&_loadedbuilding.IsSideObjectActive)
                    {
                        buildingList[i].IsCompleted = _loadedbuilding.SideObjectData.IsCompleted;
                        buildingList[i].PayedAmount = _loadedbuilding.SideObjectData.PayedAmount;
                        buildingList[i].TotalAmount = _loadedbuilding.SideObjectData.TotalRequiredAmount;
                    }

                    
            }
        }
        private void PrepareSaveData()
        {
            _currentIdleLevelData=_idleLevelListData.IdleLevelData[_currentIdleLevelId];
            for (int i = 0; i < _currentIdleLevelData.IdleBuildingData.Count; i++)
            {
                IdleBuildingData _savedbuilding=_currentIdleLevelData.IdleBuildingData[i];
                _currentIdleLevelData.IdleBuildingData[i].IsDepended=buildingList[i].IsDepended;
                _currentIdleLevelData.IdleBuildingData[i].IsCompleted = buildingList[i].IsCompleted;
                _currentIdleLevelData.IdleBuildingData[i].IsSideObjectActive=buildingList[i].IsSideObjectActive;
                _currentIdleLevelData.IdleBuildingData[i].PayedAmount = buildingList[i].PayedAmount;
                Debug.Log("buildingList[i].SaavePayedAmount;"+buildingList[i].PayedAmount);
                _currentIdleLevelData.IdleBuildingData[i].TotalRequiredAmount = buildingList[i].TotalAmount;
                if (buildingList[i].IsDepended&&buildingList[i].IsSideObjectActive)
                {
                   _currentIdleLevelData.IdleBuildingData[i].SideObjectData.IsCompleted = buildingList[i].IsCompleted;
                   _currentIdleLevelData.IdleBuildingData[i].SideObjectData.PayedAmount = buildingList[i].PayedAmount;
                   _currentIdleLevelData.IdleBuildingData[i].TotalRequiredAmount = buildingList[i].TotalAmount;
                }
            }

            _idleLevelListData.IdleLevelData[_currentIdleLevelId]=_currentIdleLevelData;

            
        }
        public void SendDataToSaveManager()
        {
            PrepareSaveData();
            SaveSignals.Instance.onChangeIdleLevelListData?.Invoke(_idleLevelListData);
            
        }

    }
}