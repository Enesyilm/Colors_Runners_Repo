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

        private bool _isEnteredRoullette=false;
        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("DroneArea"))
            {
               playerManager.CloseScoreText(true);

            }

            if (other.CompareTag("Market"))
            {
                playerManager.ChangeAnimation(PlayerAnimationTypes.Throw);
            }
            
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
                if (_isEnteredRoullette==false)
                { 
                    ScoreSignals.Instance.onAddLevelTototalScore?.Invoke();
                    playerManager.StopAllMovement();
                    playerManager.ActivateMesh();
                    _isEnteredRoullette = true;
                }
                //other.GetComponent<Collider>().enabled = false;
            }
        }
    }

}
