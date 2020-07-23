/*
 * FileName             : TalkPanelMediator.cs
 * Author               : yqs
 * Creat Date           : 2018.2.20
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
    /// 交谈面板的Mediator
    /// </summary>
    public class TalkPanelMediator : Mediator
    {
        [Inject]
        public TalkPanelView TalkView { get; set; }

        [Inject]
        public FinishTalkSignal TalkFinishSignal { get; set; }

        public override void OnRegister()
        {
            TalkView.TalkFinishSignal.AddListener(TalkFinish);
        }

        public override void OnRemove()
        {
            TalkView.TalkFinishSignal.RemoveListener(TalkFinish);
        }

        private void TalkFinish()
        {
            TalkFinishSignal.Dispatch(TalkView.IsTaskTalk, TalkView.CurrentTask);
        }
    }
}