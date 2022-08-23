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
            Debug.Log("_idleLevel"+_idleLevelID);
            Instantiate(Resources.Load<GameObject>($"Prefabs/IdleLevelPrefabs/IdleLevel {_idleLevelID}"), levelHolder);
        }
    }
}