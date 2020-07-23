/*
 * FileName             : RequestGetMoneyCommand.cs
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
using strange.extensions.command.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 请求获取当前玩家拥有的金钱，Bind From RequestGetMoneySignal
    /// </summary>
    public class RequestGetPlayerMoneyCommand : Command
    {
        [Inject]
        public PlayerInfoModel PlayerInfoModel { get; set; }

        [Inject]
        public ResponseGetPlayerMoneySignal ResGetMoneySignal { get; set; }

        public override void Execute()
        {
            ResGetMoneySignal.Dispatch(PlayerInfoModel.Gold, PlayerInfoModel.Silver, PlayerInfoModel.Copper);
        }
    }
}
