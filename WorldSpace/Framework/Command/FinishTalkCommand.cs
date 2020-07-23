/*
 * FileName             : FinishTalkCommand.cs
 * Author               : yqs
 * Creat Date           : 2018.2.20
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
    /// 谈话结束时候的Command， Bind From FinishTalkSignal
    /// </summary>
    public class FinishTalkCommand : Command
    {
        [Inject]
        public bool IsTask { get; set; }

        [Inject]
        public string CurrentTaskId { get; set; }

        [Inject]
        public FinishTalkAndChooseSignal FinishTalkSignal { get; set; }

        [Inject]
        public TaskService TaskInfoService { get; set; }

        [Inject]
        public PushPanelSignal PushPanelSignal { get; set; }

        public override void Execute()
        {
            if (IsTask) //如果刚才的交谈是接取任务的交谈，弹出一个MessageBox来确定是否接受此任务
            {
                MessageBoxPanelView messageBox =
                    MYXZUIManager.Instance.GetPanel(UIPanelType.MessageBoxPanel) as MessageBoxPanelView;
                messageBox.CancelEvent = RefuseTask;    //按下取消键
                messageBox.ConfirmEvent = GetTask;      //按下确定键
                messageBox.MessageText.text = "是否接取任务" + MYXZGameDataManager.Instance.GetTaskById(CurrentTaskId).Name; //显示的消息
                PushPanelSignal.Dispatch(UIPanelType.MessageBoxPanel);   //弹出  
            }
            else        //如果只是平常的对话
            {
                MYXZUIManager.Instance.PopPanel();      //关闭对话框
                FinishTalkSignal.Dispatch();    //通知玩家对话已经结束
            }
        }

        /// <summary>
        /// 拒绝接受任务
        /// </summary>
        private void RefuseTask()
        {
            MYXZUIManager.Instance.PopPanel();      //关闭MessageBoxPanel
            MYXZUIManager.Instance.PopPanel();      //关闭TalkPanel
            FinishTalkSignal.Dispatch();    //通知玩家对话已经结束
        }

        /// <summary>
        /// 接受任务
        /// </summary>
        private void GetTask()
        {

            TaskInfoService.PlayerGetNewTask(CurrentTaskId); //向玩家的InfoModel中添加此任务
            MYXZUIManager.Instance.PopPanel();      //关闭MessageBoxPanel
            MYXZUIManager.Instance.PopPanel();      //关闭TalkPanel
            FinishTalkSignal.Dispatch();    //通知玩家对话已经结束
        }
    }
}