using Data.ValueObjects;
namespace Commands
{
    public class SaveToDBCommand
    {
        public void SaveDataToDatabase(SaveData _data)
        {
            ES3.Save("Bonus",_data.Bonus);
            ES3.Save("TotalColorman",_data.TotalColorman);
            ES3.Save("Level",_data.Level);
            ES3.Save("IdleLevelListData",_data.IdleLevelListData);

        }
    }
}