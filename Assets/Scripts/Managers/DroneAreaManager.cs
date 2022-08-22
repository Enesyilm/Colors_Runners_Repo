using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Enums;
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

   [SerializeField] private List<Collider> droneColliderForDetect;
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

   }
   private void UnSubscribeEvents()
   {
      DroneAreaSignals.Instance.onDroneCheckCompleted -= OnDroneCheckCompleted;
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
