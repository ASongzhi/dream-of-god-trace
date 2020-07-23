/*
 * FileName             : ChangeSceneCommand.cs
 * Author               : yqs
 * Creat Date           : 2018.1.31
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
    /// 跳转场景， Bind From ChangeSceneSignal
    /// </summary>
    public class ChangeSceneCommand : Command
    {
        [Inject]
        public string TargetScene { get; set; }

        private Dictionary<string, string> mMessage = new Dictionary<string, string>();

        public override void Execute()
        {
            mMessage.Add("Scene01", "确定前往雾隐山 天青观？");
            mMessage.Add("StartScene", "返回开始界面？");
            mMessage.Add("Scene02", "确定前往苗疆 凤鸣镇？");
            MessageBoxPanelView MessageBoxView =
                MYXZUIManager.Instance.GetPanel(UIPanelType.MessageBoxPanel) as MessageBoxPanelView;
            MessageBoxView.MessageText.text = mMessage[TargetScene];
            MessageBoxView.ConfirmEvent = ChangeScene;
            MessageBoxView.CancelEvent = MYXZUIManager.Instance.PopPanel;
            MYXZUIManager.Instance.PushPanel(UIPanelType.MessageBoxPanel);
        }

        private void ChangeScene()
        {
            MYXZUIManager.Instance.PopPanel();
            LoadingScenePanelView LoadingSceneView =
                MYXZUIManager.Instance.GetPanel(UIPanelType.LoadingScenePanel) as LoadingScenePanelView;
            LoadingSceneView.TargetSceneName = TargetScene;
            MYXZUIManager.Instance.PushPanel(UIPanelType.LoadingScenePanel);
        }
    }
}