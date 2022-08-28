using System;

namespace Data.ValueObjects
{
    [Serializable]
    public class SaveData
    {
        public int TotalColorman;
        public int Level;
        public int IdleLevel;
        public IdleLevelListData IdleLevelListData;
        public int Bonus;
    }
}