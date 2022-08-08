using System;
using System.Collections;
using System.Collections.Generic;
using Data.UnityObjects;
using Data.ValueObjects;
using Commands;
using Enums;
using Signals;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
  #region Self Variables

  #region Public Variables
  [Header("Data")] public SaveData Data;
   

  #endregion

  #region Serialized Variables


  #endregion

  #region Private Variables

  private SaveToDBCommand _saveToDBCommand;
  private InitializationSyncDatasCommand _initializationSyncDatasCommand;
  

    #endregion
  

    #endregion

    private void Awake()
    {
        Data = GetSaveData();
        _initializationSyncDatasCommand = new InitializationSyncDatasCommand();
        _saveToDBCommand = new SaveToDBCommand();
        _initializationSyncDatasCommand.OnInitializeSyncDatas(Data);
        SaveSignals.Instance.onSendDataToManagers?.Invoke(Data);

    }

    private SaveData GetSaveData() => Resources.Load<CD_Save>("Data/CD_Save").SaveData;

    #region Subscribe Events
    private void OnEnable()
    {
        SubscribeEvents();
    }private void OnDisable()
    {
        UnSubscribeEvents();
    }
    private void SubscribeEvents()
    {
        SaveSignals.Instance.onChangeSaveData+=OnChangeSaveData;
    }
    

    private void UnSubscribeEvents()
    {
        SaveSignals.Instance.onChangeSaveData-=OnChangeSaveData;
    }
    #endregion
    

    private void OnChangeSaveData(SaveTypes savetype, int saveAmount)
    {
        switch (savetype)
            {
                case SaveTypes.All:
                    Data.BonusColorman=saveAmount;
                    Data.CollectedColorman=saveAmount;
                    Data.CurrentLevel=saveAmount;
                    break;
                case SaveTypes.BonusColorman:
                    Data.BonusColorman=saveAmount;
                    break;
                case SaveTypes.CollectedColorman:
                    Data.CollectedColorman=saveAmount;
                    break;
                case SaveTypes.CurrentLevel:
                    Data.CollectedColorman=saveAmount;
                    break;
            
            }
            _saveToDBCommand.SaveDataToDatabase(Data);

            private BuildingData GetBuildingData() => Resources<BuildingData>.load("Data/CD_Level")
                .IdleLevels[SaveData.CurrentidleLEvel].BuildingList[ID];
            
    }

}
