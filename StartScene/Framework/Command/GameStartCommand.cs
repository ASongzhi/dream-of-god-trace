/*
 * FileName             : GameStartCommand.cs
 * Author               : yqs
 * Creat Date           : 2017.12.10
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 游戏开启时创建此Command，做初始化
    /// </summary>
    public class GameStartCommand : Command
    {
        public override void Execute()
        {
            MYXZUIManager.Instance.PushPanel(UIPanelType.GameStartMenuPanel);
        }
    }
}