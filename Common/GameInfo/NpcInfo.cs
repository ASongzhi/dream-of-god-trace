/*
 * FileName             : NpcInfo.cs
 * Author               : ZSZ
 * Creat Date           : 2018.2.12
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MYXZ
{
    /// <summary>     
    /// 储存NPC信息类  
    /// </summary>
    public class NpcInfo
    {
        public string Name;
        public string Id;
        public string[] TaskIds;

        public Sprite Sprite;

        /// <summary>
        /// 与这个NPC的平常对话
        /// </summary>
        public Talk[] Talks;

        public NpcInfo()
        {
            TaskIds = new string[0];
            Talks = new Talk[0];
        }
    }
}