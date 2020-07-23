/*
 * FileName             : TaskItem.cs
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
    /// 任务物品，不可出售
    /// </summary>
    public class TaskItem : Item
    {
        public TaskItem(string name, string id, string spriteID, string itemDescription, string useDescription
            ) : base(name, id, spriteID, itemDescription, useDescription)
        {
        }

        public override string ToString()
        {
            return string.Format("{0}", base.ToString());
        }
    }
}