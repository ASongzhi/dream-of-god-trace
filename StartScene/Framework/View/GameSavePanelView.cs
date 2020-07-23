/*
 * FileName             : GameSavePanelView.cs
 * Author               : hy
 * Creat Date           : 2018.01.26
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using strange.extensions.signal.impl;

namespace MYXZ
{
    /// <summary>
    /// 读取存档面板
    /// </summary>
    public class GameSavePanelView : BasePanelView
    {
        public Signal ShowGameSavePanelSignal = new Signal();

        public void OnClickExitGameSavePanel()
        {
            ShowGameSavePanelSignal.Dispatch();
        }
    }
}