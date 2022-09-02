using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using Enums;
using Controllers;
using Signals;
using UnityEngine;

public class DroneAreaManager : MonoBehaviour
{
   #region Self Variables

   #region Public Variables

   

   #endregion

   #region Serialized Variables

   [SerializeField]
   private GameObject droneColliderObject;

   [SerializeField]
   private GameObject droneObject;

   [SerializeField] private List<Collider> droneColliderForDetect;
   [SerializeField] private List<DroneColorAreaManager> droneColorAreaManagers;
   #endregion

   #region Private Variables

   

   #endregion

   #endregion

   #region Event Subscription

   private void OnEnable()
   {
      SubscribeEvents();
   }
   private void OnDisable()
   {
      UnSubscribeEvents();
   }

   private void SubscribeEvents()
   {
      DroneAreaSignals.Instance.onDroneCheckCompleted += OnDroneCheckCompleted;
      DroneAreaSignals.Instance.onDroneCheckStarted += OnDroneCheckStarted;

   }
   private void UnSubscribeEvents()
   {
      DroneAreaSignals.Instance.onDroneCheckCompleted -= OnDroneCheckCompleted;
      DroneAreaSignals.Instance.onDroneCheckStarted -= OnDroneCheckStarted;
   }

   private async void OnDroneCheckStarted()
   {
      droneObject.SetActive(true);
      await Task.Delay(150);
      foreach (var droneColorAreaManager in droneColorAreaManagers)
      {
         
         if (droneColorAreaManager.matchType == MatchType.UnMatched)
         {
            droneColorAreaManager.gameObject.transform.DOScaleZ(0,0.5f).OnComplete(() =>
            {
               droneColorAreaManager.gameObject.transform.DOScaleX(0, 0.5f);
            });
         }
      }
     
   }


   private void OnDroneCheckCompleted()
   {
      ChangeColliders();
   }
   private async void ChangeColliders()
   {
      foreach (var collider in droneColliderForDetect)
      {
         collider.enabled=false;
      }
      droneColliderObject.SetActive(true);
      await Task.Delay(200);
      droneColliderObject.SetActive(false);
   }

   
   
   #endregion
}