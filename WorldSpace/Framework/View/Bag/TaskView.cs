/*
 * FileName             : TaskView.cs
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
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace MYXZ
{
    /// <summary>
    /// 每个Task都对应一个TaskView
    /// </summary>
    public class TaskView : View
    {
        public TaskParentView TaskParentView;
        [SerializeField]
        private Text mTaskName;

        private bool mHasInit = false;

        public Task Task;
        public Signal OnClickTaskSignal = new Signal();

        private void Init()
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                if (this.transform.GetChild(i).name.Equals("Name"))
                {
                    mTaskName = transform.GetChild(i).GetComponent<Text>();
                }
            }
            this.GetComponent<Button>().onClick.AddListener(delegate
            {
                TaskParentView.ShowTaskInfo(this.Task);
            });
            mHasInit = true;
        }

        public TaskView SetTask(string taskId)
        {
            if (!mHasInit)
            {
                Init();
            }
            Task = MYXZGameDataManager.Instance.GetTaskById(taskId) as Task;
            this.mTaskName.text = Task.Name;
            return this;
        }
    }
}
