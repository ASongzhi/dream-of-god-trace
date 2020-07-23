/*
 * FileName             : ScrollbarWheel.cs
 * Author               : yqs
 * Creat Date           : 2018.1.22
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MYXZ
{
    /// <summary>
    /// 自定义ScrollbarHandle
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class ScrollbarWheel : MonoBehaviour, IDragHandler, IBeginDragHandler
    {
        /// <summary>
        /// 此Handle位置上限
        /// </summary>
        public RectTransform LimitTop;

        /// <summary>
        /// 此Handle位置下限
        /// </summary>
        public RectTransform LimitBottom;

        /// <summary>
        /// 此Handle所控制的Scrollbar
        /// </summary>
        public Scrollbar Bar;

        /// <summary>
        /// 使用鼠标滑轮时的移动速度
        /// </summary>
        public float MouseScrollWheelSpeed;

        [Space(10)] public Button MoveTopButton;
        public Button MoveUpButton;
        public Button MoveDownButton;
        public Button MoveBottomButton;
        public float MoveDistance;

        /// <summary>
        /// 所操控的Handle
        /// </summary>
        private Image mHandle;

        private Vector2 mPrePosition;
        private float mLength;

        void Start()
        {
            mHandle = this.GetComponent<Image>();
            mLength = LimitTop.localPosition.y - LimitBottom.localPosition.y; //获取Handle可移动长度
            mHandle.rectTransform.SetLocalPositionY(LimitTop.localPosition.y); //初始化Handle位置

            MoveTopButton.onClick.AddListener(MoveTop);
            MoveUpButton.onClick.AddListener(MoveUp);
            MoveDownButton.onClick.AddListener(MoveDown);
            MoveBottomButton.onClick.AddListener(MoveBottom);
        }

        void Update()
        {
            MoveHandleByMouseWheel();
            Bar.value = (mHandle.rectTransform.anchoredPosition.y - LimitBottom.anchoredPosition.y) / mLength;
        }

        private void MoveTop()
        {
            mHandle.rectTransform.SetLocalPositionY(LimitTop.localPosition.y);
        }

        private void MoveUp()
        {
            mHandle.rectTransform.SetLocalPositionY(mHandle.rectTransform.localPosition.y + MoveDistance,
                LimitBottom.localPosition.y, LimitTop.localPosition.y);
        }

        private void MoveDown()
        {
            mHandle.rectTransform.SetLocalPositionY(mHandle.rectTransform.localPosition.y - MoveDistance,
                LimitBottom.localPosition.y, LimitTop.localPosition.y);
        }

        private void MoveBottom()
        {
            mHandle.rectTransform.SetLocalPositionY(LimitBottom.localPosition.y);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            mPrePosition = eventData.position; //拖拽事件触发时的初始化
        }

        public void OnDrag(PointerEventData eventData)
        {
            MoveHandleByDrag(eventData);
        }

        /// <summary>
        /// 鼠标拖拽Handle移动
        /// </summary>
        /// <param name="eventData"></param>
        private void MoveHandleByDrag(PointerEventData eventData)
        {
            float offset = eventData.position.y - mPrePosition.y; //获取每帧之间的鼠标偏移量
            mHandle.rectTransform.SetPositionY(mHandle.rectTransform.position.y + offset, LimitBottom.position.y,
                LimitTop.position.y);
            Bar.value = (mHandle.rectTransform.anchoredPosition.y - LimitBottom.anchoredPosition.y) / mLength;
            mPrePosition = eventData.position; //记录
        }

        /// <summary>
        /// 鼠标滑轮控制Handle移动
        /// </summary>
        private void MoveHandleByMouseWheel()
        {
            float moveSpeed = Input.GetAxis("Mouse ScrollWheel") * MouseScrollWheelSpeed;
            mHandle.rectTransform.SetLocalPositionY(mHandle.rectTransform.localPosition.y + moveSpeed * Time.deltaTime,
                LimitBottom.localPosition.y, LimitTop.localPosition.y);
        }
    }
}