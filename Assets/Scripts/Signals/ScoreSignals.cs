using System;
using System.Collections.Generic;
using Enums;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals:MonoSingleton<ScoreSignals>
    {
        public UnityAction<ScoreTypes,ScoreVariableType> onChangeScore=delegate { };
        public Func<ScoreVariableType,int> onGetScore= delegate { return 0;};
        public UnityAction<List<int>> onUpdateScore= delegate { };
        public UnityAction onAddLevelTototalScore= delegate { };

    }
}