using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class ParticleSignals:MonoSingleton<ParticleSignals>
    {

        public UnityAction<Vector3> onParticleBurst =delegate{};
        public UnityAction<Vector3> onPlayerDeath =delegate{};
        public UnityAction onParticleStop =delegate{};
    }
}