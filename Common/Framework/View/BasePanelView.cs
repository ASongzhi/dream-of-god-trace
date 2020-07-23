/*
 * FileName             : BasePanelView.cs
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
using strange.extensions.mediation.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// UIPanel的基类
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class BasePanelView : View
    {
        /// <summary>
        /// 界面加载
        /// </summary>
        public virtual void OnEnter()
        {
            this.gameObject.SetActive(true);
            this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        /// <summary>
        /// 界面暂停
        /// </summary>
        public virtual void OnPause()
        {
            this.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        /// <summary>
        /// 界面继续
        /// </summary>
        public virtual void OnResume()
        {
            this.gameObject.SetActive(true);
            this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        /// <summary>
        /// 界面退出
        /// </summary>
        public virtual void OnExit()
        {
            this.gameObject.SetActive(false);
        }
    }
}