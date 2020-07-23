/*
 * FileName             : RequestUseItemCommand.cs
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
    /// 玩家使用了任何物品(包括装备)时的Command,Bind From RequestUseItemSignal,接受一个ItemId
    /// </summary>
    public class RequestUseItemCommand : Command
    {
        [Inject]
        public string ItemId { get; set; }

        [Inject]
        public GameInfoService GameInfoService { get; set; }

        [Inject]
        public RequestGetPlayerItemsSignal ReqGetPlayerItemsSignal { get; set; }

        [Inject]
        public ResponseGetCharaInfoSignal ResGetCharaInfoSignal { get; set; }

        public override void Execute()
        {
            string itemType = ItemId.Substring(0, 2);
            if (itemType.Equals("03"))                  //如果是普通物品
            {
                GameInfoService.PlayerUseItem(ItemId);  //玩家使用该物品
                ReqGetPlayerItemsSignal.Dispatch();     //请求获得玩家当前的物品列表
            }
            else if (itemType.Equals("04"))             //如果是装备
            {
                GameInfoService.PlayerEquipEquipment(ItemId);   //玩家装备上此物品
                ResGetCharaInfoSignal.Dispatch(GameInfoService.GetCharaInfo());   //发送玩家当前属性信息
            }
        }
    }
}