/*
 * FileName             : SmallSettingBoxView.cs
 * Author               : yqs
 * Creat Date           : 2018.1.31
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
    /// 按下ESC键时弹出的小型设置窗口的View
    /// </summary>
    public class SmallSettingBoxView : BasePanelView
    {
        public Signal ReturnGameSignal = new Signal();
        public Signal ExitGameSignal = new Signal();
        public Signal SaveGameSignal = new Signal();

        public void ReturnGame()
        {
            ReturnGameSignal.Dispatch();
        }

        public void ExitGame()
        {
            ExitGameSignal.Dispatch();
        }

        public void SaveGame()
        {
            SaveGameSignal.Dispatch();
        }
    }
}