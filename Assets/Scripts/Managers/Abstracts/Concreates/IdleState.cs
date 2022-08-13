using System.Threading.Tasks;
using Cinemachine;
using UnityEngine;
namespace Managers.Abstracts.Concreates
{
    public class IdleState:CameraBaseState
    {
        public override void EnterState(CameraStateManager _camManager,CinemachineVirtualCamera virtualCamera,Transform followTarget)
        {
            
        }

        public override void UpdateState(CameraStateManager _camManager)
        {
        }

        public override void OnCollisionEnter(CameraStateManager _camManager)
        {
        }
    }
}