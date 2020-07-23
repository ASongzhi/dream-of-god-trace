/*
 * FileName             : BagPanelView.cs
 * Author               : yqs
 * Creat Date           : 2018.2.3
 * Revision History     : 
 *          R1: 
 *              修改作者：zsz
 *              修改日期：2018.9.15
 *              修改内容：新增方法ShowMoney
 */
using System.Collections;
using System.Collections.Generic;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace MYXZ
{
    /// <summary>
    /// 背包界面的View
    /// </summary>
    public class BagPanelView : BasePanelView
    {
        public Signal CloseSignal = new Signal();
        public Signal BagOpenSignal = new Signal();
        public Text GoldText;
        public Text SilverText;
        public Text CopperText;

        /// <summary>
        /// 控制player当前拥有的金钱显示
        /// </summary>
        public void ShowMoney(int gold, int silver, int copper)
        {
            GoldText.text = gold.ToString();
            SilverText.text = silver.ToString();
            CopperText.text = copper.ToString();
        }

        /// <summary>
        /// 绑定在了Panel上的关闭Button上
        /// </summary>
        public void OnClickClose()
        {
            CloseSignal.Dispatch();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            BagOpenSignal.Dispatch();
        }

        public override void OnResume()
        {
            base.OnResume();
            BagOpenSignal.Dispatch();
        }
    }
}