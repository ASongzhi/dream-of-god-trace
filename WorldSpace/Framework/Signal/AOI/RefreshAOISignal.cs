/*
 * FileName             : AOISignal.cs
 * Author               : zsz
 * Creat Date           : 2018.10.6
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
    /// 发出更新AOI信号，传输物体的transform属性
    /// </summary>
    public class RefreshAOISignal : Signal<Transform>
    {

    }
}
