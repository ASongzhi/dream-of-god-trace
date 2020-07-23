/*
 * FileName             : Consumable.cs
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
    /// 消耗品
    /// </summary>
    public class Consumable : Item
    {
        public int HP { get; private set; }
        public Consumable(string name, string id, string spriteID, string itemDescription, string useDescription, int buyPrice, int salePrice
            ,int hp) : base(name, id, spriteID, itemDescription, useDescription, buyPrice, salePrice)
        {
            this.HP = hp;
        }

        public override string ToString()
        {
            return string.Format("{0}, HP: {1}", base.ToString(), HP);
        }
    }

}