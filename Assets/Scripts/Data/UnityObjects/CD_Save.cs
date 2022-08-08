using UnityEngine;
using Data.ValueObjects;
namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Save", menuName = "ColorsRunners/CD_Save", order = 0)]
    public class CD_Save : ScriptableObject
    {
        public SaveData SaveData;
    }
}