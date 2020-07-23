/*
 * FileName             : BeginTalkSignal.cs
 * Author               : yqs
 * Creat Date           : 2018.2.20
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using strange.extensions.signal.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 开始交谈, 传递交谈的NPC的ID，Bind To BeginTalkSignal
    /// </summary>
    public class BeginTalkSignal : Signal<string>
    {
    }
}