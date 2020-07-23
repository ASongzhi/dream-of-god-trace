/*
 * FileName             : RequestGetEquipmentsCommand.cs
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
using strange.extensions.command.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 请求获取当前玩家拥有的装备，Bind From RequestGetEquipmentsSignal
    /// </summary>
    public class RequestGetEquipmentsCommand : Command
    {
        [Inject]
        public ResponseGetEquipmentsSignal ResponseGetEquipmentsSignal { get; set; }

        [Inject]
        public GameInfoService GameInfoService { get; set; }

        public override void Execute()
        {
            ResponseGetEquipmentsSignal.Dispatch(GameInfoService.GetPlayerAllEquipments());
        }
    }
}