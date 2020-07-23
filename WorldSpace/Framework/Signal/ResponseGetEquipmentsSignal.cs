/*
 * FileName             : ResponseGetEquipmentsSignal.cs
 * Author               : yqs
 * Creat Date           : 2018.3.28
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
    /// 接收到玩家装备的Signal，传递所有装备的ID，注入于EquipmentParentMediator
    /// </summary>
    public class ResponseGetEquipmentsSignal : Signal<List<string>>
    {
    }
}