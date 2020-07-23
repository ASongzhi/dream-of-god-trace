/*
 * FileName             : ButtonEffect.cs
 * Author               : yqs
 * Creat Date           : 2017.12.27
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
    /// 给StartScene的Button增添效果
    /// </summary>
    public class ButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject MoveObject;
        public Image LeftImage;
        public Image RightImage;

        private float mSpeed = 5.0f;
        private bool mIsPointerStay;

        void Start()
        {
            LeftImage.fillOrigin = 1; //对应Right，从右向左填充
            RightImage.fillOrigin = 0; //对应Left
            mIsPointerStay = false;
        }

        void Update()
        {
            if (mIsPointerStay)
            {
                LeftImage.fillAmount = Mathf.Lerp(LeftImage.fillAmount, 1, Time.deltaTime * mSpeed);
                RightImage.fillAmount = Mathf.Lerp(RightImage.fillAmount, 1, Time.deltaTime * mSpeed);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            mIsPointerStay = true;
            MoveObject.transform.SetPositionX(MoveObject.transform.position.x - 30); //向左偏移30个单位
            LeftImage.gameObject.SetActive(true);
            RightImage.gameObject.SetActive(true);

            LeftImage.fillAmount = 0;
            RightImage.fillAmount = 0;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            mIsPointerStay = false;
            MoveObject.transform.SetPositionX(MoveObject.transform.position.x + 30); //向右偏移30个单位
            LeftImage.gameObject.SetActive(false);
            RightImage.gameObject.SetActive(false);
        }
    }
}