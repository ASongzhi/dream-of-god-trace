/*
 * FileName             : TaskTargetType.cs
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
    /// 任务种类  
    /// </summary>
    public enum TaskTargetType
    {
        /// <summary>
        /// 找到某人/某物
        /// </summary>
        Find = 0,
        /// <summary>
        /// 到达某处
        /// </summary>
        Arrive,
        /// <summary>
        /// 击败某人/某物
        /// </summary>
        Defeat,
        Level
    }
}
