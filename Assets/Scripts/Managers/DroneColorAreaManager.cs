using System;
using System.Collections.Generic;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class DroneColorAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public ColorTypes CurrentColorType;
        public  MatchType matchType=MatchType.UnMatched;

        #endregion
        #region Serialized Variables
        
        [SerializeField]bool openDroneAreaExit;
        [SerializeField] private DroneAreaMeshController droneAreaMeshController;

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
           droneAreaMeshController.ChangeAreaColor(CurrentColorType);
        }

        void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onGameInit -= OnSetColor;
        }

        #endregion
        
        // private async void DroneAreaFinal()
        // {
        //     await Task.Delay(3000);
        //     _playerManager.transform.position = new Vector3(tr.x,_playerManager.transform.position.y,newPos.z);
        // }

       
    }
}