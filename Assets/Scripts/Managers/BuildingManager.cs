using System;
using Controllers;
using Managers.Interface;
using Signals;
using UnityEngine;
using UnityEngine.TextCore;
using UnityEngine.XR;

namespace Managers
{
    public class BuildingManager : MonoBehaviour

    {

    #region Self Variables

    #region Public Variables

    public int PayedAmount;
    public int TotalAmount;
    public int SideTotalAmount;
    public bool IsDepended;
    public bool IsCompleted;
    public bool IsSideObjectActive;


    #endregion

    #region Serialized Variables
        [SerializeField]
        private BuildingMarketController buildingMarketController;
        [SerializeField]
        private BuildingMarketController sideBuildingMarketController;



    #endregion

    #region Private Variables


    #endregion

    #endregion

    private void Start()
    {
        DecideMarketState();
    }

    private void DecideMarketState()
    {
        if (IsDepended)
        {
            if (IsSideObjectActive)
            {
                sideBuildingMarketController.gameObject.SetActive(true);
                buildingMarketController.gameObject.SetActive(false);
            }
            if(IsSideObjectActive&&IsCompleted)
            {
                Debug.Log("DecideMarketState else if");
                sideBuildingMarketController.gameObject.SetActive(false);
            }
            else
            {
                sideBuildingMarketController.gameObject.SetActive(false);
                buildingMarketController.gameObject.SetActive(true);
            }
            
        }
    }

    public void UpgradeBuilding()
    {
        if (IsDepended)
        {
            if (IsSideObjectActive)
            {
                sideBuildingMarketController.UpgradeBuilding();

            }
            else
            {
                buildingMarketController.UpgradeBuilding();
            }
        }
    }

    public void NextBuilding()
    {
        IsCompleted = false;
        IsSideObjectActive=true;
        PayedAmount = 0;
        TotalAmount = SideTotalAmount;
        buildingMarketController.gameObject.SetActive(false);
        sideBuildingMarketController.gameObject.SetActive(true);
    }

    public void CloseBuilding()
    {
        IsCompleted = true;
        buildingMarketController.gameObject.SetActive(false);
        sideBuildingMarketController.gameObject.SetActive(false);
    }
    
    }
}