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
      droneColliderObject.SetActive(true);
      foreach (var collider in droneColliderForDetect)
      {
         Debug.Log("Kapandi");
         collider.enabled=false;
         
      }
   }

   
   
   #endregion
}
