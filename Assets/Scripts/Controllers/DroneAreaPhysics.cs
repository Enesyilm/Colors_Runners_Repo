using System;
using Enums;
using UnityEngine;

namespace Controllers
{
    
    public class DroneAreaPhysics:MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField]
        private DroneColorAreaManager droneColorAreaManager;
    

        #endregion
    

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collected"))
            {
                if (other.GetComponentInParent<CollectableManager>().CurrentColorType == droneColorAreaManager.CurrentColorType)
                {
                    droneColorAreaManager.matchType = MatchType.Match;
                }
                
            }
        }
    }
}