// using System.Threading.Tasks;
// using Controllers;
// using UnityEngine;
//
//
// namespace Managers
// {
//     public class SideBuildingManager : MonoBehaviour
//
//     {
//
//     #region Self Variables
//
//     #region Public Variables
//
//     public int PayedAmount;
//     public int TotalAmount;
//     
//
//
//     #endregion
//
//     #region Serialized Variables
//         [SerializeField]
//         private BuildingMarketController buildingMarketController;
//         [SerializeField]
//         private SideBuildingMarketController sideBuildingMarketController;
//
//
//
//     #endregion
//
//     #region Private Variables
//
//
//     #endregion
//
//     #endregion
//
//     private async void Start()
//     {
//         await Task.Delay(1000);
//         DecideMarketState();
//     }
//
//     private void DecideMarketState()
//     {
//         if (IsDepended)
//         {
//             if (IsSideObjectActive)
//             {
//                 sideBuildingMarketController.gameObject.transform.parent.gameObject.SetActive(true);
//                 buildingMarketController.gameObject.transform.parent.gameObject.SetActive(false);
//             }
//            if(IsSideObjectActive&&IsCompleted)
//             {
//                 sideBuildingMarketController.transform.parent.gameObject.SetActive(false);
//             }
//
//         }
//         else if (IsCompleted)
//         {
//             buildingMarketController.gameObject.transform.parent.gameObject.SetActive(false);
//         }
//     }
//
//     public void UpgradeBuilding()
//     {
//         if (IsDepended&&IsSideObjectActive)
//         {
//             sideBuildingMarketController.UpgradeBuilding();
//            
//         }
//         else
//         {
//             buildingMarketController.UpgradeBuilding();
//         }
//     }
//
//     public async void NextBuilding()
//     {
//         
//         buildingMarketController.gameObject.transform.parent.gameObject.SetActive(false);
//         sideBuildingMarketController.gameObject.transform.parent.gameObject.SetActive(true);
//         IsCompleted = false;
//         IsSideObjectActive=true;
//         PayedAmount = -1;
//         TotalAmount = SideTotalAmount;
//     }
//
//     public void CloseBuilding()
//     {
//         IsCompleted = true;
//         buildingMarketController.gameObject.transform.parent.gameObject.SetActive(false);
//         if (IsDepended)
//         {
//              sideBuildingMarketController.gameObject.transform.parent.gameObject.SetActive(false);
//         }
//     }
//     
//     }
// }