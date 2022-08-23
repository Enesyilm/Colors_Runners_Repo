using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class DroneAreaSignals : MonoSingleton<DroneAreaSignals>
    {
        public UnityAction onDroneCheckStarted=delegate {  };
        public UnityAction onDroneCheckCompleted =delegate { };

    }
}