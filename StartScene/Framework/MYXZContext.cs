/*
 * FileName             : MYXZContext.cs
 * Author               : yqs
 * Creat Date           : 2017.12.10
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 在此类中完成所有Binding
    /// </summary>
    public class MYXZContext : MVCSContext
    {
        public MYXZContext(MonoBehaviour view) : base(view)
        {

        }

        // Unbind the default EventCommandBinder and rebind the SignalCommandBinder
        protected override void addCoreComponents()
        {
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }

        // Override Start so that we can fire the StartSignal 
        public override IContext Start()
        {
            base.Start();
            GameStartSignal gameStartSignal = (GameStartSignal) injectionBinder.GetInstance<GameStartSignal>();
            gameStartSignal.Dispatch();
            return this;
        }

        protected override void mapBindings()
        {
            #region Model
            #endregion

            #region Mediator

            mediationBinder.BindView<GameStartMenuPanelView>().ToMediator<GameStartMenuPanelMediator>();
            mediationBinder.BindView<LoadingScenePanelView>().ToMediator<LoadingScenePanelMediator>();
            mediationBinder.BindView<GameInfoPanelView>().ToMediator<GameInfoPanelMediator>();
            mediationBinder.BindView<GameSavePanelView>().ToMediator<GameSavePanelMediator>();

            #endregion

            #region Command

            commandBinder.Bind<PopPanelSignal>().To<PopPanelCommand>();
            commandBinder.Bind<PushPanelSignal>().To<PushPanelCommand>();
            commandBinder.Bind<ChangeSceneSignal>().To<ChangeSceneCommand>();
            commandBinder.Bind<LoadingSceneFinishSignal>().To<LoadingSceneFinishCommand>();
            commandBinder.Bind<ExitGameSignal>().To<ExitGameCommand>(); //绑定退出游戏的信号和退出游戏的指令(StartScene)
            commandBinder.Bind<RequestLoadArchiveSignal>().To<RequestLoadArchiveCommand>();

            #endregion
            #region Service
            injectionBinder.Bind<SceneChangeService>().ToSingleton();

            #endregion

            commandBinder.Bind<GameStartSignal>().To<GameStartCommand>()
                .Once(); //GameStartSignal在游戏开始时自动调用，创建GameStartCommand，创建后解除Bind

        }
    }
}