using System.Threading.Tasks;
using Cinemachine;
using UnityEngine;
namespace Managers.Abstracts.Concreates
{
    public class RunnerState:CameraBaseState
    {
        public override void EnterState(CameraStateManager _camManager,CinemachineVirtualCamera virtualCamera,Transform followTarget)
        {
            ChangeFollowTarget(virtualCamera,followTarget);
        }
        private async void ChangeFollowTarget(CinemachineVirtualCamera virtualCamera,Transform followTarget)
        {
            await Task.Delay(1000);
            virtualCamera.Follow = followTarget;
            
        }
        public override void UpdateState(CameraStateManager _camManager)
        {
        }

        public override void OnCollisionEnter(CameraStateManager _camManager)
        {
        }
    }
}