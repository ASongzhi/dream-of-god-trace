/*
 * FileName             : RequestSaveInfoCommand.cs
 * Author               : hy
 * Creat Date           : 2018.4.29
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using strange.extensions.command.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 请求获取主角坐标信息的Command, Bind from RequestSaveInfoSignal
    /// </summary>
    public class RequestGetPlayerTransformCommand : Command
    {
        [Inject]
        public ResponsePlayerTransformSignal ResPlayerTransformSignal { get; set; }

        public override void Execute()
        {
            if (MYXZGameDataManager.Instance.CurrentSaveInfo != null)
            {
                ResPlayerTransformSignal.Dispatch(MYXZGameDataManager.Instance.CurrentSaveInfo.GetplayerTransform());
            }
            else
            {
                Debug.LogError("不存在存档！");
            }
        }
    }
}
