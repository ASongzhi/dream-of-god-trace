/*
 * FileName             : Equipment.cs
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
    /// 装备
    /// </summary>
    public class Equipment : Item
    {
        public enum Type
        {
            Weapon = 0,
            Hat,
            Clothes,
            Shoes,
            /// <summary>
            /// 饰品
            /// </summary>
            Ornament
        }
        public Equipment.Type EquipmentType { get; private set; }
        public int PhysicalAttack { get; private set; }
        public int MagicAttack { get; private set; }
        public int PhysicalDefense { get; private set; }
        public int MagicDefense { get; private set; }
        public int HP { get; private set; }

        public Equipment(string name, string id, string spriteID, string itemDescription, string useDescription, int buyPrice, int salePrice,
            int physicalAttack, int magicAttack, int physicalDefense, int magicDefense, int hp, Equipment.Type type) : base(name, id, spriteID, itemDescription, useDescription, buyPrice, salePrice)
        {
            this.PhysicalAttack = physicalAttack;
            this.MagicAttack = magicAttack;
            this.PhysicalDefense = physicalDefense;
            this.MagicDefense = magicDefense;
            this.HP = hp;
            this.EquipmentType = type;
        }

        /// <summary>
        /// 创建一个空的装备
        /// </summary>
        public Equipment() : base()
        {
            
        }

        public override string ToString()
        {
            return string.Format("{0}, EquipmentType: {1}, PhysicalAttack: {2}, MagicAttack: {3}, PhysicalDefense: {4}, MagicDefense: {5}, HP: {6}", base.ToString(), EquipmentType, PhysicalAttack, MagicAttack, PhysicalDefense, MagicDefense, HP);
        }
    }
}