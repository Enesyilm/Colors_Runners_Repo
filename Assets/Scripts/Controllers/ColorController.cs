using DG.Tweening;
using Enums;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Controllers
{
    public class ColorController : MonoBehaviour
    {
        #region Self Variables


        #region Public Variables

        public ColorTypes ColorType;


        #endregion

        #region Serialized Variables

        [SerializeField]

        private MeshRenderer meshRenderer;
        

        #endregion

        #endregion

        private void Awake()
        {
            ChangeAreaColor(ColorType);
        }
        public void ChangeAreaColor(ColorTypes _colorType)
        {
            var colorHandler=Addressables.LoadAssetAsync<Material>($"PortalColors/Color_{_colorType}");
            meshRenderer.material = (colorHandler.WaitForCompletion() != null)?colorHandler.Result:null;
        }
        
    }
}