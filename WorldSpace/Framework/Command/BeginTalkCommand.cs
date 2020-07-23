/*
 * FileName             : BeginTalkCommand.cs
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
    /// 开始交谈的Command， Bind From BeginTalkSignal
    /// </summary>
    public class BeginTalkCommand : Command
    {
        /// <summary>
        /// 对话的NPC的ID
        /// </summary>
        [Inject]
        public string ID { get; set; }

        [Inject]
        public TaskService TaskInfoService { get; set; }

        [Inject]
        public PushPanelSignal PushPanelSignal { get; set; }

        public override void Execute()
        {
            TalkPanelView talkPanel = MYXZUIManager.Instance.GetPanel(UIPanelType.TalkPanel) as TalkPanelView;

            List<string> taskIds;
            TaskInfoService.CheckTaskFinsh(ID);

            if (TaskInfoService.IsInTaskOfNpc(ID, out taskIds))                     //如果Player正在执行此NPC的任务
            {
                talkPanel.CurrentTalks = MYXZGameDataManager.Instance.GetTaskById(taskIds[0]).InTaskTalk;
                talkPanel.IsTaskTalk = false;
            }
            else if ((taskIds = TaskInfoService.GetAcceptableTasksInNpc(ID)).Count >= 1) //如果此NPC当前有可以接取的任务
            {
                talkPanel.CurrentTalks =
                    MYXZGameDataManager.Instance.GetTaskById(taskIds[0]).TakeTaskTalk;  //给出此NPC可以被接受的第一个任务TakeTaskTalk
                talkPanel.CurrentTask = taskIds[0];
                talkPanel.IsTaskTalk = true;                                        //这是一个接取任务的对话
            }
            else                    //如果没有可接的任务和正在进行的任务，说明是一个平常对话
            {
                talkPanel.CurrentTalks = MYXZGameDataManager.Instance.GetNpcInfoById(ID).Talks;
                talkPanel.IsTaskTalk = false;
            }
            PushPanelSignal.Dispatch(UIPanelType.TalkPanel);                        //弹出对话面板
        }
    }
}