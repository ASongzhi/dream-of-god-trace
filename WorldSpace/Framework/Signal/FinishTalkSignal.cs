/*
 * FileName             : FinishTalkSignal.cs
 * Author               : yqs
 * Creat Date           : 2018.2.22
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
    /// 结束一段谈话时的Signal，传递一个bool值（是否是任务对话），string（对话的任务的ID） Bind To FinishTalkCommand
    /// </summary>
    public class FinishTalkSignal : Signal<bool, string>
    {
    }
}