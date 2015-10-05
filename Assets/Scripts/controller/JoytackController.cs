using UnityEngine;
using System.Collections;

namespace Game
{

    public class JoytackController : MonoBehaviour
    {
        public static JoytackController instance;
        private Vector3 origin;
        private Transform selfTransform;

        private float distance, moveMaxDistance = 80, activationDistance = 1;
        //偏移矢量
        private Vector2 deltaPos;
        //移动方向
        public Vector3 movePosNorm { get; private set; }
        void Awake()
        {
            instance = this;

            #region 添加操作响应函数
            //1.拖拽开始响应函数
            EventTriggrListener.GetEventTriggerListener(gameObject).OnDragDelegate = OnDragDelegate;

            //2.拖拽结束响应函数
            EventTriggrListener.GetEventTriggerListener(gameObject).OnDragEndDelegate = OnDragEndDelegate;

            //3.触摸按钮响应函数
            EventTriggrListener.GetEventTriggerListener(gameObject).OnPointDownDelegate = OnPointerDownDelegate;
            #endregion

        }

        /// <summary>
        /// 点击响应函数
        /// </summary>
        /// <param name="go">点击的对象</param>
        private void OnPointerDownDelegate(GameObject go)
        {
            //响应玩家控制类的对应函数
            if (PlayerMoveController.moveStart != null)
                PlayerMoveController.moveStart();
        }

        /// <summary>
        /// 拖拽结束响应函数
        /// </summary>
        /// <param name="go">点击对象</param>
        private void OnDragEndDelegate(GameObject go)
        {
            //回归原点
            selfTransform.localPosition = origin;

            //响应玩家控制类的对应函数
            if (PlayerMoveController.moveEnd != null)
                PlayerMoveController.moveEnd();
        }

        /// <summary>
        /// 拖拽响应函数 
        /// </summary>
        /// <param name="go">点击对象</param>
        /// <param name="delta">拖拽距离</param>
        private void OnDragDelegate(GameObject go, Vector2 delta)
        {
            //设置偏移矢量
            deltaPos = delta;

            //设置移动位置
            selfTransform.localPosition += new Vector3(deltaPos.x, deltaPos.y, 0);
        }



        #region Unity Method

        void Start()
        {
            //初始化
            origin = transform.localPosition;
            selfTransform = this.transform;
        }

        void Update()
        {
            //计算距离，用来判定当前位置和原点的距离，用来做最大距离的限制判断值
            distance = Vector3.Distance(selfTransform.localPosition, origin);

            //限制拖拽的最大移动距离
            if (distance >= moveMaxDistance)
            {
                //计算在圆上的一个点<公式：（目标点-原点）*半径/原点到目标点的距离了>
                Vector3 point = origin + (selfTransform.localPosition - origin) * moveMaxDistance / distance;

                //设置当前位置为圆上一点
                selfTransform.localPosition = point;
            }

            //判定玩家是否激活摇杆，即滑动距离超过预设值就代表移动
            if (Vector3.Distance(selfTransform.localPosition, origin) > activationDistance)
            {
                //获取移动的方向<移除长度>
                movePosNorm = (selfTransform.localPosition - origin).normalized;
                //设置方向
                movePosNorm = new Vector3(movePosNorm.x, 0, movePosNorm.y);
            }
            else
            {
                //无移动，任一向
                movePosNorm = Vector3.zero;
            }
        }
        #endregion
    }
}

