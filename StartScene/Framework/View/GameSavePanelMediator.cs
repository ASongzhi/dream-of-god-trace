/*
 * FileName             : GameSavePanelMediator.cs
 * Author               : hy
 * Creat Date           : 2018.1.26
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
    /// 游戏存档面板的Mediator
    /// </summary>
    public class GameSavePanelMediator : Mediator
    {
        [Inject]
        public GameSavePanelView GameSaveView { get; set; }

        [Inject]
        public PopPanelSignal PopPanelSignal { get; set; }

        public override void OnRegister()
        {
            GameSaveView.ShowGameSavePanelSignal.AddListener(PopThisPanel);
        }

        public override void OnRemove()
        {
            GameSaveView.ShowGameSavePanelSignal.RemoveListener(PopThisPanel);
        }

        public void PopThisPanel()
        {
            PopPanelSignal.Dispatch();
        }
    }
}