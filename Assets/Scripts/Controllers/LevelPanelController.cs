using TMPro;
using UnityEngine;

namespace Controllers
{
    public class LevelPanelController:MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables

        [SerializeField] private TextMeshProUGUI levelText;

        #endregion

        #region Private Variables
        
        #endregion
        
        #endregion

        public void SetLevelText(int value)
        {
            int _levelValue=value + 1;
            Debug.Log("_levelValue"+_levelValue);
            levelText.text = "LEVEL " + (_levelValue + 1).ToString();
        }
    }
}