/*
 * FileName             : LoadingSceneFinishCommand.cs
 * Author               : yqs
 * Creat Date           : 2018.1.29
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
    /// 加载场景完成,Bind From LoadingSceneFinishSignal
    /// </summary>
    public class LoadingSceneFinishCommand : Command
    {
        [Inject]
        public SceneChangeService SceneChangeService { get; set; }

        public override void Execute()
        {
            Retain();
            SceneChangeService.ActionsFinishSignal.AddListener(OnComplete);
            SceneChangeService.SceneChange(); //完成本场景的信息存储及下一场景信息加载
        }

        private void OnComplete(bool sceneChangeSuccess)
        {
            if (sceneChangeSuccess)
            {
                Debug.Log("Scene Change Success");
                MYXZUIManager.Instance.PopPanel();
                MYXZUIManager.Instance.PopAllPanel();
            }
            else
            {
                Debug.Log("Scene Change Failed");
            }
            Release();
        }
    }
}