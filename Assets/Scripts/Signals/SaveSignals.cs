using System;
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
        public Func<SaveTypes,int>onGetSaveData= delegate { return 0; };
        public UnityAction<SaveData>onSendDataToManagers=delegate {  };
    }
}