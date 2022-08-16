using System;
using System.Collections.Generic;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class DroneColorAreaController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public ColorTypes ColorType; 

        #endregion
        #region Serialized Variables
        
        [SerializeField]bool openDroneAreaExit;

        #endregion
        #region Private Variables
            private List<GameObject> _tempList=new List<GameObject>();
            private MeshRenderer _meshRenderer;
            #endregion

        #endregion

        #region SubscribeEvents

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnEnable()
        {
            SubscribeEvents();

        }
        private void OnDisable()
        {
            UnSubscribeEvents();

        }

        void SubscribeEvents()
        {
           
            CoreGameSignals.Instance.onGameInit += OnSetColor;
        }

        private void OnSetColor()
        {
           //renk gelecek _meshRenderer.material.color = Resources.Load<Material>("/Data/").color;
        }

        void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onGameInit -= OnSetColor;
        }

        #endregion
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collected"))
            {
                //other.tag = "Collectable";
               
                                
            }

        }

       // public void OnDroneAreaExitTrigger()
       //  {
       //      Debug.Log("_colorType PlayerSignal.Instance.onGetColor?.Invoke()"+
       //                (_colorType != PlayerSignal.Instance.onGetColor?.Invoke()));
       //      Debug.Log("_colorType "+_colorType+"onGetColor?.Invoke()"+
       //                (PlayerSignal.Instance.onGetColor?.Invoke()));
       //      if (_colorType != PlayerSignal.Instance.onGetColor?.Invoke())
       //      {
       //          foreach (var collectable in _tempList)
       //          {
       //              Debug.Log("Destroy Calisti");
       //              Destroy(collectable);
       //          }
       //      }
       //      else
       //      {
       //          foreach (var collectable in _tempList)
       //          {
       //              Debug.Log("Stackadd Calisti");
       //              collectable.GetComponent<CollectableManager>().IncreaseStack(collectable);
       //          }
       //      }
       //      
       //  }
    }
}