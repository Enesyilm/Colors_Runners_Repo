using UnityEngine;
using Cinemachine;

namespace Managers.Abstracts
{
    public abstract class CameraBaseState
    {
        public abstract void EnterState(CameraStateManager _camManager,CinemachineVirtualCamera virtualCamera,Transform followTarget);
        public abstract void UpdateState(CameraStateManager _camManager);
        public abstract void OnCollisionEnter(CameraStateManager _camManager);

    }
}