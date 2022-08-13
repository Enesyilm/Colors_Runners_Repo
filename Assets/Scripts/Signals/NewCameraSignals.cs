using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class NewCameraSignals : MonoSingleton<NewCameraSignals>
    {
        public UnityAction<CameraStates> onChangeCameraState=delegate{  };
    }
}