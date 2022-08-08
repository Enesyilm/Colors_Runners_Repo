using Data.ValueObjects;
namespace Commands
{
    public class SaveToDBCommand
    {
        public void SaveDataToDatabase(SaveData _data)
        {
            ES3.Save("BonusColorman",_data.BonusColorman);
            ES3.Save("CollectedColorman",_data.CollectedColorman);
            ES3.Save("CurrentLevel",_data.CurrentLevel);
        }
    }
}