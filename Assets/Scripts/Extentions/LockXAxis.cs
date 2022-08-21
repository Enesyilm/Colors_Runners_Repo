using UnityEngine;
using Cinemachine;
 

[ExecuteInEditMode] [SaveDuringPlay] [AddComponentMenu("")]
public class LockXAxis: CinemachineExtension
{
    [Tooltip("Lock the camera's Z position to this value")]
    private float xPosition = 0;
 
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            pos.x = xPosition;
            state.RawPosition = pos;
        }
    }
}