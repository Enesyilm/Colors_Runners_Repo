using UnityEngine;
using Managers;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private new Collider collider;
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private GameObject playerObj;
        [SerializeField] private GameObject collectedObjHolder;
        #endregion

        #region Private Variables
        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Finish"))
            {
                //UISignals.Instance.onOpenPanel?.Invoke(UIPanels.IdlePanel);
            }
        }

    }

}
