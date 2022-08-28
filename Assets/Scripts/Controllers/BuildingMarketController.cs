using System;
using Enums;
using Managers;
using Signals;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class BuildingMarketController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        

        #endregion

        #region Private Variables

        private TextMeshPro _marketText;
        private BuildingManager _buildingManager;
        private int _totalColorMan;
        private float _saturation;
        
        #endregion

        #endregion

        private void Awake()
        {
            _marketText = GetComponent<TextMeshPro>();
            _buildingManager = GetComponentInParent<BuildingManager>();
            
        }

        private void Start()
        {
            UpdateText();
            CalculateSaturation();
            
        }

        public void UpgradeBuilding()
        {
            if (CheckCanIncrease())
            {
                UpdateText();
                CalculateSaturation();
            }
        }

        private bool CheckCanIncrease()
        {
            //Score Managerden gelecek burasi
            if (/*SaveSignals.Instance.onGetIntSaveData(SaveTypes.TotalColorman) > 0*/true&&!_buildingManager.IsCompleted)
            {
                _totalColorMan--;
                _buildingManager.PayedAmount++;
                _buildingManager.PayedAmount++;
                if (_buildingManager.PayedAmount>=_buildingManager.TotalAmount)
                {
                    _buildingManager.IsCompleted = true;
                    if (_buildingManager.IsDepended&&!_buildingManager.IsSideObjectActive)
                    {
                        _buildingManager.NextBuilding();
                    }
                    else if(_buildingManager.IsDepended&&_buildingManager.IsSideObjectActive)
                    {
                        _buildingManager.CloseBuilding();
                    }
                    
                    
                    //ikinci market kontrollere gitsin
                    return true;
                }
                return true;

            }

            return false;

        }

        private void CalculateSaturation()
        {
            _saturation= (_buildingManager.PayedAmount != 0 && _buildingManager.TotalAmount != 0)
                ? _buildingManager.PayedAmount / _buildingManager.TotalAmount
                : 0;
        }

        private void UpdateText()
        {
            _marketText.text =_buildingManager.PayedAmount+"/"+_buildingManager.TotalAmount;
        }
    }
}