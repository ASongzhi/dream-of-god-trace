/*
 * FileName             : Task.cs
 * Author               : ZSZ
 * Creat Date           : 2018.2.12
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MYXZ
{
    /// <summary>     
    /// 储存任务类
    /// </summary>
    [Serializable]
    public class Task
    {
        /// <summary>
        /// 任务的目标
        /// </summary>
        public class Target
        {
            /// <summary>
            /// 任务类型
            /// </summary>
            public TaskTargetType Type;
            /// <summary>
            /// 任务目标的ID
            /// </summary>
            public string TargetId;
            /// <summary>
            /// 需要对任务目标操作的次数
            /// </summary>
            public int Count;
            /// <summary>
            /// 任务所需要的等级
            /// </summary>
            public int Level;
        }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 任务ID
        /// </summary>
        public string Id;
        /// <summary>
        /// 任务发布者
        /// </summary>
        public string Publisher;
        /// <summary>
        /// 任务交付者
        /// </summary>
        public string Deliverer;

        /// <summary>
        /// 任务领取时奖励的经验
        /// </summary>
        public int TakeTaskExperience;

        /// <summary>
        /// 任务领取时的金币
        /// </summary>
        public int TakeTaskGold;

        /// <summary>
        /// 任务领取时的银币
        /// </summary>
        public int TakeTaskSilver;

        /// <summary>
        /// 任务领取时的铜币
        /// </summary>
        public int TakeTaskCopper;

        /// <summary>
        /// 任务领取时的物品
        /// </summary>
        public Dictionary<string, int> TakeTaskItem;

        /// <summary>
        /// 任务完成时的经验
        /// </summary>
        public int FinishTaskExperience;

        /// <summary>
        /// 任务完成时的金币
        /// </summary>
        public int FinishTaskGold;

        /// <summary>
        /// 任务完成时的银币
        /// </summary>
        public int FinishTaskSilver;

        /// <summary>
        /// 任务完成时的铜币
        /// </summary>
        public int FinishTaskCopper;

        /// <summary>
        /// 任务完成时的物品
        /// </summary>
        public Dictionary<string, int> FinishTaskItem;

        #region 接取任务的需求
        /// <summary>
        /// 前置任务
        /// </summary>
        public string[] Predecessors;
        /// <summary>
        /// 等级需求
        /// </summary>
        public int LevelRequirement;
        /// <summary>
        /// 需要持有的物品及个数
        /// </summary>
        public Dictionary<string, int> RequireItems;
        #endregion

        /// <summary>
        /// 任务目标
        /// </summary>
        public Target[] Targets;
        /// <summary>
        /// 任务描述
        /// </summary>
        public string Description;

        /// <summary>
        /// 接取任务时的对话
        /// </summary>
        public Talk[] TakeTaskTalk;

        /// <summary>
        /// 在此任务中时的对话
        /// </summary>
        public Talk[] InTaskTalk;

        /// <summary>
        /// 如果任务的交付者ID不是010000，即系统，会有此对话
        /// </summary>
        public Talk[] FinishTaskTalk;

        public Task()
        {
            Predecessors = new string[0];
            RequireItems = new Dictionary<string, int>();
            Targets = new Target[0];
            TakeTaskTalk = new Talk[0];
            InTaskTalk = new Talk[0];
            FinishTaskTalk = new Talk[0];
            TakeTaskItem = new Dictionary<string, int>();
            FinishTaskItem = new Dictionary<string, int>();
        }
    }
}