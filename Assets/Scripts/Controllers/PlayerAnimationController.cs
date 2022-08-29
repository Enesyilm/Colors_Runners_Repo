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
            if (_animationType==PlayerAnimationTypes.Run)
            {
                animator.Play("Running");
            }
            else
            {
                animator.SetTrigger(_animationType.ToString());
                
            }
        }
    }
}