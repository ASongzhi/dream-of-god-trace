/*
 * FileName             : ButtonEffectExtension.cs
 * Author               : yqs
 * Creat Date           : 2018.1.24
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
    /// Button效果增添，提供可选的HighLightImage和PressImage
    /// </summary>
    public class ButtonEffectExtension : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler,
        IPointerUpHandler
    {
        /// <summary>
        /// 高亮Image
        /// </summary>
        public Image ButtonEffectImage;

        [Range(0, 1f)]
        public float HighLightA = 0.5f;
        [Range(0, 1.0f)]
        public float PressA = 1.0f;

        /// <summary>
        /// 鼠标是否悬浮在此Button上
        /// </summary>
        private bool mIsPointerStay;

        private bool mIsPointerPress;

        public enum State
        {
            Normal,
            HighLight,
            Press
        }

        public State CurrentState
        {
            get { return mState; }
        }

        private State mState = State.Normal;

        void Start()
        {
            mIsPointerStay = false;
            mIsPointerPress = false;
            if (ButtonEffectImage != null)
            {
                ButtonEffectImage.gameObject.SetActive(false);
            }
        }

        void Update()
        {
            switch (mState)
            {
                case State.Normal:
                    {
                        if (mIsPointerStay)
                        {
                            mState = mIsPointerPress ? State.Press : State.HighLight;
                        }
                        break;
                    }
                case State.HighLight:
                    {
                        if (!mIsPointerStay)        //如果鼠标不在按钮上
                        {
                            mState = State.Normal;
                        }
                        else if(mIsPointerPress)    //如果鼠标按下
                        {
                            mState = State.Press;
                        }
                        break;
                    }
                case State.Press:
                    {
                        if (!mIsPointerPress)
                        {
                            mState = mIsPointerStay ? State.HighLight : State.Normal;
                        }
                        break;
                    }
            }
            if (mState != State.Normal)
            {
                ButtonEffectImage.gameObject.SetActive(true);
                ButtonEffectImage.SetColorA(mState == State.HighLight ? HighLightA : PressA);
            }
            else
            {
                ButtonEffectImage.gameObject.SetActive(false);
            }
        }

        void OnEnable()
        {
            mIsPointerStay = false;
            mIsPointerPress = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            mIsPointerStay = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            mIsPointerStay = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            mIsPointerPress = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            mIsPointerPress = true;
        }
    }
}