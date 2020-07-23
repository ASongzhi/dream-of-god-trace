/*
 * FileName             : TaskParentMediator.cs
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
using strange.extensions.mediation.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 管理TaskList的TaskParentView的Mediator
    /// </summary>
    public class TaskParentMediator : Mediator
    {
        [Inject]
        public TaskParentView TaskParentView { get; set; }

        [Inject]
        public RequestGetTaskSignal RequestGetTaskSignal { get; set; }

        [Inject]
        public ResponseGetTaskSignal ResponseGetTaskSignal { get; set; }

        public override void OnRegister()
        {
            ResponseGetTaskSignal.AddListener(GetTasks);
            RequestGetTaskSignal.Dispatch();
        }

        public override void OnRemove()
        {
            ResponseGetTaskSignal.RemoveListener(GetTasks);
            RequestGetTaskSignal.Dispatch();
        }

        public void GetTasks(List<string> taskIds)
        {
            for (int i = 0; i < TaskParentView.ListRectTransform.childCount; i++)  //关闭所有TaskGameObject
            {
                TaskParentView.ListRectTransform.GetChild(i).gameObject.SetActive(false);
            }
            for (int i = 0; i < taskIds.Count; i++)
            {
                if (i < TaskParentView.ListRectTransform.childCount)        //如果此TaskList下的已实例化出的TaskGO足够使用
                {
                    GameObject equipmentGO = TaskParentView.ListRectTransform.GetChild(i).gameObject;
                    equipmentGO.GetComponent<TaskView>().SetTask(taskIds[i]);  //将此TaskGameObject中的Task重新设置
                    equipmentGO.SetActive(true);
                }
                else
                {
                    GameObject taskGO = GameObject.Instantiate(TaskParentView.TaskGameObject,     //实例化出新的TaskGameObject
                       TaskParentView.ListRectTransform);

                    taskGO.AddComponent<TaskView>().SetTask(taskIds[i]).TaskParentView = this.TaskParentView;
                    taskGO.SetActive(true);
                }
               
            }
        }
    }
}
