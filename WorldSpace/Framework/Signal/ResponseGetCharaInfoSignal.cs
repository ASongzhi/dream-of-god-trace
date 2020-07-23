/*
 * FileName             : ResponseGetCharaInfoSignal.cs
 * Author               : yqs
 * Creat Date           : 2018.3.29
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
    /// 返回当前角色信息，传递Character.Info，注入于PlayerMediator,EquipmentParentMediator
    /// </summary>
    public class ResponseGetCharaInfoSignal : Signal<BaseCharacter.Info>
    {
    }
}