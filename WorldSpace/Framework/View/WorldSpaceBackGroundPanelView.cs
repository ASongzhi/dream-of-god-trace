/*
 * FileName             : WorldSpaceBackGroundPanelView.cs
 * Author               : yqs
 * Creat Date           : 2018.2.1
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
    /// 在地图上行走时的底部View
    /// </summary>
    public class WorldSpaceBackGroundPanelView : BasePanelView
    {
        public Signal OpenBagSignal = new Signal();

        public void OpenBagPanel()
        {
            OpenBagSignal.Dispatch();
        }

        public override void OnPause()
        {
            base.OnPause();
            MYXZInputManager.Instance.IsEnable = false;
        }

        public override void OnResume()
        {
            base.OnResume();
            MYXZInputManager.Instance.IsEnable = true;
        }
    }
}