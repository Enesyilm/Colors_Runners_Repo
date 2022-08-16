using System.Collections;
using System.Collections.Generic;
using Commands;
using UnityEngine;
using Controllers;
using Enums;
using Signals;

public class CollectableManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    public ColorTypes CurrentColorType;
    public CollectableMeshController CollectableMeshController; 
    public CollectableMovementController CollectableMovementController; 
    public CollectablePhysicsController CollectablePhysicsController; 
    public CollectableAnimationController CollectableAnimationController;
    public MatchType MatchType;


    #endregion

    #region Serialized Variables

    [SerializeField] private CollectableMovementCommand movementCommand;
    #endregion

    #region Private Variables

    

    #endregion
    #endregion
    public void DecreaseStack()
    {
        StackSignals.Instance.onDecreaseStack?.Invoke( transform.GetSiblingIndex());
        Destroy(gameObject);
    }
    public void DeListStack()
    {
        //transform.parent = null;
        StackSignals.Instance.onDroneArea?.Invoke(transform.GetSiblingIndex());
        //StackSignals.Instance.onDecreaseStackOnDroneArea?.Invoke(transform.GetSiblingIndex());
    }
    public void IncreaseStack(GameObject other)
    {
        StackSignals.Instance.onIncreaseStack?.Invoke(other);
        ChangeAnimationOnController(CollectableAnimationTypes.Run);
    }
    public void ChangeAnimationOnController(CollectableAnimationTypes _currentAnimation)
    {
        CollectableAnimationController.ChangeAnimation(_currentAnimation);
    }

    public void SetCollectablePositionOnDroneArea(Transform groundTransform)
    {
        ChangeAnimationOnController(CollectableAnimationTypes.Run);
        movementCommand.MoveToGround(groundTransform);
    }

    private void OnChangeColor(ColorTypes colorType)
    {
        CurrentColorType = colorType;
        //CollectableMeshController.ChangeCollectableMaterial();
    }
    public void Death()
    {
        ChangeAnimationOnController(CollectableAnimationTypes.Death);
        Destroy(gameObject,5f);
    }
}
