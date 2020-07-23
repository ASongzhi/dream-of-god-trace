/*
 * FileName             : Material.cs
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
    /// 材料
    /// </summary>
    public class Material : Item
    {
        public Material(string name, string id, string spriteID, string itemDescription, string useDescription, int buyPrice, int salePrice) : base(name, id, spriteID, itemDescription, useDescription, buyPrice, salePrice)
        {
        }

        public override string ToString()
        {
            return string.Format("{0}", base.ToString());
        }
    }
}