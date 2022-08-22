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
        private CollectableManager collectableManager;
        
        

        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {

            if (CompareTag("Collected"))
            {
                if( other.CompareTag("Collectable"))
                {
                    CollectableManager _otherCollectableManager =other.transform.parent.GetComponent<CollectableManager>();
                    if (_otherCollectableManager.CurrentColorType==collectableManager.CurrentColorType)
                    {
                        other.transform.tag = "Collected";
                        _otherCollectableManager.IncreaseStack();
                    }
                    else
                    {
                        Destroy(other.transform.parent.gameObject);
                        collectableManager.DecreaseStack();
                    }
                }
                if (other.CompareTag("Obstacle"))
                {
                    Destroy(other.transform.parent.gameObject);
                    collectableManager.DecreaseStack();
                
                }
                if (other.CompareTag("TurretAreaGround"))
                {
                    TurretAreaController _turretAreaController=other.GetComponent<TurretAreaController>();
                    TurretAreaManager _turretAreaManager=other.GetComponentInParent<TurretAreaManager>();
                    collectableManager.ChangeAnimationOnController(CollectableAnimationTypes.CrouchRun);
                    if(collectableManager.CurrentColorType!=_turretAreaController.colorType)
                    {
                        _turretAreaManager.AddTargetToList(transform.parent.gameObject);
                    }
                }
                if (other.CompareTag("ColoredGround"))
                {
                    collectableManager.DeListStack();
                    collectableManager.SetCollectablePositionOnDroneArea(other.gameObject.transform);//ucu ayni fonksiyonda tetiklenecek
                    collectableManager.CheckColorType(other.GetComponent<DroneColorAreaManager>());
                    tag = "Untagged";
                }
            }
            if (other.CompareTag("DroneAreaPhysics"))
            {
                if (collectableManager.MatchType == MatchType.Match)
                {
                    collectableManager.IncreaseStack();
                }
                else
                {
                    collectableManager.DelayedDeath(true);
                    
                }
            }
            if (other.CompareTag("DroneAreaPhysics"))
            {
                tag = "Collected";
            }
                
        }
        private void OnTriggerExit(Collider other)
        {
            

            if (other.CompareTag("TurretAreaGround"))
            {
                gameObject.tag = "Collected";
                collectableManager.ChangeAnimationOnController(CollectableAnimationTypes.Run);
            }

            if (other.CompareTag("DroneAreaPhysics"))
            {
                collectableManager.ChangeAnimationOnController(CollectableAnimationTypes.Run);
            }
            
        } 

    }
}