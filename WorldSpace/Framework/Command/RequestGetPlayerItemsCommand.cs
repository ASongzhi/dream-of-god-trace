/*
 * FileName             : RequestGetPlayerItemsCommand.cs
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
using strange.extensions.command.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 请求获取玩家拥有的Item（不包括Equipment）的Command，Bind From RequestGetPlayerItemsSignal
    /// </summary>
    public class RequestGetPlayerItemsCommand : Command
    {
        [Inject]
        public GameInfoService GameInfoService { get; set; }

        [Inject]
        public ResponseGetPlayerItemsSignal ResponseGetItemsSignal { get; set; }

        public override void Execute()
        {
            ResponseGetItemsSignal.Dispatch(GameInfoService.GetPlayerAllItems());
        }
    }
}