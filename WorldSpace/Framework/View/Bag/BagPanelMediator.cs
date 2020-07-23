/*
 * FileName             : BagPanelMediator.cs
 * Author               : yqs
 * Creat Date           : 2018.2.3
 * Revision History     : 
 *          R1: 
 *              修改作者：zsz
 *              修改日期：2018.9.15
 *              修改内容：新增方法Money
 */
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace MYXZ
{
    /// <summary>
    /// BagPanelView的Mediator
    /// </summary>
    public class BagPanelMediator : Mediator
    {
        [Inject]
        public BagPanelView BagView { get; set; }

        [Inject]
        public PopPanelSignal PopPanelSignal { get; set; }

        [Inject]
        public RequestGetPlayerItemsSignal ReqGetItemsSignal { get; set; }

        [Inject]
        public RequestGetTaskSignal ReqGetTaskSignal { get; set; }

        [Inject]
        public RequestGetPlayerMoneySignal ReqGetPlayerMoneySignal { get; set; }

        [Inject]
        public ResponseGetPlayerMoneySignal ResGetPlayerMoneySignal { get; set; }

        [Inject]
        public RequestGetCharacterInfoSignal ReqGetCharacterInfoSignal { get; set; }

        public override void OnRegister()
        {
            ResGetPlayerMoneySignal.AddListener(GetMoney);
            BagView.CloseSignal.AddListener(ClosePanel);
            BagView.BagOpenSignal.AddListener(BagOpen);
            BagOpen();
        }

        public override void OnRemove()
        {
            ResGetPlayerMoneySignal.RemoveListener(GetMoney);
            BagView.CloseSignal.RemoveListener(ClosePanel);
            BagView.BagOpenSignal.RemoveListener(BagOpen);
        }

        /// <summary>
        /// 关闭背包Panel
        /// </summary>
        private void ClosePanel()
        {
            PopPanelSignal.Dispatch();
        }

        private void BagOpen()
        {
            ReqGetItemsSignal.Dispatch();
            ReqGetTaskSignal.Dispatch();
            ReqGetPlayerMoneySignal.Dispatch();
            ReqGetCharacterInfoSignal.Dispatch();
        }

        private void GetMoney(int gold, int silver, int copper)
        {
            BagView.ShowMoney(gold, silver, copper);
        }
    }
}