using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class TurretSignals : MonoSingleton<TurretSignals>
    {
        public UnityAction onResetList=delegate {  };
    }
}