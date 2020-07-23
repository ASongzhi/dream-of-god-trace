/*
 * FileName             : MessageBoxPanelView.cs
 * Author               : yqs
 * Creat Date           : 2017.12.28
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MYXZ
{
    /// <summary>
    /// 消息面板
    /// </summary>
    public class MessageBoxPanelView : BasePanelView
    {
        /// <summary>
        /// 此消息弹窗的文本信息
        /// </summary>
        public Text MessageText;

        [SerializeField] private Button mConfirmButton;
        public UnityAction ConfirmEvent;
        [Space(10)] [SerializeField] private Button mCancelButton;
        public UnityAction CancelEvent;

        public override void OnEnter()
        {
            base.OnEnter();
            mConfirmButton.onClick.AddListener(ConfirmEvent);
            mCancelButton.onClick.AddListener(CancelEvent);
        }

        public override void OnExit()
        {
            base.OnExit();
            mConfirmButton.onClick.RemoveListener(ConfirmEvent);
            mCancelButton.onClick.RemoveListener(CancelEvent);
        }
    }
}