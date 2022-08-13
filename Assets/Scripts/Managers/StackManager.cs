using System;
using System.Collections.Generic;
using Enums;
using Signals;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        [SerializeField]
        private Transform playerManager;

        [SerializeField] private GameObject collectablePrefab;
        [SerializeField] private int initAmount=4;
         [SerializeField]
         private List<GameObject> StackList;

        #endregion

        #region Private Variables
       

        #endregion

        #endregion

        #region EventSubcription
            private void OnEnable()
            {
                SubscribeEvents();
            }

            private void SubscribeEvents()
            {
                StackSignals.Instance.onIncreaseStack += OnIncreaseStack;
                StackSignals.Instance.onDoubleStack += OnDoubleStack;
                StackSignals.Instance.onDecreaseStack += OnDecreaseStack;
            }

            private void OnDisable()
            {
                UnSubscribeEvents();
            }

            private void UnSubscribeEvents()
            {
                StackSignals.Instance.onIncreaseStack -= OnIncreaseStack;
                StackSignals.Instance.onDoubleStack -= OnDoubleStack;
                StackSignals.Instance.onDecreaseStack -= OnDecreaseStack;
            }

            #endregion

        private void FixedUpdate()
        {
            OnLerpStack();
        }

        #region Subscribed Methods
        private void OnIncreaseStack(GameObject _currentGameObject)
        {
           StackList.Add(_currentGameObject);
        }
        private void OnDecreaseStack(int _removedIndex)
        {
            StackList[_removedIndex].GetComponent<CollectableManager>().Death();
            StackList.RemoveAt(_removedIndex);

        }
        private void OnDoubleStack()
        {
            throw new NotImplementedException();
        }

        private void OnLerpStack()
        {
            if (StackList.Count > 0)
            {
                StackList[0].transform.position = playerManager.position;
                for (int index = 1; index < StackList.Count; index++)
                {
                    StackList[index].transform.position = new Vector3(
                        Mathf.Lerp(StackList[index].transform.position.x, StackList[index - 1].transform.position.x,.2f),
                        Mathf.Lerp(StackList[index].transform.position.y, StackList[index - 1].transform.position.y,.2f),
                        Mathf.Lerp(StackList[index].transform.position.z, StackList[index - 1].transform.position.z-1,.2f));
                }
                
            }
        }
        private void OnInitialStack()
        {
            for (int i = 0; i <initAmount ; i++)
            {
               var _gameObject= Instantiate(collectablePrefab, Vector3.back * i, Quaternion.identity);
               OnIncreaseStack(_gameObject);
               _gameObject.GetComponent<CollectableManager>().ChangeAnimationOnController(CollectableAnimationTypes.Crouch);
            }

            
        }

        #endregion
    }
}