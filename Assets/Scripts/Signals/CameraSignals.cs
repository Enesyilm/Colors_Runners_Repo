using System;
using UnityEngine;
using Extentions;
using Enums;
using Data.ValueObjects;
using UnityEngine.Events;
using Object = System.Object;

namespace Signals
{
    public class CameraSignals : MonoSingleton<CameraSignals>
    {
        
        public UnityAction<CameraStates> onChangeCameraStates=delegate{  };
    }
}