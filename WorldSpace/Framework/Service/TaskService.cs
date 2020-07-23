/*
 * FileName             : TaskService.cs
 * Author               : zsz
 * Creat Date           : 2018.5.3
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 任务的Service类
    /// </summary>
    public class TaskService
    {
        /// <summary>
        /// 请勿通过TaskService直接访问PlayerInfoModel
        /// </summary>
        [Inject]
        public PlayerInfoModel PlayerInfoModel { get; set; }

        [Inject]
        public GameInfoService GameInfoService { get; set; }
        /// <summary>
        /// 通过NPC的ID获取这个NPC上可以被接取的任务
        /// </summary>
        /// <param name="npcId">NPC的ID</param>
        /// <returns>可以被接取的任务</returns>
        public List<string> GetAcceptableTasksInNpc(string npcId)
        {
            List<string> acceptableTasks = new List<string>();
            foreach (string task in MYXZGameDataManager.Instance.GetNpcInfoById(npcId).TaskIds)
            {
                if (!PlayerInfoModel.FinishTaskIds.Contains(task) && !PlayerInfoModel.CurrentTaskIds.Contains(task))
                {
                    //TODO 如果有两个前置任务呢？
                    if (MYXZGameDataManager.Instance.GetTaskById(task).Predecessors.Length < 1 || PlayerInfoModel.FinishTaskIds.Contains(MYXZGameDataManager.Instance.GetTaskById(task).Predecessors[0]))//判断是否完成前置任务
                    {
                        if (PlayerInfoModel.Info.Level >= MYXZGameDataManager.Instance.GetTaskById(task).LevelRequirement)//判断是否达到要求等级
                        {
                            acceptableTasks.Add(task);
                        }
                    }
                }
            }
            return acceptableTasks;
        }

        /// <summary>
        /// 获得领取任务时获得的奖励
        /// </summary>
        /// <param name="id">任务的ID</param>
        private void GetRewardBeforeTask(string id)
        {
            Task task = MYXZGameDataManager.Instance.GetTaskById(id);
            foreach (KeyValuePair<string, int> kvp in task.TakeTaskItem)//获取物品奖励
            {
                for (int j = 0; j < kvp.Value; j++)
                {
                    GameInfoService.PlayerGetNewItem(kvp.Key);
                }
            }
            GameInfoService.PlayerGetMoney(task.TakeTaskGold, task.TakeTaskSilver, task.TakeTaskCopper);
            GameInfoService.PlayerGetExperience(task.TakeTaskExperience);
        }

        /// <summary>
        /// 玩家是否正在执行此NPC的任务，如果是，out玩家正在执行此NPC的任务
        /// </summary>
        /// <param name="npcId">NPC的ID</param>
        /// <param name="tasks">玩家正在执行的此NPC的任务</param>
        /// <returns>是否正在执行此NPC的任务</returns>
        public bool IsInTaskOfNpc(string npcId, out List<string> tasks)
        {
            tasks = (from task in MYXZGameDataManager.Instance.GetNpcInfoById(npcId).TaskIds
                     where PlayerInfoModel.CurrentTaskIds.Contains(task)
                     select task).ToList();
            return tasks.Count > 0;
        }

        /// <summary>
        /// 玩家获得了一个新任务
        /// </summary>
        /// <param name="id">任务的ID</param>
        public void PlayerGetNewTask(string id)
        {
            if (!PlayerInfoModel.CurrentTaskIds.Contains(id))       //如果玩家当前没有已接取此任务
            {
                PlayerInfoModel.CurrentTaskIds.Add(id);
                GetRewardBeforeTask(id);
            }
        }

        /// <summary>
        /// 检查是否有任务满足完成条件
        /// </summary>
        /// <param name="npcId">交互NPC的ID</param>
        public void CheckTaskFinsh(string npcId)
        {
            for (int i = 0; i < PlayerInfoModel.CurrentTaskIds.Count; i++)//遍历任务列表
            {
                string id = PlayerInfoModel.CurrentTaskIds[i];
                if (MYXZGameDataManager.Instance.GetTaskById(id).Deliverer.Equals(npcId) && !PlayerInfoModel.FinishTaskIds.Contains(id))//匹配任务列表中是否有对应的任务交付对象,且任务是否完成过
                {
                    Task task = MYXZGameDataManager.Instance.GetTaskById(id);   //完成的任务
                    task.Name = task.Name + "(已完成)";
                    PlayerInfoModel.FinishTaskIds.Add(id);
                    PlayerInfoModel.CurrentTaskIds.Remove(id);

                    foreach (KeyValuePair<string, int> kvp in task.FinishTaskItem)//获取物品奖励
                    {
                        for (int j = 0; j < kvp.Value; j++)
                        {
                            GameInfoService.PlayerGetNewItem(kvp.Key);
                        }
                    }
                    GameInfoService.PlayerGetMoney(task.FinishTaskGold, task.FinishTaskSilver, task.FinishTaskCopper);
                    GameInfoService.PlayerGetExperience(task.FinishTaskExperience);
                }
            }
        }

        /// <summary>
        /// 获取玩家所有已完成和正在执行的任务
        /// </summary>
        public List<string> GetPlayerAllTasks()
        {
            return PlayerInfoModel.CurrentTaskIds.Union(PlayerInfoModel.FinishTaskIds).ToList();
        }
    }
}
