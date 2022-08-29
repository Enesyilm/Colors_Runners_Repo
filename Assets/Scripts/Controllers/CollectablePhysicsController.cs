using System;
using System.Security.Cryptography;
using DG.Tweening;
using Enums;
using Managers;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class CollectablePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField]
        private CollectableManager manager;
        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {

            if (CompareTag("Collected"))
            {
                if( other.CompareTag("Collectable"))
                {
                    CollectableManager _otherCollectableManager =other.transform.parent.GetComponent<CollectableManager>();
                    if (_otherCollectableManager.CurrentColorType==manager.CurrentColorType)
                    {
                        other.transform.tag = "Collected";
                        _otherCollectableManager.IncreaseStack();
                    }
                    else
                    {
                        Destroy(other.transform.parent.gameObject);
                        manager.DecreaseStack();
                    }
                }

                
                if (other.CompareTag("Obstacle"))
                {
                    Destroy(other.transform.parent.gameObject);
                    manager.DecreaseStack();
                
                }
                if (other.CompareTag("TurretAreaGround"))
                {
                    TurretAreaController _turretAreaController=other.GetComponent<TurretAreaController>();
                    TurretAreaManager _turretAreaManager=other.GetComponentInParent<TurretAreaManager>();
                    manager.ChangeAnimationOnController(CollectableAnimationTypes.CrouchRun);
                    if(manager.CurrentColorType!=_turretAreaController.ColorType)
                    {
                        _turretAreaManager.AddTargetToList(transform.parent.gameObject);
                    }
                }
                if (other.CompareTag("ColoredGround"))
                {
                    manager.DeListStack();
                    manager.SetCollectablePositionOnDroneArea(other.gameObject.transform);//ucu ayni fonksiyonda tetiklenecek
                    manager.CheckColorType(other.GetComponent<DroneColorAreaManager>());
                    tag = "Untagged";
                }
            }
            if (other.CompareTag("Roulette"))
            {
                manager.DecreaseStackOnIdle();
            }
            if (other.CompareTag("DroneAreaPhysics"))
            {
                tag = "Collected";
                if (manager.MatchType == MatchType.Match)
                {
                    manager.ChangeOutlineState(true);
                    manager.IncreaseStack();
                    
                }
                else
                {
                    manager.DelayedDeath(true);
                    
                }
            }
             
        }
        private void OnTriggerExit(Collider other)
        {
            if (CompareTag("Collected"))
            {
                if (other.CompareTag("TurretAreaGround"))
                {
                    gameObject.tag = "Collected";
                    manager.ChangeAnimationOnController(CollectableAnimationTypes.Run);
                }

                if (other.CompareTag("DroneAreaPhysics"))
                {
                    manager.ChangeAnimationOnController(CollectableAnimationTypes.Run);
                }
            }
            
            
        } 

    }
}