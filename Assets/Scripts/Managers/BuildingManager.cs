using System;
using System.Threading.Tasks;
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
    public int SidePayedAmount;
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

    private async void Start()
    {
        await Task.Delay(1000);
        DecideMarketState();
    }

    private void DecideMarketState()
    {
        if (IsDepended)
        {
            if (IsSideObjectActive)
            {
                sideBuildingMarketController.gameObject.transform.parent.gameObject.SetActive(true);
                buildingMarketController.gameObject.transform.parent.gameObject.SetActive(false);
            }
           if(IsSideObjectActive&&IsCompleted)
            {
                sideBuildingMarketController.transform.parent.gameObject.SetActive(false);
            }

        }
        else if (IsCompleted)
        {
            buildingMarketController.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    public void UpgradeBuilding()
    {
        if (IsDepended&&IsSideObjectActive)
        {
            sideBuildingMarketController.UpgradeBuilding();
           
        }
        else
        {
            buildingMarketController.UpgradeBuilding();
        }
    }

    public async void NextBuilding()
    {
        
        buildingMarketController.gameObject.transform.parent.gameObject.SetActive(false);
        sideBuildingMarketController.gameObject.transform.parent.gameObject.SetActive(true);
        IsCompleted = false;
        IsSideObjectActive=true;
        PayedAmount = 0;
        TotalAmount = SideTotalAmount;
    }

    public void CloseBuilding()
    {
        IsCompleted = true;
        buildingMarketController.gameObject.transform.parent.gameObject.SetActive(false);
        if (IsDepended)
        {
             sideBuildingMarketController.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
    
    }
}