using System;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class BuildingPhysicController : MonoBehaviour
    {
        #region Self Variables


        #region Private Variables

        private float _timer=0;
        private float _repeatOffset=0.1f;
        private BuildingManager _buildingManager;
        

        #endregion

        #endregion

        private void Awake()
        {
            _buildingManager = GetComponentInParent<BuildingManager>();
        }


        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _timer += Time.fixedDeltaTime;
                if (_timer >= _repeatOffset)
                {
                    _timer = 0;
                   
                    _buildingManager.UpgradeBuilding();
                }
                
            }
            }
            
        }
}