using Signals;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Commands
{
    public class LevelLoaderCommand : MonoBehaviour
    {
        public void InitializeLevel(int _levelID, Transform levelHolder)
        {
            Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/level {_levelID}"), levelHolder);
            CoreGameSignals.Instance.onGameInitLevel?.Invoke();
        }
    }
}