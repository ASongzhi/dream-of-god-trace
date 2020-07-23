/*
 * FileName             : WorldSpaceContext.cs
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
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 世界场景的Bind初始化，
    /// </summary>
    public class WorldSpaceContext : MVCSContext
    {
        public WorldSpaceContext(MonoBehaviour view) : base(view)
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
            WorldSpaceOpenSignal sceneOpenSignal = injectionBinder.GetInstance<WorldSpaceOpenSignal>();
            sceneOpenSignal.Dispatch();
            return this;
        }

        protected override void mapBindings()
        {
            #region Signal
            injectionBinder.Bind<FinishTalkAndChooseSignal>().ToSingleton();
            injectionBinder.Bind<ResponseGetPlayerItemsSignal>().ToSingleton();
            injectionBinder.Bind<ResponseGetCharaInfoSignal>().ToSingleton();
            injectionBinder.Bind<ResponseGetEquipmentsSignal>().ToSingleton();
            injectionBinder.Bind<ResponseGetTaskSignal>().ToSingleton();
            injectionBinder.Bind<ResponsePlayerTransformSignal>().ToSingleton();
            injectionBinder.Bind<ResponseGetPlayerMoneySignal>().ToSingleton();
            #endregion

            #region Model
            injectionBinder.Bind<PlayerInfoModel>().ToSingleton();  //角色信息
			injectionBinder.Bind<AOIInfoModel>().ToSingleton();//AOI信息
            #endregion

            #region Mediator
            mediationBinder.BindView<WorldSpaceBackGroundPanelView>().ToMediator<WorldSpaceBackGroundPanelMediator>();
            mediationBinder.BindView<LoadingScenePanelView>().ToMediator<LoadingScenePanelMediator>();
            mediationBinder.BindView<PlayerView>().ToMediator<PlayerMediator>();
            mediationBinder.BindView<SmallSettingBoxView>().ToMediator<SmallSettingBoxMediator>();
            mediationBinder.BindView<BagPanelView>().ToMediator<BagPanelMediator>();
            mediationBinder.BindView<TalkPanelView>().ToMediator<TalkPanelMediator>();
            mediationBinder.BindView<NpcView>().ToMediator<NpcMediator>();
            mediationBinder.BindView<ItemView>().ToMediator<ItemMediator>();
            mediationBinder.BindView<ItemParentView>().ToMediator<ItemParentMediator>();
            mediationBinder.BindView<UseItemView>().ToMediator<UseItemMediator>();
            mediationBinder.BindView<EquipmentParentView>().ToMediator<EquipmentParentMediator>();
            mediationBinder.BindView<EquipmentView>().ToMediator<EquipmentMediator>();
            mediationBinder.BindView<TaskParentView>().ToMediator<TaskParentMediator>();
            mediationBinder.BindView<TaskView>().ToMediator<TaskMediator>();
            mediationBinder.BindView<TeamView>().ToMediator<TeamMediator>();
            #endregion

            #region Command
            commandBinder.Bind<PopPanelSignal>().To<PopPanelCommand>();
            commandBinder.Bind<PushPanelSignal>().To<PushPanelCommand>();
            commandBinder.Bind<ChangeSceneSignal>().To<ChangeSceneCommand>();
            commandBinder.Bind<LoadingSceneFinishSignal>().To<LoadingSceneFinishCommand>();
            commandBinder.Bind<BeginTalkSignal>().To<BeginTalkCommand>();
            commandBinder.Bind<FinishTalkSignal>().To<FinishTalkCommand>();
            commandBinder.Bind<RequestGetPlayerItemsSignal>().To<RequestGetPlayerItemsCommand>();
            commandBinder.Bind<RequestUseItemSignal>().To<RequestUseItemCommand>();
            commandBinder.Bind<RequestGetEquipmentsSignal>().To<RequestGetEquipmentsCommand>();
            commandBinder.Bind<RequestGetCharacterInfoSignal>().To<RequestGetCharacterInfoCommand>();
            commandBinder.Bind<RequestGetTaskSignal>().To<RequestGetTaskCommand>();
            commandBinder.Bind<RequestSaveArchiveSignal>().To<RequestSaveArchiveCommand>();
            commandBinder.Bind<RequestGetPlayerTransformSignal>().To<RequestGetPlayerTransformCommand>();
            commandBinder.Bind<RequestLoadArchiveSignal>().To<RequestLoadArchiveCommand>();
            commandBinder.Bind<RequestGetPlayerMoneySignal>().To<RequestGetPlayerMoneyCommand>();
            commandBinder.Bind<RefreshAOISignal>().To<RefreshAOICommond>();
            commandBinder.Bind<RegisterSkillSignal>().To<RegisterSkillCommand>();
            commandBinder.Bind<GetSkillInputSignal>().To<GetSkillInputCommand>();
			#endregion

            #region Service
            injectionBinder.Bind<SceneChangeService>().ToSingleton();
            injectionBinder.Bind<GameObjectPoolService>().ToSingleton();
            injectionBinder.Bind<GameInfoService>().ToSingleton();
            injectionBinder.Bind<TaskService>().ToSingleton();
            injectionBinder.Bind<AOIService>().ToSingleton();
            injectionBinder.Bind<SkillService>().ToSingleton();
            #endregion

            commandBinder.Bind<WorldSpaceOpenSignal>().To<WorldSpaceOpenCommand>().Once(); //此场景打开时调用
        }
    }
}