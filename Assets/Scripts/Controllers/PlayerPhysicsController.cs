using System;
using Enums;
using UnityEngine;
using Managers;
using Signals;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private new Collider collider;
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private GameObject playerObj;
        #endregion

        #region Private Variables
        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            
            if(other.CompareTag("DroneAreaPhysics"))
            {
                playerManager.RepositionPlayerForDrone(other.gameObject);
                playerManager.EnableVerticalMovement();
            }
             if (other.CompareTag("Portal"))
            {
                StackSignals.Instance.onColorChange?.Invoke(other.GetComponent<ColorController>().ColorType);
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("DroneArea"))
            {
                playerManager.StopVerticalMovement();
            }
            if (other.CompareTag("Roulette"))
            {
                CoreGameSignals.Instance.onChangeGameState?.Invoke(GameStates.Roulette);
                playerManager.StopAllMovement();
                playerManager.ActivateMesh();
                
            }
        }
    }

}
