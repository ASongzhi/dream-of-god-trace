/*
 * FileName             : RequestGetCharacterInfoCommand.cs
 * Author               : yqs
 * Creat Date           : 2018.4.22
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
    /// 请求获得当前角色信息的Command，Bind From RequestGetCharacterInfoSignal
    /// </summary>
    public class RequestGetCharacterInfoCommand : Command
    {
        [Inject]
        public ResponseGetCharaInfoSignal ResGetCharaInfoSignal { get; set; }

        [Inject]
        public GameInfoService GameInfoService { get; set; }

        public override void Execute()
        {
            ResGetCharaInfoSignal.Dispatch(GameInfoService.GetCharaInfo());
        }
    }
}