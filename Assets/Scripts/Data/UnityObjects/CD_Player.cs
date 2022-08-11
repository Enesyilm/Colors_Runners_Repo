using UnityEngine;
using Data.ValueObjects;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Player", menuName = "ColorsRunners/CD_Player", order = 0)]
    public class CD_Player : ScriptableObject
    {
        public PlayerData PlayerData;
    }
}


