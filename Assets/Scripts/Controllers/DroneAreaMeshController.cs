using Enums;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Controllers
{
    public class DroneAreaMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private MeshRenderer meshRenderer;
        

        #endregion

        #endregion
        public void ChangeAreaColor(ColorTypes _colorType)
        {
            var colorHandler=Addressables.LoadAssetAsync<Material>($"CoreColor/Color_{_colorType}");
            meshRenderer.material = (colorHandler.WaitForCompletion() != null)?colorHandler.Result:null;
        }
    }
}