/*
 * FileName             : WorldSpaceBackGroundPanelMediator.cs
 * Author               : yqs
 * Creat Date           : 2018.2.1
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
    /// 在地图上行走时的底部PanelView的Mediator
    /// </summary>
    public class WorldSpaceBackGroundPanelMediator : Mediator
    {
        [Inject]
        public WorldSpaceBackGroundPanelView WorldSpaceBackGroundView { get; set; }

        [Inject]
        public PushPanelSignal PushPanelSignal { get; set; }

        public override void OnRegister()
        {
            WorldSpaceBackGroundView.OpenBagSignal.AddListener(PushBagPanel);
        }

        public override void OnRemove()
        {
            WorldSpaceBackGroundView.OpenBagSignal.RemoveListener(PushBagPanel);
        }

        private void PushBagPanel()
        {
            PushPanelSignal.Dispatch(UIPanelType.BagPanel);
        }
    }
}