using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        [SerializeField] 
        private CollectableManager collectableManager;

        [SerializeField]
        private SkinnedMeshRenderer meshRenderer;
        #endregion

        #endregion

        public void ChangeCollectableMaterial(ColorTypes _colorType)
        {

            var colorHandler=Addressables.LoadAssetAsync<Material>($"Collectable/Color_{_colorType}");
            meshRenderer.material = (colorHandler.WaitForCompletion() != null)?colorHandler.Result:null;
        }

        public void CheckColorType(DroneColorAreaManager _droneColorAreaRef)
        {
            collectableManager.MatchType = (collectableManager.CurrentColorType == _droneColorAreaRef.CurrentColorType)
                ? MatchType.Match
                : MatchType.UnMatched;
        }
        public async void ActivateOutline(bool _isOutlineActive)
        {
            //await Task.Delay(2000);
            float _outlineValue = _isOutlineActive ? 71 : 0;
            meshRenderer.material.DOFloat(_outlineValue,"_OutlineSize",1f);
        }
    }
}    
    
    