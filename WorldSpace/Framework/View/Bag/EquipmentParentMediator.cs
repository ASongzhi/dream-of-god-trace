/*
 * FileName             : EquipmentParentMediator.cs
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
using System.Text;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 管理EquipmentList的EquipmentParentView的Mediator
    /// </summary>
    public class EquipmentParentMediator : Mediator
    {
        [Inject]
        public EquipmentParentView View { get; set; }

        [Inject]
        public RequestGetEquipmentsSignal ReqGetEquipmentsSignal { get; set; }

        [Inject]
        public ResponseGetEquipmentsSignal ResGetEquipmentsSignal { get; set; }

        [Inject]
        public RequestGetCharacterInfoSignal ReqGetCharaInfoSignal { get; set; }

        [Inject]
        public ResponseGetCharaInfoSignal ResGetCharaInfoSignal { get; set; }

        private bool mHasInit = false;
        private BaseCharacter.Info mCharaInfo;

        public override void OnRegister()
        {
            View.ShowEquipmentChangeSignal.AddListener(ShowEquipmentDifference);
            ResGetEquipmentsSignal.AddListener(ShowEquipments);
            ResGetCharaInfoSignal.AddListener(GetCharaInfo);
            ReqGetEquipmentsSignal.Dispatch();
            mHasInit = true;
        }

        public override void OnRemove()
        {
            View.ShowEquipmentChangeSignal.RemoveListener(ShowEquipmentDifference);
            ResGetEquipmentsSignal.RemoveListener(ShowEquipments);
            ResGetCharaInfoSignal.RemoveListener(GetCharaInfo);
        }

        void OnEnable()
        {
            if (mHasInit)
            {
                ReqGetEquipmentsSignal.Dispatch();
            }
        }

        private void ShowEquipments(List<string> equipmentIds)
        {
            for (int i = 0; i < View.ListRectTransform.childCount; i++)  //关闭所有EquipmentGameObject
            {
                View.ListRectTransform.GetChild(i).gameObject.SetActive(false);
            }
            for (int i = 0; i < equipmentIds.Count; i++)
            {
                if (i < View.ListRectTransform.childCount)        //如果此EquipmentList下的已实例化出的EquipmentGO足够使用
                {
                    GameObject equipmentGO = View.ListRectTransform.GetChild(i).gameObject;
                    equipmentGO.GetComponent<EquipmentView>().SetEquipment(equipmentIds[i]);  //将此EquipmentGameObject中的Equipment重新设置
                    equipmentGO.SetActive(true);
                }
                else
                {
                    GameObject equipmentGO = GameObject.Instantiate(View.EquipmentGameObject,     //实例化出新的ItemGameObject
                        View.ListRectTransform);
                    equipmentGO.AddComponent<EquipmentView>().SetEquipment(equipmentIds[i]).EquipmentParentView = this.View;
                    equipmentGO.SetActive(true);
                }
            }
        }

        private void GetCharaInfo(BaseCharacter.Info info)
        {
            mCharaInfo = info;
            View.CurrentWeaponName.text = mCharaInfo.Weapon == null ? "" : mCharaInfo.Weapon.Name;
            View.CurrentHatName.text = mCharaInfo.Hat == null ? "" : mCharaInfo.Hat.Name;
            View.CurrentClothesName.text = mCharaInfo.Clothes == null ? "" : mCharaInfo.Clothes.Name;
            View.CurrentShoesName.text = mCharaInfo.Shoes == null ? "" : mCharaInfo.Shoes.Name;
        }

        private void ShowEquipmentDifference(Equipment equipment)
        {
            if (mCharaInfo == null)
            {
                ReqGetCharaInfoSignal.Dispatch();
            }

            Equipment currentEquipment = equipment;
            switch (equipment.EquipmentType)        //如果玩家有装备，就赋给，没有的话就创建一个空的
            {
                case Equipment.Type.Weapon:
                    currentEquipment = mCharaInfo.Weapon ?? new Equipment();    
                    break;
                case Equipment.Type.Hat:
                    currentEquipment = mCharaInfo.Hat ?? new Equipment();
                    break;
                case Equipment.Type.Clothes:
                    currentEquipment = mCharaInfo.Clothes ?? new Equipment();
                    break;
                case Equipment.Type.Ornament:
                    currentEquipment = mCharaInfo.Ornament1 ?? new Equipment();
                    break;
            }

            StringBuilder sb = new StringBuilder("");
            int statsCount = 0;

            int difference = equipment.HP - currentEquipment.HP;
            if (difference != 0)
            {
                statsCount++;
                sb.Append("生命\t" + mCharaInfo.HP + " （" + (difference > 0 ? ("+" + difference) : difference.ToString()) + "）\t\t");
            }

            difference = equipment.MagicAttack - currentEquipment.MagicAttack;
            if (difference != 0)
            {
                statsCount++;
                sb.Append("术法攻击\t" + mCharaInfo.MagicAttack + " （" +
                          (difference > 0 ? ("+" + difference) : difference.ToString()) + "）"
                          + ((statsCount % 2 == 0 && statsCount > 0) ? "\n" : "\t\t"));
            }


            difference = equipment.MagicDefense - currentEquipment.MagicDefense;
            if (difference != 0)
            {
                statsCount++;
                sb.Append("术法防御\t" + mCharaInfo.MagicDefense + " （" +
                          (difference > 0 ? ("+" + difference) : difference.ToString())
                          + "）" + ((statsCount % 2 == 0 && statsCount > 0) ? "\n" : "\t\t"));
            }

            difference = equipment.PhysicalAttack - currentEquipment.PhysicalAttack;
            if (difference != 0)
            {
                statsCount++;
                sb.Append("物理攻击\t" + mCharaInfo.PhysicalAttack + " （" +
                          (difference > 0 ? ("+" + difference) : difference.ToString())
                          + "）" + ((statsCount % 2 == 0 && statsCount > 0) ? "\n" : "\t\t"));
            }

            difference = equipment.PhysicalDefense - currentEquipment.PhysicalDefense;
            if (difference != 0)
            {
                statsCount++;
                sb.Append("物理防御\t" + mCharaInfo.PhysicalDefense + " （" +
                          (difference > 0 ? ("+" + difference) : difference.ToString())
                          + "）" + ((statsCount % 2 == 0 && statsCount > 0) ? "\n" : "\t\t"));
            }
            View.CharacterInfo.text = sb.ToString();
        }
    }
}