using System;
using System.Collections.Generic;
using DG.Tweening;
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

        [SerializeField] 
        private List<MeshRenderer> meshRendererList; 

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
            _buildingManager = transform.parent.GetComponentInParent<BuildingManager>();
            
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
            Debug.Log("_buildingManager.IsCompleted");
            //Score Managerden gelecek burasi
            if (/*SaveSignals.Instance.onGetIntSaveData(SaveTypes.TotalColorman) > 0*/true&&!_buildingManager.IsCompleted)
            {
                _totalColorMan--;
               
                    _buildingManager.PayedAmount++;
                    if (_buildingManager.PayedAmount>=_buildingManager.TotalAmount)
                {
                    _buildingManager.IsCompleted = true;
                    if (_buildingManager.IsDepended&&!_buildingManager.IsSideObjectActive)
                    {
                        _buildingManager.NextBuilding();
                    }
                    else
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
            _saturation=((float)_buildingManager.PayedAmount / _buildingManager.TotalAmount) * 2;
            Debug.Log("tag"+gameObject.tag+" id"+_buildingManager.PayedAmount+" _saturation"+_saturation);
            ChangeSaturation();
        }

        private void ChangeSaturation()
        {
             foreach (var meshrenderer in meshRendererList)
             {
                 
                 meshrenderer.material.DOFloat(_saturation,"_Saturation",1f);
                 if(CompareTag("Market")&& _buildingManager.IsSideObjectActive)
                 {
                     Debug.Log("Calisti kill");
                     // DOTween.Kill(meshrenderer.material);
                     meshrenderer.material.DOFloat(2,"_Saturation",1f);
                 }
                 

             }
        }

        private void UpdateText()
        {
            
                _marketText.text =_buildingManager.PayedAmount+"/"+_buildingManager.TotalAmount;
            
          
        }
    }
}