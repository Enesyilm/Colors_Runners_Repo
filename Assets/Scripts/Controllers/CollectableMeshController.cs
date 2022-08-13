using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
namespace Controllers
{
    public class CollectableMeshController : MonoBehaviour
    {
        public Material materialdeneme;
        private void Awake()
        {
            ChangeCollectableMaterial(1);
        }
        public void ChangeCollectableMaterial(int index)
        {
            
            // var colorHandler=Addressables.LoadAssetAsync<Material>($"Color_{0}");
            //
            //  if (colorHandler.WaitForCompletion() != null)
            //  {
            //      Debug.Log("Color geldi");
            //      GetComponent<SkinnedMeshRenderer>().material = colorHandler.Result;
            //  }
            //  Debug.Log("Color gelmedi");
            //  // GetComponent<SkinnedMeshRenderer>().material=Resources.Load<>()
        }
    }
}