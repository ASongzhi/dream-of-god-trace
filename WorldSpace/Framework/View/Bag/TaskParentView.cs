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
    /// 管理着Task的View
    /// </summary>
    public class TaskParentView:View
    {
        public GameObject TaskGameObject;
        public RectTransform ListRectTransform;

        public Text TaskDescription;

        public void ShowTaskInfo(Task task)
        {
            TaskDescription.text = task.Description;
        }
    }
}
