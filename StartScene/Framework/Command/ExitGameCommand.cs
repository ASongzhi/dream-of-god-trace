/*
 * FileName             : ExitGameCommand.cs
 * Author               : yqs
 * Creat Date           : 2017.12.28
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using strange.extensions.command.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 来自ExitGameSignal
    /// </summary>
    public class ExitGameCommand : Command
    {
        public override void Execute()
        {
            MessageBoxPanelView messagePanel =
                MYXZUIManager.Instance.GetPanel(UIPanelType.MessageBoxPanel) as MessageBoxPanelView;
            messagePanel.MessageText.text = "是否退出游戏？";
            messagePanel.ConfirmEvent = Application.Quit;
            messagePanel.CancelEvent = delegate { MYXZUIManager.Instance.PopPanel(); };

            MYXZUIManager.Instance.PushPanel(UIPanelType.MessageBoxPanel); //弹出消息框
        }
    }
}