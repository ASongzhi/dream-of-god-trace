/*
 * FileName             : EquipmentView.cs
 * Author               : yqs
 * Creat Date           : 2018.3.28
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace MYXZ
{
    /// <summary>
    /// 每个Equipment都对应一个EquipmentView
    /// </summary>
    public class EquipmentView : View
    {
        public EquipmentParentView EquipmentParentView;
        [SerializeField]
        private Text mEquipmentName;

        private ButtonEffectExtension mEffect;
        private bool mHasInit = false;
        private ButtonEffectExtension.State mPreEffectState;

        public Equipment CurrentEquipment;
        public Signal OnClickEquipmentSignal =  new Signal();

        private void Init()
        {
            mEffect = this.GetComponent<ButtonEffectExtension>();
            for (int i = 0; i < this.transform.childCount; i++)
            {
                if (this.transform.GetChild(i).name.Equals("Name"))
                {
                    mEquipmentName = transform.GetChild(i).GetComponent<Text>();
                }
            }
            this.GetComponent<Button>().onClick.AddListener(() =>
            {
                OnClickEquipmentSignal.Dispatch();
            });
            mHasInit = true;
        }

        public EquipmentView SetEquipment(string equipmentId)
        {
            if (!mHasInit)
            {
                Init();
            }
            CurrentEquipment = MYXZGameDataManager.Instance.GetItemOrEquipmentById(equipmentId) as Equipment;
            this.mEquipmentName.text = CurrentEquipment.Name;
            return this;
        }

        void FixedUpdate()
        {
            if (mEffect.CurrentState == ButtonEffectExtension.State.HighLight && mPreEffectState != ButtonEffectExtension.State.HighLight)
            {
                EquipmentParentView.ShowEquipmentInfo(this.CurrentEquipment);
            }
            mPreEffectState = mEffect.CurrentState;
        }
    }
}