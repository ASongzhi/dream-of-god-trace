/*
 * FileName             : GameStartMenuPanelView.cs
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
using strange.extensions.signal.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 开始菜单View
    /// </summary>
    public class GameStartMenuPanelView : BasePanelView
    {
        public Signal NewGame = new Signal();
        public Signal GameSave = new Signal();
        public Signal GameInfo = new Signal();
        public Signal ExitGame = new Signal();

        public void OnClickNewGame()
        {
            NewGame.Dispatch();
        }

        public void OnClickGameSave()
        {
            GameSave.Dispatch();
        }

        public void OnClickGameInfo()
        {
            GameInfo.Dispatch();
        }

        public void OnClickExitGame()
        {
            ExitGame.Dispatch();
        }
    }
}