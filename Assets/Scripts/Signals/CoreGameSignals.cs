using System;
using UnityEngine;
using Enums;
using UnityEngine.Events;
using Extentions;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction<GameStates> onChangeGameState = delegate { };

        public UnityAction<GameStates> onGetGameState = delegate { };
        
        public UnityAction onGameInit = delegate { };
        public UnityAction onGameInitLevel = delegate { };

        public UnityAction onLevelInitialize = delegate { };
        public UnityAction onLevelIdleInitialize = delegate { };
        public UnityAction onClearActiveIdleLevel=delegate { };
        
        public UnityAction onClearActiveLevel = delegate { };
        
        public UnityAction onLevelFailed = delegate { };
        
        public UnityAction onLevelSuccessful = delegate { };
        
        public UnityAction onNextLevel = delegate { };
        
        public UnityAction onRestartLevel = delegate { };
        
        public UnityAction onPlay = delegate { };
        
        public UnityAction onReset = delegate { };
        
        public UnityAction onStageAreaReached = delegate { };
        
        public UnityAction onStageSuccessful = delegate { };
        public Func<int> onGetLevelID = delegate { return 0; };
        public Func<int> onGetIdleLevelID = delegate { return 0; };

            protected override void Awake()
            {
                base.Awake();
            }
            //public UnityAction<SaveGameDataParams> onSaveGameData = delegate { };
            //public UnityAction<LoadGameDataParams> onLoadGameData = delegate { };

    }
}