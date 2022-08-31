using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        public UnityAction<GameObject> onIncreaseStack = delegate { };
        public UnityAction<int> onDecreaseStack = delegate { };
        public UnityAction onStackInit = delegate { };
        public UnityAction<int> onDecreaseStackRoullette = delegate { };
        public UnityAction<int> onDroneArea = delegate { };
        public UnityAction<int> onDecreaseStackOnDroneArea = delegate { };
        public UnityAction onDoubleStack = delegate { };
        public UnityAction<ColorTypes> onColorChange = delegate { };
        public UnityAction<GameObject> onRebuildStack = delegate { };
        public UnityAction<CollectableAnimationTypes> onAnimationChange = delegate { };

    }
}