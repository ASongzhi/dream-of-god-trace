/*
 * FileName             : ResponseGetSaveinfoSignal.cs
 * Author               : hy
 * Creat Date           : 2018.4.22
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
    /// 返回角色的Transform信息
    /// </summary>
    public class ResponsePlayerTransformSignal : Signal<SaveInfo.Transform>
    {
    }
}