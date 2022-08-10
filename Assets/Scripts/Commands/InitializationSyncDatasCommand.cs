using Data.ValueObjects;

namespace Commands
{
    public class InitializationSyncDatasCommand
    {
        public void OnInitializeSyncDatas(SaveData _data)
        {
            if (!ES3.FileExists())
            {
                _data.Level = ES3.Load<int>("Level", 0);
                _data.Bonus = ES3.Load<int>("Bonus", 0);
                _data.IdleLevel = ES3.Load<int>("IdleLevel", 0);
                _data.TotalColorman = ES3.Load<int>("TotalColorman", 0);
                // _data.CurrentLevel= ES3.KeyExists("CurrentLevel") ? ES3.Load<int>("Level", 0) : 0;
            }
        }
    }
}