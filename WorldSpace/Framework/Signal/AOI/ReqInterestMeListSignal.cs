/*
 * FileName             : ReqInterestMeListSignal.cs
 * Author               : yqs
 * Creat Date           : 2018.10.14
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
    /// 请求获得对当前目标感兴趣的物体，发送Transform，Bind To ReqInterestMeListCommand
    /// </summary>
    public class ReqInterestMeListSignal : Signal<Transform>
    {
    }
}