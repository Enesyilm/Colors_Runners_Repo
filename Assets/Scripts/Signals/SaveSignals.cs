using UnityEngine;
using Extentions;
using Enums;
using Data.ValueObjects;
using UnityEngine.Events;

namespace Signals
{
    public class SaveSignals : MonoSingleton<SaveSignals>
    {
        public UnityAction<SaveTypes,int>onChangeSaveData=delegate {  };
        public UnityAction<SaveTypes>onSaveDataToDatabase=delegate {  };
        public UnityAction<SaveData>onSendDataToManagers=delegate {  };
    }
}