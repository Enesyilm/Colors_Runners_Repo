using System;
using System.Security.Cryptography;
using DG.Tweening;
using Enums;
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
                    Destroy(other.transform.parent);
                
                }
                if (other.CompareTag("TurretArea"))
                {
                    collectableManager.ChangeAnimationOnController(CollectableAnimationTypes.CrouchRun);
                
                }
                if (other.CompareTag("ColoredGround"))
                {
                    collectableManager.DeListStack();
                    collectableManager.SetCollectablePositionOnDroneArea(other.gameObject.transform);//ucu ayni fonksiyonda tetiklenecek
                    collectableManager.CheckColorType(other.GetComponent<DroneColorAreaController>());
                    tag = "Collectable";
                }
                if (other.CompareTag("Bullet"))
                {
                    collectableManager.Death();
                    StackSignals.Instance.onDecreaseStack?.Invoke(transform.parent.GetSiblingIndex());
                    Destroy(gameObject, 1f);
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
                    collectableManager.Death();
                    
                }
            }
                
        }
        private void OnTriggerExit(Collider other)
        {
            if (CompareTag("Collected"))
            {
                if (other.CompareTag("TurretArea")||other.CompareTag("DroneAreaPhysics"))
                {
                    collectableManager.ChangeAnimationOnController(CollectableAnimationTypes.Run);
                }
            }
            
        } 

    }
}