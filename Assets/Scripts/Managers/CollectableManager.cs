using System;
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

    [SerializeField]
    private CapsuleCollider collider;
    #endregion

    #region Private Variables

    

    #endregion
    #endregion

    private void Awake()
    {
        OnChangeColor(CurrentColorType);
    }

    public void DecreaseStack()
    {
        StackSignals.Instance.onDecreaseStack?.Invoke( transform.GetSiblingIndex());
        gameObject.transform.parent = null;
        DelayedDeath(false);
        
    }
    public void DeListStack()
    {
        //transform.parent = null;
        StackSignals.Instance.onDroneArea?.Invoke(transform.GetSiblingIndex());
        //StackSignals.Instance.onDecreaseStackOnDroneArea?.Invoke(transform.GetSiblingIndex());
    }
    public async void IncreaseStack()
    {
        //await Task.Delay(2000);
        StackSignals.Instance.onIncreaseStack?.Invoke(gameObject);
        ChangeAnimationOnController(CollectableAnimationTypes.Run);
    }
    public void ChangeAnimationOnController(CollectableAnimationTypes _currentAnimation)
    {
        CollectableAnimationController.ChangeAnimation(_currentAnimation);
    }

    public async void SetCollectablePositionOnDroneArea(Transform groundTransform)
    {
        ChangeAnimationOnController(CollectableAnimationTypes.Run);
        movementCommand.MoveToGround(groundTransform);
        ActivateOutline(false);
        await Task.Delay(3000);
        ActivateOutline(true);

    }

    public void OnChangeColor(ColorTypes colorType)
    {
        CurrentColorType = colorType;
        CollectableMeshController.ChangeCollectableMaterial(CurrentColorType);
    }
    private void ActivateOutline(bool _state)
    {
        CollectableMeshController.ActivateOutline(_state);
    }
    public async void DelayedDeath(bool _isDelayed)
    {
        if (_isDelayed)
        { 
        collider.enabled=false;
        await Task.Delay(3000);
        ChangeAnimationOnController(CollectableAnimationTypes.Death);
        Destroy(gameObject,3f);
        }
        else
        {
            ChangeAnimationOnController(CollectableAnimationTypes.Death);
            collider.enabled=false;
            Destroy(gameObject,.1f);
        }
    }

    public void CheckColorType(DroneColorAreaManager _droneColorAreaManager)
    {
        CollectableMeshController.CheckColorType(_droneColorAreaManager);
    }
    public void CheckMatchType(DroneColorAreaManager _droneColorAreaManager)
    {
        CollectableMeshController.CheckColorType(_droneColorAreaManager);
    }

    private void OnDestroy()
    {
        ChangeAnimationOnController(CollectableAnimationTypes.Death);
    }
}
