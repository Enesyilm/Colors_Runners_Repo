using System.Collections.Generic;
using Data.ValueObjects;
using UnityEngine;


namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Level", menuName = "ColorsRunners/CD_Level", order = 0)]
    public class CD_Level : ScriptableObject
    {
        public LevelData LevelData;
    }
}