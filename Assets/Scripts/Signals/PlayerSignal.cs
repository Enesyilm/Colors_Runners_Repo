using System;
using Enums;
using Extentions;
using UnityEngine;

namespace Signals
{
    public class PlayerSignal : MonoSingleton<PlayerSignal>
    {
        public Func<ColorTypes> onGetColor= delegate { return 0;};
    }
}