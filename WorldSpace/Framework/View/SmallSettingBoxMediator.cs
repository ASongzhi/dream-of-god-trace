/*
 * FileName             : SmallSettingBoxMediator.cs
 * Author               : yqs
 * Creat Date           : 2018.1.31
 * Revision History     : 
 *          R1: 
 *              修改作者：hy
 *              修改日期：2018.4.21
 *              修改内容：添加了游戏存档
 */
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 按下ESC键时弹出的小型设置窗口的Mediator
    /// </summary>
    public class SmallSettingBoxMediator : Mediator
    {
        [Inject]
        public SmallSettingBoxView SmallSettingView { get; set; }

        [Inject]
        public PopPanelSignal PopPanelSignal { get; set; }

        [Inject]
        public ChangeSceneSignal ChangeSceneSignal { get; set; }

        [Inject]
        public RequestSaveArchiveSignal RequestSaveArchiveSignal { get; set; }

        public override void OnRegister()
        {
            SmallSettingView.ReturnGameSignal.AddListener(ClosePanel);
            SmallSettingView.ExitGameSignal.AddListener(ReturnToStarScene);
            SmallSettingView.SaveGameSignal.AddListener(SaveGame);
        }

        public override void OnRemove()
        {
            SmallSettingView.ReturnGameSignal.RemoveListener(ClosePanel);
            SmallSettingView.ExitGameSignal.RemoveListener(ReturnToStarScene);
            SmallSettingView.SaveGameSignal.RemoveListener(SaveGame);
        }

        private void ClosePanel()
        {
            PopPanelSignal.Dispatch();
        }

        private void ReturnToStarScene()
        {
            ChangeSceneSignal.Dispatch("StartScene");
        }

        private void SaveGame()
        {
            RequestSaveArchiveSignal.Dispatch(GameObject.FindGameObjectWithTag("Player").transform);
        }
    }
}