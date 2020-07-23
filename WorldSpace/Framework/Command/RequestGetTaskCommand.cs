/*
 * FileName             : RequestGetTaskCommand.cs
 * Author               : zsz
 * Creat Date           : 2018.4.22
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
    /// 请求获取拥有的任务，Bind From RequestGetTaskSignal
    /// </summary>
    public class RequestGetTaskCommand : Command
    {
        [Inject]
        public ResponseGetTaskSignal ResponseGetTaskSignal { get; set; }

        [Inject]
        public TaskService TaskInfoService { get; set; }

        public override void Execute()
        {
            ResponseGetTaskSignal.Dispatch(TaskInfoService.GetPlayerAllTasks());
        }
    }
}
