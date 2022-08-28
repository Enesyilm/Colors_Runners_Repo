using System.Collections.Generic;
using Data.ValueObjects;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_IdleLevel", menuName = "ColorsRunners/CD_IdleLevel", order = 0)]
    public class CD_IdleLevel : ScriptableObject
    {
        public IdleLevelListData IdleLevelListData;
      
    }
}