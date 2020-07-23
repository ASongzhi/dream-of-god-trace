/*
 * FileName             : ItemParentView.cs
 * Author               : yqs
 * Creat Date           : 2018.3.18
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
    /// 管理itemList的View
    /// </summary>
    public class ItemParentView : View
    {
        /// <summary>
        /// 物品列表的物体
        /// </summary>
        public RectTransform ItemListRectTransform;
        /// <summary>
        /// 使用可使用的物品时出现的确定框
        /// </summary>
        public GameObject ItemUseBox;
        /// <summary>
        /// 将要实例化出的每个物品
        /// </summary>
        public GameObject ItemGameObject;

        /// <summary>
        /// 对物品的描述
        /// </summary>
        public Text ItemDescription;
        /// <summary>
        /// 物品的使用描述
        /// </summary>
        public Text UseDescription;
        /// <summary>
        /// 道具的图片
        /// </summary>
        public Image ItemImage;

        /// <summary>
        /// 显示此item的描述和使用描述
        /// </summary>
        /// <param name="item"></param>
        public void ShowItemInfo(Item item)
        {
            ItemDescription.text = item.ItemDescription;
            UseDescription.text = item.UseDescription;
            ItemImage.sprite = item.Sprite;
        }

        /// <summary>
        /// 显示使用物品的Box
        /// </summary>
        /// <param name="itemView"></param>
        public void ShowUseItemBox(ItemView itemView)
        {
            (ItemUseBox.transform.GetChild(0) as RectTransform).position =
                Input.mousePosition +
                new Vector3((ItemUseBox.transform.GetChild(0) as RectTransform).sizeDelta.x / 2, -(ItemUseBox.transform.GetChild(0) as RectTransform).sizeDelta.x / 2, 0);
            ItemUseBox.GetComponent<UseItemView>().CurrentItemId = itemView.Item.ID;
            ItemUseBox.SetActive(true);
        }

        void Update()
        {
            if (ItemUseBox.activeInHierarchy && Input.GetMouseButtonDown(1))    //当使用物品的Box激活时，如果按下了鼠标右键，关闭Box
            {
                ItemUseBox.SetActive(false);
            }
        }
    }
}