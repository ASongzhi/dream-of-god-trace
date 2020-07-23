/*
 * FileName             : WorldSpaceOpenCommand.cs
 * Author               : yqs
 * Creat Date           : 2018.1.30
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MYXZ
{
    /// <summary>
    /// 世界场景被打开时调用，做初始化,Bind From WorldSpaceOpenSignal
    /// </summary>
    public class WorldSpaceOpenCommand : Command
    {
        [Inject]
        public RequestLoadArchiveSignal ReqLoadArchiveSignal { get; set; }

        public override void Execute()
        {
            MYXZInputManager.Instance.IsEnable = true;
            MYXZGameDataManager.Instance.Init();
            MYXZUIManager.Instance.PushPanel(UIPanelType.WorldSpaceBackGroundPanel);
            //ReqLoadArchiveSignal.Dispatch();
            MYXZAssetBundleManager.Instance.LoadOrGetAssetBundle("ui/startsceneui");
        }
    }
}