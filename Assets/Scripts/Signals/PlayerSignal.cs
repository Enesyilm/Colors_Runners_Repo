using System;
using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class PlayerSignal : MonoSingleton<PlayerSignal>
    {
        public UnityAction<float> onChangeVerticalSpeed=delegate{  };
        public UnityAction onIncreaseScale=delegate{  };
        
    }
}