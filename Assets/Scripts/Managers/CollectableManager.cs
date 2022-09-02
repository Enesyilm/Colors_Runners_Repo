using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Commands;
using UnityEngine;
using Controllers;
using DG.Tweening;
using Enums;
using Signals;

public class CollectableManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    public ColorTypes CurrentColorType;
    public MatchType MatchType;


    #endregion

    #region Serialized Variables

    [SerializeField] 
    private CollectableMovementCommand movementCommand;
    [SerializeField]
    private CollectableMeshController collectableMeshController; 
    [SerializeField]
    private CollectablePhysicsController collectablePhysicsController; 
    [SerializeField]
    private CollectableAnimationController collectableAnimationController;
    [SerializeField]
    private CapsuleCollider collider;
    #endregion

    #region Private Variables

    

    #endregion
    #endregion

    private void Awake()
    {
        ChangeColor(CurrentColorType);
    }
    public void ChangeColor(ColorTypes colorType)
    {
        CurrentColorType = colorType;
        collectableMeshController.ChangeCollectableMaterial(CurrentColorType);
    }

    public void DecreaseStack()
    {
        ParticleSignals.Instance.onPlayerDeath?.Invoke(transform.position);
        StackSignals.Instance.onDecreaseStack?.Invoke( transform.GetSiblingIndex());
        gameObject.transform.parent = null;
        DelayedDeath(false);
        
    }

    public void DecreaseStackOnIdle()
    {
        //DOScale
        //transform.DOJump(transform.position+new Vector3(0,0,1),1,1,0.05f);
        StackSignals.Instance.onDecreaseStackRoullette?.Invoke( transform.GetSiblingIndex());
        gameObject.transform.parent = null;
        DelayedDeath(false);
        // transform.DOScale(0,0.2f).OnComplete(() =>
        // {
        //     
        // });
        PlayerSignal.Instance.onIncreaseScale?.Invoke();
    }
    public void DeListStack()
    {
        StackSignals.Instance.onDroneArea?.Invoke(transform.GetSiblingIndex());
    }
    public void IncreaseStack()
    {
        StackSignals.Instance.onIncreaseStack?.Invoke(gameObject);
        ChangeAnimationOnController(CollectableAnimationTypes.Run);
    }
    public void ChangeAnimationOnController(CollectableAnimationTypes _currentAnimation)
    {
        collectableAnimationController.ChangeAnimation(_currentAnimation);
    }

    public async void SetCollectablePositionOnDroneArea(Transform groundTransform)
    {
        ChangeAnimationOnController(CollectableAnimationTypes.Run);
        movementCommand.MoveToGround(groundTransform);
        ChangeOutlineState(false);
        await Task.Delay(3000);
        //ChangeOutlineState(true);
    }
    
    public void ChangeOutlineState(bool _state)
    {
        collectableMeshController.ActivateOutline(_state);
    }
    public void DelayedDeath(bool _isDelayed)
    {
        ParticleSignals.Instance.onPlayerDeath?.Invoke(transform.position);
        if (_isDelayed)
        { 
        collider.enabled=false;
        ChangeAnimationOnController(CollectableAnimationTypes.Death);
        ChangeOutlineState(true);
        Destroy(gameObject,2f);
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
        collectableMeshController.CheckColorType(_droneColorAreaManager);
    }

    private void OnDestroy()
    {
        
    }
}
