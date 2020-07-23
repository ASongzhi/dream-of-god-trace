/*
 * FileName             : GameStartMenuPanelMediator.cs
 * Author               : yqs
 * Creat Date           : 2017.12.28
 * Revision History     : 
 *          R1:                                                     R2:
 *              修改作者：zsz                                            修改作者：hy
 *              修改日期：2018.01.23                                     修改日期：2018.01.26
 *              修改内容：添加了GameInfoSignal相关内容                   修改内容：添加了GameSaveSignal相关内容
 *          R3:
 *              修改作者：yqs
 *              修改日期：2018.2.3
 *              修改内容：使用PushPanelSignal来弹出Panel，去除重复代码
 */

using strange.extensions.context.api;
using strange.extensions.mediation.impl;

namespace MYXZ
{
    /// <summary>
    /// 开始菜单Mediator
    /// </summary>
    public class GameStartMenuPanelMediator : Mediator
    {
        [Inject]
        public GameStartMenuPanelView StartMenuView { get; set; }

        [Inject]
        public ChangeSceneSignal ChangeSceneSignal { get; set; }

        [Inject]
        public ExitGameSignal ExitGameSignal { get; set; }

        [Inject]
        public PushPanelSignal PushPanelSignal { get; set; }

        public override void OnRegister()
        {
            StartMenuView.NewGame.AddListener(OnClickCreatNewGame);
            StartMenuView.ExitGame.AddListener(OnClickExitGame);
            StartMenuView.GameInfo.AddListener(OnClickGameInfo);
            StartMenuView.GameSave.AddListener(OnClickGameSave);
        }

        public override void OnRemove()
        {
            StartMenuView.NewGame.RemoveListener(OnClickCreatNewGame);
            StartMenuView.ExitGame.RemoveListener(OnClickExitGame);
            StartMenuView.GameInfo.RemoveListener(OnClickGameInfo);
            StartMenuView.GameSave.RemoveListener(OnClickGameSave);
        }

        private void OnClickCreatNewGame()
        {
            ChangeSceneSignal.Dispatch("Scene01");
        }

        private void OnClickExitGame()
        {
            ExitGameSignal.Dispatch();
        }

        private void OnClickGameInfo()
        {
            PushPanelSignal.Dispatch(UIPanelType.GameInfoPanel);
        }

        private void OnClickGameSave()
        {
            PushPanelSignal.Dispatch(UIPanelType.GameSavePanel);
        }
    }
}