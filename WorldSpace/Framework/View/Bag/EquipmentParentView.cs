/*
 * FileName             : EquipmentParentView.cs
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
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace MYXZ
{
    /// <summary>
    /// 管理着EquipmentList的View
    /// </summary>
    public class EquipmentParentView : View
    {
        /// <summary>
        /// 用于复制创建的物体
        /// </summary>
        public GameObject EquipmentGameObject;
        public RectTransform ListRectTransform;
        public Signal<Equipment> ShowEquipmentChangeSignal = new Signal<Equipment>();

        public Image EquipmentImage;
        public Text EquipmentDescription;
        public Text UseDescription;

        [Space(10)]
        public Text CharacterInfo;
        public Text CurrentWeaponName;
        public Text CurrentHatName;
        public Text CurrentShoesName;
        public Text CurrentOrnament1Name;
        public Text CurrentClothesName;
        public Text CurrentOrnament2Name;

        /// <summary>
        /// 当哪一个装备高亮时，就显示该装备的信息
        /// </summary>
        /// <param name="equipment"></param>
        public void ShowEquipmentInfo(Equipment equipment)
        {
            EquipmentImage.sprite = equipment.Sprite;
            EquipmentDescription.text = equipment.ItemDescription;
            UseDescription.text = equipment.UseDescription;
            ShowEquipmentChangeSignal.Dispatch(equipment);
        }
    }
}