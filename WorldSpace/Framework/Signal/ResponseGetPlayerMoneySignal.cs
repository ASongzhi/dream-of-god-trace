﻿/*
 * FileName             : ResponseGetInfoSignal.cs
 * Author               : zsz
 * Creat Date           : 2018.9.14
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
    /// 接收到玩家Copper的Signal，传递Gold,Silver,Copper，注入于BagPanelMediator
    /// </summary>
    public class ResponseGetPlayerMoneySignal : Signal<int, int, int>
    {
    }
}

