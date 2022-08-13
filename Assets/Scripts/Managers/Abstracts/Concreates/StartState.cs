using System.Threading.Tasks;
using Cinemachine;
using Enums;
using UnityEngine;
using Signals;
using UnityEngine.tvOS;

namespace Managers.Abstracts.Concreates
{
    public class StartState:CameraBaseState
    {
        public override void EnterState(CameraStateManager _camManager,CinemachineVirtualCamera virtualCamera,Transform followTarget)
        {
            Debug.Log("start EnterState calisityor");
            virtualCamera.Follow = _camManager.initPosition;
            CameraSignals.Instance.onChangeCameraStates?.Invoke(CameraStates.Runner);
        }
        
        

        public override void UpdateState(CameraStateManager _camManager)
        {
        }

        public override void OnCollisionEnter(CameraStateManager _camManager)
        {
        }
    }
}