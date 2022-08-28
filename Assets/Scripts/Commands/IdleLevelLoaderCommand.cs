using Signals;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Commands
{
    public class IdleLevelLoaderCommand : MonoBehaviour
    {
        public void InitializeIdleLevel(int _idleLevelID, Transform levelHolder)
        {
            Instantiate(Resources.Load<GameObject>($"Prefabs/IdleLevelPrefabs/IdleLevel {_idleLevelID}"), levelHolder);
            CoreGameSignals.Instance.onGameInit?.Invoke();

        }
    }
}