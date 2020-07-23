/*
 * FileName             : UseItemMediator.cs
 * Author               : yqs
 * Creat Date           : 2018.3.27
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
    /// 使用物品时弹出的UseItemView的Mediator
    /// </summary>
    public class UseItemMediator : Mediator
    {
        [Inject]
        public UseItemView UseItemView { get; set; }

        [Inject]
        public RequestUseItemSignal RequestUseItemSignal { get; set; }

        public override void OnRegister()
        {
            UseItemView.UseItemSignal.AddListener(OnClickUseItem);
        }

        public override void OnRemove()
        {
            UseItemView.UseItemSignal.RemoveListener(OnClickUseItem);
        }

        private void OnClickUseItem()
        {
            UseItemView.gameObject.SetActive(false);
            RequestUseItemSignal.Dispatch(UseItemView.CurrentItemId);
        }
    }
}