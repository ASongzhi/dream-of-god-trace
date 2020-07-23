/*
 * FileName             : ResponseGetPlayerItemsSignal.cs
 * Author               : yqs
 * Creat Date           : 2018.3.21
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
    /// 接收玩家所拥有的所有Item及每个Item的数量，注入于ItemParentMediator
    /// </summary>
    public class ResponseGetPlayerItemsSignal : Signal<Dictionary<string, int>>
    {
    }
}