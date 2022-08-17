using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public async void IncreaseStack()
    {
        await Task.Delay(3000);
        StackSignals.Instance.onIncreaseStack?.Invoke(gameObject);
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
    public async void Death()
    {
        //await Task.Delay(3500);
        ChangeAnimationOnController(CollectableAnimationTypes.Death);
        Destroy(gameObject,3f);
    }

    public void CheckColorType(DroneColorAreaController _droneColorAreaController)
    {
        CollectableMeshController.CheckColorType(_droneColorAreaController);
    }
    public void CheckMatchType(DroneColorAreaController _droneColorAreaController)
    {
        CollectableMeshController.CheckColorType(_droneColorAreaController);
    }
}
