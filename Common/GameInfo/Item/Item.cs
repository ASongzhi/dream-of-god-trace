/*
 * FileName             : Item.cs
 * Author               : yqs
 * Creat Date           : 2018.3.15
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 物品的基类
    /// </summary>
    public class Item
    {
        /// <summary>
        /// 物品名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 物品ID
        /// </summary>
        public string ID { get; private set; }
        /// <summary>
        /// 物品的图片
        /// </summary>
        public Sprite Sprite { get; private set; }
        /// <summary>
        /// 物品描述
        /// </summary>
        public string ItemDescription { get; private set; }
        /// <summary>
        /// 使用说明
        /// </summary>
        public string UseDescription { get; private set; }

        public bool CanBeSold { get; private set; }
        public int BuyPrice { get; private set; }
        public int SalePrice { get; private set; }

        public Item(string name, string id,string spriteID, string itemDescription, string useDescription, int buyPrice, int salePrice)
        {
            Name = name;
            ID = id;
            this.Sprite = MYXZGameDataManager.Instance.GetItemSpriteById(spriteID);
            ItemDescription = itemDescription;
            UseDescription = useDescription;
            CanBeSold = true;
            BuyPrice = buyPrice;
            SalePrice = salePrice;
        }

        public Item(string name, string id, string spriteID, string itemDescription, string useDescription)
        {
            Name = name;
            ID = id;
            this.Sprite = MYXZGameDataManager.Instance.GetItemSpriteById(spriteID);
            ItemDescription = itemDescription;
            UseDescription = useDescription;
            CanBeSold = false;
            BuyPrice = -1;
            SalePrice = -1;
        }

        /// <summary>
        /// 创建一个空的Item
        /// </summary>
        public Item()
        {
            
        }

        public override string ToString()
        {
            return string.Format("Name: {0}, ID: {1}, Sprite: {2}, ItemDescription: {3}, UseDescription: {4}, CanBeSold: {5}, BuyPrice: {6}, SalePrice: {7}", Name, ID, Sprite, ItemDescription, UseDescription, CanBeSold, BuyPrice, SalePrice);
        }
    }
}