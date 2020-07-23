/*
 * FileName             : PopPanelCommand.cs
 * Author               : yqs
 * Creat Date           : 2018.1.24
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
    /// 弹出栈顶Panel,Bind From PopPanelSignal
    /// </summary>
    public class PopPanelCommand : Command
    {
        public override void Execute()
        {
            MYXZUIManager.Instance.PopPanel();
        }
    }
}