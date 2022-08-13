using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        public UnityAction<GameObject> onIncreaseStack = delegate { };
        public UnityAction<int> onDecreaseStack = delegate { };
        public UnityAction onDoubleStack = delegate { };
        public UnityAction onColorChange = delegate { };

    }
}