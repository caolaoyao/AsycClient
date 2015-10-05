using UnityEngine;
using System.Collections;

namespace Game
{

    public delegate void MoveDelegate();
    public class PlayerMoveController : MonoBehaviour
    {
        public static MoveDelegate moveStart;
        public static MoveDelegate moveEnd;
        public static PlayerMoveController instance;

        private JoytackController guiJoystackController;
        private Transform selfTransform;
        [SerializeField]
        private bool turnBase = false;
        private float angle;
        [SerializeField]
        private float moveSpeed = 5;

//        private Animation playerAnimation;
        void Awake()
        {
//             playerAnimation = GetComponent<Animation>();
//             playerAnimation["wait"].blendMode = AnimationBlendMode.Blend;
            instance = this;
            selfTransform = this.transform;

            moveStart = OnMoveStart;
            moveEnd = OnMoveEnd;
            guiJoystackController = JoytackController.instance;
        }

        void Update()
        {
            if (guiJoystackController == null)
            {
                return;
            }

            if (turnBase)
            {
                //位置的移动
                Vector3 move = guiJoystackController.movePosNorm * Time.deltaTime * moveSpeed;
                selfTransform.localPosition += move;
                //从JoytackController移动方向 算出自身的角度
                angle = Mathf.Atan2(guiJoystackController.movePosNorm.x,
                    guiJoystackController.movePosNorm.z) * Mathf.Rad2Deg;
                selfTransform.localRotation = Quaternion.Euler(Vector3.up * angle);
//                Debug.Log(selfTransform.localRotation +"    "+selfTransform.localRotation.eulerAngles);
            }
        }


        private void OnMoveEnd()
        {
            turnBase = false;
//            playerAnimation.Play("wait");

        }

        private void OnMoveStart()
        {
            turnBase = true;
//            playerAnimation.Play("run");
        }
    }

}
