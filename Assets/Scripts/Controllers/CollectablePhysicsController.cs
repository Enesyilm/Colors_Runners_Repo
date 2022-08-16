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

            if(CompareTag("Collected") && other.CompareTag("Collectable"))
            {
                CollectableManager _otherCollectableManager =other.transform.parent.GetComponent<CollectableManager>();
                if (_otherCollectableManager.CurrentColorType==collectableManager.CurrentColorType)
                {
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
            if (other.CompareTag("DroneArea")&&CompareTag("Collected"))
            {
                // transform.parent.DOMove(new Vector3(other.transform.localPosition.x,transform.position.y,
                //     Random.Range(other.transform.localScale.z/2,other.transform.localScale.z)),1f);
                collectableManager.DeListStack();
                
            }

            if (other.CompareTag("ColoredGround") && CompareTag("Collected"))
            {
                collectableManager.SetCollectablePositionOnDroneArea(other.gameObject.transform);
                if (collectableManager.CurrentColorType == other.GetComponent<DroneColorAreaController>().ColorType)
                {
                    collectableManager.MatchType = MatchType.Match;
                }
                else
                {
                    collectableManager.MatchType = MatchType.UnMatched;

                }
                tag = "Collectable";
            }
            // if (other.CompareTag("DroneColorArea"))
            // {
            //     // transform.parent.DOMove(new Vector3(other.transform.localPosition.x,transform.position.y,
            //     //     Random.Range(other.transform.localScale.z/2,other.transform.localScale.z)),1f);
            //     transform.parent.DOMove(new Vector3(other.transform.position.x,transform.position.y,transform.position.z+Random.Range(other.transform.localPosition.z/2,other.transform.localPosition.z)),1f);
            //     collectableManager.DeListStack();
            //     
            // }

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