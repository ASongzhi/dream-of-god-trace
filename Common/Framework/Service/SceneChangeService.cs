/*
 * FileName             : SceneChangeService.cs
 * Author               : yqs
 * Creat Date           : 2018.1.29
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using strange.extensions.signal.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 场景切换的Service
    /// </summary>
    public class SceneChangeService
    {
        /// <summary>
        /// 此Sevice的所有任务都被执行完毕时触发
        /// </summary>
        public Signal<bool> ActionsFinishSignal = new Signal<bool>();

        public void SceneChange()
        {
            if (SaveInfo())
            {
                if (LoadNextSceneInfo())
                {
                    ActionsFinishSignal.Dispatch(true);
                    return;
                }
            }
            ActionsFinishSignal.Dispatch(false);
        }

        /// <summary>
        /// 保存当前场景的信息
        /// </summary>
        /// <returns>成功返回true</returns>
        private bool SaveInfo()
        {
            Debug.Log("SaveInfo");
            return true;
        }

        /// <summary>
        /// 加载下一场景的信息
        /// </summary>
        /// <returns></returns>
        private bool LoadNextSceneInfo()
        {
            Debug.Log("LoadNextSceneInfo");
            return true;
        }
    }
}