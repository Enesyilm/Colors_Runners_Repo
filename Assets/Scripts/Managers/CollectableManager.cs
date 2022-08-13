using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Enums;

public class CollectableManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    public ColorTypes CurrentColorType;
    public CollectableMeshController CollectableMeshController; 
    public CollectableMovementController CollectableMovementController; 
    public CollectablePhysicsController CollectablePhysicsController; 
    public CollectableAnimationController CollectableAnimationController; 

    

    #endregion

    #region Serialized Variables

    

    #endregion

    #region Private Variables

    

    #endregion
    #endregion
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeAnimationOnController(CollectableAnimationTypes _currentAnimation)
    {
        CollectableAnimationController.ChangeAnimation(_currentAnimation);
    }
    public void Death()
    {
        ChangeAnimationOnController(CollectableAnimationTypes.Death);
        Destroy(this,1f);
    }
}
