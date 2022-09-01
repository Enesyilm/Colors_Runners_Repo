using Data.ValueObjects;
using Enums;
using UnityEngine;

namespace Commands
{
    public class OnGetSaveDataCommand
    {
        public int GetIntSaveData(SaveTypes _saveType)
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

            return 0;
        }

        public IdleLevelListData GetIdleLevelData()
        {
            if (ES3.FileExists())
            {
                return ES3.Load<IdleLevelListData>("IdleLevelListData");
            }

           return new IdleLevelListData();
        }
    }
}