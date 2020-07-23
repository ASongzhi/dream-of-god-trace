/*
 * FileName             : ItemParentMediator.cs
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
using System.Linq;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 管理ItemList的ItemParentView的Mediator
    /// </summary>
    public class ItemParentMediator : Mediator
    {
        [Inject]
        public ItemParentView ItemParentView { get; set; }

        [Inject]
        public RequestGetPlayerItemsSignal RequestGetItemsSignal { get; set; }

        [Inject]
        public ResponseGetPlayerItemsSignal ResponseGetItemsSignal { get; set; }

        public override void OnRegister()
        {
            ResponseGetItemsSignal.AddListener(GetPlayerItems);
            RequestGetItemsSignal.Dispatch();
        }

        public override void OnRemove()
        {
            ResponseGetItemsSignal.RemoveListener(GetPlayerItems);
        }

        private void GetPlayerItems(Dictionary<string, int> items)
        {
            for (int i = 0; i < ItemParentView.ItemListRectTransform.childCount;i++)
            {
                ItemParentView.ItemListRectTransform.GetChild(i).gameObject.SetActive(false);
            }
            for (int i = 0; i < items.Count; i++)
            {
                if (i < ItemParentView.ItemListRectTransform.childCount)        //如果此ItemList下的已实例化出的Item足够使用
                {
                    GameObject itemGO = ItemParentView.ItemListRectTransform.GetChild(i).gameObject;
                    itemGO.GetComponent<ItemView>().SetItem(items.Keys.ElementAt(i), items.Values.ElementAt(i));  //将此ItemGameObject中的Item重新设置
                    itemGO.SetActive(true);
                }
                else
                {
                    GameObject itemGO = GameObject.Instantiate(ItemParentView.ItemGameObject,     //实例化出新的ItemGameObject
                        ItemParentView.ItemListRectTransform);
                    itemGO.AddComponent<ItemView>().SetItem(items.Keys.ElementAt(i), items.Values.ElementAt(i))   //添加一个ItemView并设置Item
                        .ItemParentView = this.ItemParentView;
                    itemGO.SetActive(true);
                }
            }
        }
    }
}