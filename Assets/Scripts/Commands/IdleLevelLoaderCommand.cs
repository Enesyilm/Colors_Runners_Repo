using Signals;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Commands
{
    public class IdleLevelLoaderCommand : MonoBehaviour
    {
        public void InitializeLevel(int _levelID, Transform levelHolder)
        {
            Instantiate(Resources.Load<GameObject>($"Prefabs/IdleLevelPrefabs/level {_levelID}"), levelHolder);
        }
    }
}