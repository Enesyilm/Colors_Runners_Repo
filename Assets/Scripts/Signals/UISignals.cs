using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        public UnityAction<UIPanelTypes> onOpenPanel=delegate {  };
        public UnityAction<UIPanelTypes> onClosePanel=delegate {  };
    }
}