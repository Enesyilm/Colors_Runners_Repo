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
  private OnGetSaveDataCommand _getSaveDataCommand;
  

    #endregion
  

    #endregion

    private void Awake()
    {
        Data = GetSaveData();
        _initializationSyncDatasCommand = new InitializationSyncDatasCommand();
        _saveToDBCommand = new SaveToDBCommand();
        _getSaveDataCommand = new OnGetSaveDataCommand();
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
        SaveSignals.Instance.onGetSaveData+=OnGetSaveData;
    }

    


    private void UnSubscribeEvents()
    {
        SaveSignals.Instance.onChangeSaveData-=OnChangeSaveData;
        SaveSignals.Instance.onGetSaveData-=OnGetSaveData;

    }

    private int OnGetSaveData(SaveTypes _type)
    {
        return _getSaveDataCommand.OnGetSaveData(_type);
    }

    #endregion
    

    private void OnChangeSaveData(SaveTypes savetype, int saveAmount)
    {
        switch (savetype)
            {
                case SaveTypes.All:
                    Data.Bonus=saveAmount;
                    Data.TotalColorman=saveAmount;
                    Data.Level=saveAmount;
                    Data.IdleLevel = saveAmount;
                    break;
                case SaveTypes.Bonus:
                    Data.Bonus=saveAmount;
                    break;
                case SaveTypes.TotalColorman:
                    Data.TotalColorman=saveAmount;
                    break;
                case SaveTypes.Level:
                    Data.Level=saveAmount;
                    break;
                case SaveTypes.IdleLevel:
                    Data.IdleLevel=saveAmount;
                    break;
            
            }
        _saveToDBCommand.SaveDataToDatabase(Data);



    }
    
}
