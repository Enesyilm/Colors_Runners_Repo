using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class DroneAreaSignals : MonoSingleton<DroneAreaSignals>
    {
        public UnityAction<GameObject> onDroneAreaEnter =delegate { };
        public UnityAction onDroneAreasCollectablesDeath =delegate { };

    }
}