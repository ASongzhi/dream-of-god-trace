/*
 * FileName             : TaskMediator.cs
 * Author               : zsz
 * Creat Date           : 2018.4.21
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
    public class TaskMediator : Mediator
    {
        [Inject]
        public TaskView TaskView { get; set; }

        [Inject]
        public RequestGetTaskSignal ReqGetTaskSignal { get; set; }


        public override void OnRegister()
        {
            TaskView.OnClickTaskSignal.AddListener(RequestGetTask);
        }

        public override void OnRemove()
        {
            TaskView.OnClickTaskSignal.RemoveListener(RequestGetTask);
        }

        private void RequestGetTask()
        {
            ReqGetTaskSignal.Dispatch();
        }
    }
}

