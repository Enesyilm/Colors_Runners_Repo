using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Enums;
using Signals;
using UnityEngine;
using DG.Tweening;
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
        private GameObject collectablePrefab;
        [SerializeField] 
        private int initAmount=4;
        [SerializeField]
        private List<GameObject> stackList;
        [SerializeField]
        private Transform tempHolder;

        #endregion

        #region Private Variables
        private Transform _playerManager;


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
                StackSignals.Instance.onDroneArea += OnDroneArea;
                StackSignals.Instance.onDoubleStack += OnDoubleStack;
                StackSignals.Instance.onDecreaseStack += OnDecreaseStack;
                CoreGameSignals.Instance.onGameInit += OnInitalStackSettings;
                StackSignals.Instance.onAnimationChange += OnChangeAnimationInStack;
                

            }

            private void OnChangeAnimationInStack(CollectableAnimationTypes _currentAnimation)
            {
                for (int i = 0; i < stackList.Count; i++)
                {
                    stackList[i].GetComponent<CollectableManager>().ChangeAnimationOnController(_currentAnimation);
                }
            }

            private void OnDisable()
            {
                UnSubscribeEvents();
            }

            private void UnSubscribeEvents()
            {
                StackSignals.Instance.onIncreaseStack -= OnIncreaseStack;
                StackSignals.Instance.onDroneArea -= OnDroneArea;
                // StackSignals.Instance.onDecreaseStackOnDroneArea -= OnDecreaseStackOnDroneArea;
                StackSignals.Instance.onDoubleStack -= OnDoubleStack;
                StackSignals.Instance.onDecreaseStack -= OnDecreaseStack;
                CoreGameSignals.Instance.onGameInit -= OnInitalStackSettings;
                StackSignals.Instance.onAnimationChange -= OnChangeAnimationInStack;

            }
            #endregion

        private void FixedUpdate()
        {
            OnLerpStack();
        }
        #region Subscribed Methods
        private void OnIncreaseStack(GameObject _currentGameObject)
        {
            _currentGameObject.transform.SetParent(transform);
            stackList.Add(_currentGameObject);
        }
        private void OnDecreaseStack(int _removedIndex)
        {
            stackList.RemoveAt(_removedIndex);
            stackList.TrimExcess();

        }
        private void OnDroneArea(int index)
        {
            
            stackList[index].transform.parent = tempHolder;
            stackList.RemoveAt(index);
            stackList.TrimExcess();
            if (stackList.Count == 0)
            {
                Debug.Log("if");
                DroneAreaSignals.Instance.onDroneCheckCompleted?.Invoke();
                //DroneAreaFinal();
            }
                
        }
        private void OnFindPlayer()
        {
            _playerManager=GameObject.FindWithTag("Player").transform;
        }
        private void OnLerpStack()
        {
            if (stackList.Count > 0)
            {
                // stackList[0].transform.position = _playerManager.position;
                stackList[0].transform.position = new Vector3(
                    Mathf.Lerp(stackList[0].transform.position.x, _playerManager.transform.position.x,.2f),
                    Mathf.Lerp(stackList[0].transform.position.y, _playerManager.transform.position.y,.2f),
                    Mathf.Lerp(stackList[0].transform.position.z, _playerManager.transform.position.z-1,.2f));
                Quaternion _toPlayerRotation = Quaternion.LookRotation(_playerManager.transform.position - stackList[0].transform.position);
                _toPlayerRotation = Quaternion.Euler(0,_toPlayerRotation.eulerAngles.y,0);
                stackList[0].transform.rotation = Quaternion.Slerp( _playerManager.transform.rotation,_toPlayerRotation,1f);
                for (int index = 1; index < stackList.Count; index++)
                {
                    stackList[index].transform.position = new Vector3(
                        Mathf.Lerp(stackList[index].transform.position.x, stackList[index - 1].transform.position.x,.2f),
                        Mathf.Lerp(stackList[index].transform.position.y, stackList[index - 1].transform.position.y,.2f),
                        Mathf.Lerp(stackList[index].transform.position.z, stackList[index - 1].transform.position.z-1,.2f));
                        Quaternion toRotation = Quaternion.LookRotation(stackList[index - 1].transform.position - stackList[index].transform.position);
                        toRotation = Quaternion.Euler(0,toRotation.eulerAngles.y,0);
                        stackList[index].transform.rotation = Quaternion.Slerp( stackList[index-1].transform.rotation,toRotation,1f);
                }
                
            }
        }
        private void OnChangeStack(int initAmount)
        {
            for (int i = 0; i <initAmount ; i++)
            {
               var _gameObject= Instantiate(collectablePrefab, Vector3.back * i, Quaternion.identity);
               OnIncreaseStack(_gameObject);
               _gameObject.GetComponent<CollectableManager>().ChangeAnimationOnController(CollectableAnimationTypes.Crouch);
            }

            
        }
        private void OnDoubleStack()
        {
            OnChangeStack(stackList.Count * 2);
        }


        public void OnInitalStackSettings()
        {//deger datadan gelmeli
            OnFindPlayer();
            OnChangeStack(initAmount);
            StackSignals.Instance.onAnimationChange?.Invoke(CollectableAnimationTypes.Crouch);
        }

        

        #endregion
    }
}