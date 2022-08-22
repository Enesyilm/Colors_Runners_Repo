using System;
using System.Collections.Generic;
using DG.Tweening;
using Enums;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Controllers
{
    public class CollectableMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables


        #endregion

        #region Serialized Variables

        [SerializeField] private CollectableManager collectableManager;

        [SerializeField]
        private SkinnedMeshRenderer meshRenderer;
        #endregion

        #endregion

        public void ChangeCollectableMaterial(ColorTypes _colorType)
        {

            var colorHandler=Addressables.LoadAssetAsync<Material>($"Collectable/Color_{_colorType}");
           
             if (colorHandler.WaitForCompletion() != null)
             {
                 meshRenderer.material = colorHandler.Result;
             }
            
        }

        public void CheckColorType(DroneColorAreaManager _droneColorAreaRef)
        {
            if (collectableManager.CurrentColorType == _droneColorAreaRef.CurrentColorType)
            {
                collectableManager.MatchType = MatchType.Match;
            }
            else
            {
                collectableManager.MatchType = MatchType.UnMatched;

            }
        }
        public void ActivateOutline(bool _isOutlineActive)
        {
            float _outlineValue = _isOutlineActive ? 71 : 0;
            meshRenderer.material.DOFloat(_outlineValue,"_OutlineSize",1f);
        }
    }
}    
    
    