using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Controllers
{
    public class CollectableMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public Material materialdeneme;



        #endregion

        #region Serialized Variables

        [SerializeField] private CollectableManager collectableManager;

        #endregion

        #endregion

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

        public void CheckColorType(DroneColorAreaController _droneColorAreaRef)
        {
            if (collectableManager.CurrentColorType == _droneColorAreaRef.ColorType)
            {
                collectableManager.MatchType = MatchType.Match;
            }
            else
            {
                collectableManager.MatchType = MatchType.UnMatched;

            }
        }
    }
}    
    
    