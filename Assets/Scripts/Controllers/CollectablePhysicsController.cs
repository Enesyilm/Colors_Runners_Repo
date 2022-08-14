using System;
using System.Security.Cryptography;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;

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
            Debug.Log("OnTriggerEnter"+other.tag+"Mine tag "+this.tag);

            if(CompareTag("Collected") && other.CompareTag("Collectable"))
            {
                Debug.Log("1. if OnTriggerEnter");
               CollectableManager _otherCollectableManager =other.transform.parent.GetComponent<CollectableManager>();
                if (_otherCollectableManager.CurrentColorType==collectableManager.CurrentColorType)
                {
                    Debug.Log("if Getcompeonent calisti");

                    other.transform.tag = "Collected";
                    _otherCollectableManager.IncreaseStack(other.transform.parent.gameObject);
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

            if (other.CompareTag("Bullet"))
            {
                collectableManager.Death();
                StackSignals.Instance.onDecreaseStack?.Invoke(transform.parent.GetSiblingIndex());
                Destroy(gameObject, 1f);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("TurretArea"))
            {
                collectableManager.ChangeAnimationOnController(CollectableAnimationTypes.Run);
            }
        }
    }
}