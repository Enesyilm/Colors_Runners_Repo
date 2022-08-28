using Data.UnityObjects;
using Data.ValueObjects;
using UnityEngine;

namespace Commands
{
    public class InitializationSyncDatasCommand
    {
        private IdleLevelListData GetIdleLevelListData() => Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelListData;
        public void OnInitializeSyncDatas(SaveData _data)
        {
            if (ES3.FileExists())
            {
                _data.Level = ES3.Load<int>("Level", 0);
                _data.Bonus = ES3.Load<int>("Bonus", 0);
                _data.IdleLevel = ES3.Load<int>("IdleLevel", 0);
                _data.TotalColorman = ES3.Load<int>("TotalColorman", 0);
                _data.IdleLevelListData = ES3.Load<IdleLevelListData>("IdleLevelListData");
                
                // _data.CurrentLevel= ES3.KeyExists("CurrentLevel") ? ES3.Load<int>("Level", 0) : 0;
            }
            else
            {
                ES3.Save("Level",0);
                ES3.Save("Bonus",0);
                ES3.Save("IdleLevel",0);
                ES3.Save("TotalColorman",0);
                ES3.Save("IdleLevelListData",GetIdleLevelListData());
                OnInitializeSyncDatas(_data);
                

            }
        }

        
    }
}