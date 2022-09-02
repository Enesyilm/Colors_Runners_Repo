using System;
using System.Collections.Generic;
using Data.ValueObjects;
using Enums;
using Signals;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private int _totalScore;
        private int _levelScore;
        private List<int> _scoreVariables = new List<int>(Enum.GetNames(typeof(ScoreVariableType)).Length);
        

        #endregion

        #region Serialized Variables
        

        #endregion
        #endregion

        private void Awake()
        {
            InitScoreValues();
        }
        

        private void InitScoreValues()
        {
            for (int i = 0; i < Enum.GetNames(typeof(ScoreVariableType)).Length; i++)
            {
                _scoreVariables.Add(0);
            }
        }

        #region Event Subscription
        
        private void OnEnable()
        {
            
            SubscribeEvents();
        }


        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onChangeGameState += OnSendScoreToManagers;
            ScoreSignals.Instance.onChangeScore+=OnChangeScore;
            SaveSignals.Instance.onSendDataToManagers += InitTotalScoreData;
            SaveSignals.Instance.onApplicationPause += OnSendScoreToSave;
            ScoreSignals.Instance.onAddLevelTototalScore += OnAddLevelToTotalScore;
            // CoreGameSignals.Instance.onReset += OnReset;
            StackSignals.Instance.onStackInit += OnReset;
            ScoreSignals.Instance.onGetScore+=OnGetScore;
        }

        private void OnAddLevelToTotalScore()
        {
            _scoreVariables[0] += _scoreVariables[1];
        }


        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            SaveSignals.Instance.onApplicationPause -= OnSendScoreToSave;
            CoreGameSignals.Instance.onChangeGameState -= OnSendScoreToManagers;
            SaveSignals.Instance.onSendDataToManagers -= InitTotalScoreData;
            ScoreSignals.Instance.onAddLevelTototalScore -= OnAddLevelToTotalScore;
            ScoreSignals.Instance.onChangeScore-=OnChangeScore;
            //CoreGameSignals.Instance.onReset -= OnReset;
            ScoreSignals.Instance.onGetScore-=OnGetScore;
            StackSignals.Instance.onStackInit -= OnReset;
        }

        private void OnSendScoreToSave()
        {
            SaveSignals.Instance.onChangeSaveData(SaveTypes.TotalColorman,_totalScore);
        }

        private void InitTotalScoreData(SaveData _saveData)
        {
            _scoreVariables[0]=_saveData.TotalColorman;
            ScoreSignals.Instance.onUpdateScore?.Invoke(_scoreVariables);
        }

        #endregion
        private int OnGetScore(ScoreVariableType _scoreVarType)
        {
            return _scoreVariables[(int)_scoreVarType];
        }

        private void OnReset()
        {
            _scoreVariables[1] = 0;
            ScoreSignals.Instance.onUpdateScore?.Invoke( _scoreVariables);
        }
        private void OnChangeScore(ScoreTypes _scoreType,ScoreVariableType _scoreVarType)
        {
            int _changedScoreValue = 0;
            switch (_scoreType)
            {
                case ScoreTypes.DecScore:
                    _changedScoreValue--;
                    break;
                case ScoreTypes.IncScore:
                    _changedScoreValue++;
                    break;
                case ScoreTypes.DoubleScore:
                    _changedScoreValue+= _scoreVariables[(int)_scoreVarType];
                    break;
                    
            }

            _scoreVariables[(int)_scoreVarType]+=_changedScoreValue;
            ScoreSignals.Instance.onUpdateScore?.Invoke( _scoreVariables);

        }
        private void OnSendScoreToManagers(GameStates arg0)
        {
            ScoreSignals.Instance.onUpdateScore?.Invoke(_scoreVariables);
        }
    }
}