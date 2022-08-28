using Enums;
using UnityEngine;

namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables
        #region Serialized Variables
        [SerializeField] 
        private Animator animator;
        #endregion
        #endregion

        public void ChangeAnimation(PlayerAnimationTypes _animationType)
        {
            animator.SetTrigger(_animationType.ToString());
        }
    }
}