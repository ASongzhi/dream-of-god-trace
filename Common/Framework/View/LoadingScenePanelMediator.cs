/*
 * FileName             : LoadingScenePanelMediator.cs
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
using strange.extensions.mediation.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 场景加载界面的Mediator
    /// </summary>
    public class LoadingScenePanelMediator : Mediator
    {
        [Inject]
        public LoadingScenePanelView LoadingSceneView { get; set; }

        [Inject]
        public LoadingSceneFinishSignal LoadingFinishSignal { get; set; }

        public override void OnRegister()
        {
            LoadingSceneView.LoadingFinish.AddListener(LoadSceneFinish);
        }

        public override void OnRemove()
        {
            LoadingSceneView.LoadingFinish.RemoveListener(LoadSceneFinish);
        }

        private void LoadSceneFinish()
        {
            LoadingFinishSignal.Dispatch(LoadingSceneView.TargetSceneName);
        }
    }
}