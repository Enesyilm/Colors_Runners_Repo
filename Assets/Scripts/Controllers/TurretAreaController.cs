using System;
using Enums;
using Managers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class TurretAreaController : MonoBehaviour
    {
        #region Self Region

        #region Public Variables
        public ColorTypes ColorType; 
        public Vector3 CurrentTargetPos;

        

        #endregion

        #region Serialized Variables

        [SerializeField] private TurretAreaManager turretAreaManager;

        [SerializeField]
        private Transform turret;

        [SerializeField]
        private int turretSearchPeriod;
        [SerializeField] MeshRenderer meshRenderer;



        #endregion

        #region Private Variables


        #endregion

        #endregion

        private void Awake()
        {
            ChangeInitColor();
        }

        private void ChangeInitColor()
        {
              var colorHandler=Addressables.LoadAssetAsync<Material>($"CoreColor/Color_{ColorType}");
                        meshRenderer.material = (colorHandler.WaitForCompletion() != null)?colorHandler.Result:null;
        }

        public void GetRandomSearchPoint() {
            CurrentTargetPos= new Vector3(
                Random.Range(transform.position.x- (transform.localScale.x / 2), transform.position.x+ (transform.localScale.x / 2)),
                transform.position.y,
                Random.Range(transform.position.z- (transform.localScale.z / 2), transform.position.z+(transform.localScale.z / 2))
            );
        }
        private void Start()
        {
            InvokeRepeating("GetRandomSearchPoint",0f,turretSearchPeriod);
        }
        
        public void StartSearchRotation()
        {
            RotateToTargetPos();
        }

        public void RotateToTargetPos()
        {
            Vector3 _relativePos = CurrentTargetPos - turret.position;
            Quaternion _finalRotate=Quaternion.LookRotation(_relativePos);
            turret.rotation=Quaternion.Lerp(turret.rotation,_finalRotate,0.1f);
        }

        public void StartWarnedRotation(GameObject _target)
        {
            CurrentTargetPos=_target.transform.position;
            RotateToTargetPos();
            
           
        }


        public void FireTurretAnimation()
        {
        }
    }
}