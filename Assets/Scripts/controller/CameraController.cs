using UnityEngine;
using System.Collections;

namespace Game
{
    public class CameraController: MonoBehaviour
    {
        public Transform targeTransform;
        private Transform selfTransform;
        public float z, y;
        // Use this for initialization
        void Start()
        {
            selfTransform = this.transform;
            selfTransform.LookAt(targeTransform);
        }

        // Update is called once per frame
        void LateUpdate()
        {
            GetCameraPos();
        }

        private void GetCameraPos()
        {
            Vector3 newTagetVector3 = new Vector3(targeTransform.position.x, targeTransform.position.y + y,
                targeTransform.position.z + z);
            selfTransform.position = Vector3.Lerp(selfTransform.position, newTagetVector3, Time.deltaTime * 5);
        }
    }
}

