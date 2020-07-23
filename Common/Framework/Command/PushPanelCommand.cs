/*
 * FileName             : PushPanelCommand.cs
 * Author               : yqs
 * Creat Date           : 2018.2.3
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
    /// 弹出一个Panel，Bind From PushPanelSignal
    /// </summary>
    public class PushPanelCommand : Command
    {
        [Inject]
        public UIPanelType Type { get; set; }

        public override void Execute()
        {
            if (Type == UIPanelType.SmallSettingBoxPanel) //如果要弹出SmallSettingBoxPanel
            {
                if (MYXZUIManager.Instance.UIPanelStack.Peek() is WorldSpaceBackGroundPanelView) //只有当当前的Panel是WorldSpaceBackGroundPanelView时才可以
                {
                    MYXZUIManager.Instance.PushPanel(Type);
                }
            }
            else
            {
                MYXZUIManager.Instance.PushPanel(Type); //弹出对应type的panel
            }
        }
    }
}