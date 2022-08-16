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
   private Transform droneAreaTempHolder;
   

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
      DroneAreaSignals.Instance.onDroneAreaEnter += OnSetDroneAreaHolder;
      DroneAreaSignals.Instance.onDroneAreasCollectablesDeath += OnSendCollectablesBackToDeath;

   }

   private async void OnSendCollectablesBackToDeath()
   {
      for (int i = 0; i < droneAreaTempHolder.childCount; i++)
      {
         await Task.Delay(100);
         if (droneAreaTempHolder.transform.GetChild(i).GetComponent<CollectableManager>().MatchType != MatchType.Match)
         {
            droneAreaTempHolder.transform.GetChild(i).GetComponent<CollectableManager>().ChangeAnimationOnController(CollectableAnimationTypes.Death);
            Destroy(droneAreaTempHolder.GetChild(i).gameObject,3f);
         }
         
      }
   }
   private void OnSendCollectablesBackToStack()
   {
      for (int i = 0; i < droneAreaTempHolder.childCount; i++)
      {
         StackSignals.Instance.onRebuildStack?.Invoke(droneAreaTempHolder.transform.GetChild(i).gameObject);
      }
   }

   private void UnSubscribeEvents()
   {
      DroneAreaSignals.Instance.onDroneAreaEnter -= OnSetDroneAreaHolder;
      DroneAreaSignals.Instance.onDroneAreasCollectablesDeath -= OnSendCollectablesBackToDeath;
   }

   #endregion
   private void OnSetDroneAreaHolder(GameObject _collectable)
   {
      _collectable.transform.SetParent(droneAreaTempHolder);
      
   }
   
}
