using Data.ValueObjects;

namespace Commands
{
    public class InitializationSyncDatasCommand
    {
        public void OnInitializeSyncDatas(SaveData _data)
        { 
            _data.BonusColorman=ES3.Load<int>("BonusColorman");
            _data.CollectedColorman=ES3.Load<int>("CollectedColorman");
            _data.CurrentLevel=ES3.Load<int>("CurrentLevel");
            
        }
    }
}