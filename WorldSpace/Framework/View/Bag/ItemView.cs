/*
 * FileName             : ItemView.cs
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
using UnityEngine;
using UnityEngine.UI;

namespace MYXZ
{
    /// <summary>
    /// 每个ItemGameObject对应一个ItemView
    /// </summary>
    public class ItemView : View
    {
        public ItemParentView ItemParentView;
        [SerializeField]
        private Image mItemImage;
        [SerializeField]
        private Text mItemName;
        [SerializeField]
        private Text mItemCount;

        private ButtonEffectExtension mEffect;
        private bool mHasInit = false;

        public Item Item;

        private void Init()
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                if (this.transform.GetChild(i).name.Equals("Sprite"))
                {
                    mItemImage = transform.GetChild(i).GetComponent<Image>();
                }
                else if (this.transform.GetChild(i).name.Equals("Name"))
                {
                    mItemName = transform.GetChild(i).GetComponent<Text>();
                }
                else if (this.transform.GetChild(i).name.Equals("Count"))
                {
                    mItemCount = transform.GetChild(i).GetComponent<Text>();
                }
            }
            mEffect = GetComponent<ButtonEffectExtension>();
            this.GetComponent<Button>().onClick.AddListener(delegate
            {
                if (this.Item.ID.Substring(0, 2).Equals("03")) //如果此物品是消耗品
                {
                    ItemParentView.ShowUseItemBox(this);
                }
            });

            mHasInit = true;
        }

        public ItemView SetItem(string itemId, int count)
        {
            if (!mHasInit)
            {
                Init();
            }
            Item = MYXZGameDataManager.Instance.GetItemOrEquipmentById(itemId);
            this.mItemImage.sprite = Item.Sprite;
            this.mItemName.text = Item.Name;
            this.mItemCount.text = count.ToString();
            return this;
        }

        void FixedUpdate()
        {
            if (mEffect.CurrentState == ButtonEffectExtension.State.HighLight)
            {
                ItemParentView.ShowItemInfo(this.Item);
            }
        }
    }
}