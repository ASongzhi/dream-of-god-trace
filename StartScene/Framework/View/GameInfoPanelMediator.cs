/*
 * FileName             : GameInfoPanelMediator.cs
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
using strange.extensions.mediation.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 游戏信息View的Mediator
    /// </summary>
    public class GameInfoPanelMediator : Mediator
    {
        [Inject]
        public GameInfoPanelView GameInfoView { get; set; }

        [Inject]
        public PopPanelSignal PopPanelSignal { get; set; }

        public override void OnRegister()
        {
            GameInfoView.ExitGameInfoPanelSignal.AddListener(PopThisPanel);
        }

        public override void OnRemove()
        {
            GameInfoView.ExitGameInfoPanelSignal.RemoveListener(PopThisPanel);
        }

        public void PopThisPanel()
        {
            PopPanelSignal.Dispatch();
        }
    }
}