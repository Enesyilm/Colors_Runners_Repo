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
        public int TotalColorMan;

        

        #endregion

        #region Serialized Variables

        [SerializeField] 
        private List<MeshRenderer> meshRendererList; 

        #endregion

        #region Private Variables

        private TextMeshPro _marketText;
        private BuildingManager _buildingManager;
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
                ParticleSignals.Instance.onParticleBurst?.Invoke(transform.position);
                UpdateText();
                CalculateSaturation();
            }
        }

        private bool CheckCanIncrease()
        {
            Debug.Log("_buildingManager.IsCompleted");
            //Score Managerden gelecek burasi
            Debug.Log("TotalColorMan"+TotalColorMan);
            if (TotalColorMan>0&&!_buildingManager.IsCompleted)
            {
                TotalColorMan--;
                ScoreSignals.Instance.onChangeScore?.Invoke(ScoreTypes.DecScore,ScoreVariableType.TotalScore);
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
            ChangeSaturation();
        }

        private void ChangeSaturation()
        {
             foreach (var meshrenderer in meshRendererList)
             {
                 
                 meshrenderer.material.DOFloat(_saturation,"_Saturation",1f);
                 if(CompareTag("Market")&& _buildingManager.IsSideObjectActive)
                 {
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