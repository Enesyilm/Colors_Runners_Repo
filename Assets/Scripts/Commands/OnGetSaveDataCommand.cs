using Enums;
namespace Commands
{
    public class OnGetSaveDataCommand
    {
        public int OnGetSaveData(SaveTypes _saveType)
        {
            if (ES3.FileExists())
            {
                switch (_saveType)
                {
                    case SaveTypes.Bonus:
                        return ES3.Load<int>("Bonus");

                    case SaveTypes.Level:
                        return ES3.Load<int>("Level");

                    case SaveTypes.IdleLevel:
                        return ES3.Load<int>("IdleLevel");

                    case SaveTypes.TotalColorman:
                        return ES3.Load<int>("TotalColorman");
                    default:
                        return 0;
                
                } 
                
            }
            else{
                return 0;
            }
        }
    }
}