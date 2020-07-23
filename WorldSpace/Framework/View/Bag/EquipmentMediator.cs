/*
 * FileName             : EquipmentMediator.cs
 * Author               : yqs
 * Creat Date           : 2018.3.29
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
    /// EquipmentView的Mediator
    /// </summary>
    public class EquipmentMediator : Mediator
    {
        [Inject]
        public EquipmentView EquipmentView { get; set; }

        /// <summary>
        /// 请求装备当前选择的装备
        /// </summary>
        [Inject]
        public RequestUseItemSignal ReqEquipEquipmentSignal { get; set; }

        public override void OnRegister()
        {
            EquipmentView.OnClickEquipmentSignal.AddListener(RequestEquipEquipment);
        }

        public override void OnRemove()
        {
            EquipmentView.OnClickEquipmentSignal.RemoveListener(RequestEquipEquipment);
        }

        private void RequestEquipEquipment()
        {
            ReqEquipEquipmentSignal.Dispatch(EquipmentView.CurrentEquipment.ID);
        }
    }
}