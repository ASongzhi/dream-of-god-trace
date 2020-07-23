/*
 * FileName             : GameInfoPanelView.cs
 * Author               : zsz
 * Creat Date           : 2018.01.23
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.signal.impl;
using UnityEngine.UI;

namespace MYXZ
{
    /// <summary>
    /// 游戏信息面板
    /// </summary>
    public class GameInfoPanelView : BasePanelView
    {
        public Signal ExitGameInfoPanelSignal = new Signal();

        public void OnClickExitGameInfoPanel()
        {
            ExitGameInfoPanelSignal.Dispatch();
        }
    }
}