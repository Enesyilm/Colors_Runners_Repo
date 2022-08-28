using System;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class TurretAreaPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField]
        private TurretAreaManager turretAreaManager;
        

        #endregion
        

        #endregion
       
        private void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("Collected"))
            {
                turretAreaManager.ResetTurretArea();

            }
        }
    }
}