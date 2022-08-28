using System;
using Signals;
using UnityEngine.Events;

namespace Managers.Interface
{
    interface ISaveable
    { 
        void SendDataToSaveManager();
    }
}