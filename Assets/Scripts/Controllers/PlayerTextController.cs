using UnityEngine;
using TMPro;

namespace Controllers
{
    public class PlayerTextController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        #endregion

        #region Serialized Variables

        [SerializeField] private TextMeshPro playerScoreText;

        #endregion

        #region Private Variables
        #endregion

        #endregion

        public void UpdatePlayerScore(float totalScore)
        {
            playerScoreText.text = totalScore.ToString();
        }
    }
}

