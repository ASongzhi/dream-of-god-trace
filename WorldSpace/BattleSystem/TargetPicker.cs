/*
 * FileName             : TargetPicker.cs
 * Author               : yqs
 * Creat Date           : 2018.9.26
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
    /// 攻击区域
    /// </summary>
    public abstract class TargetPicker
    {
        public abstract List<Transform> Pick(Transform player, List<Transform> targets);
    }
}