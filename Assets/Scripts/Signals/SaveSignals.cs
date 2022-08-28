using System;
using UnityEngine;
using Extentions;
using Enums;
using Data.ValueObjects;
using UnityEngine.Events;
using Object = System.Object;

namespace Signals
{
    public class SaveSignals : MonoSingleton<SaveSignals>
    {
        
        public UnityAction<SaveTypes, int> onChangeSaveData=delegate{  };
        public UnityAction<IdleLevelListData> onChangeIdleLevelListData=delegate{  };
        public UnityAction onApplicationPause = delegate { };

        
        public Func<SaveTypes,int>onGetIntSaveData= delegate { return 0; };

        public Func<IdleLevelListData>onGetIdleSaveData= delegate { return new IdleLevelListData(); };
        
        
        
        public UnityAction<SaveData>onSendDataToManagers=delegate {  };
    }
}