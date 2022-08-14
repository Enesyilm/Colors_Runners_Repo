using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Controllers
{
    public class UIPanelController: MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private List<GameObject> panels;

        #endregion

        #region Private Variables
        
        #endregion

        #endregion

        public void OpenPanel(UIPanelTypes panelParam)
        {
            panels[(int)panelParam].SetActive(true);
        }

        public void ClosePanel(UIPanelTypes panelParam)
        {
            panels[(int)panelParam].SetActive(false);
        }
    }
}