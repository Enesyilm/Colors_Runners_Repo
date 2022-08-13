using System;
using System.Security.Cryptography;
using DG.Tweening;
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
            if (CompareTag("Collected")&&other.CompareTag("Collectable"))
            {
                if (other.transform.parent.GetComponent<CollectableManager>().CurrentColorType==collectableManager.CurrentColorType)
                {
                    other.transform.parent.tag = "Collected";
                    StackSignals.Instance.onIncreaseStack?.Invoke(transform.parent.gameObject);
                }
                else
                {
                    Destroy(other.transform.parent);
                    StackSignals.Instance.onDecreaseStack?.Invoke( transform.parent.GetSiblingIndex());
                }
            }
            if (other.CompareTag("Obstacle"))
            {
                Destroy(other.transform.parent);
                
            }
            if (other.CompareTag("TurretArea"))
            {
                collectableManager.EnterTurretArea();
                
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
                collectableManager.ExitTurretArea();
            }
        }
    }
}