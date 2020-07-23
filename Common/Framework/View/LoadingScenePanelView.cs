/*
 * FileName             : LoadingScenePanelView.cs
 * Author               : yqs
 * Creat Date           : 2018.1.29
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MYXZ
{
    /// <summary>
    /// 加载中的UIPanel,显示加载进度
    /// </summary>
    public class LoadingScenePanelView : BasePanelView
    {
        /// <summary>
        /// 要加载的场景名字
        /// </summary>
        [NonSerialized] public string TargetSceneName;

        /// <summary>
        /// 进度条
        /// </summary>
        private Slider mSlider;

        public Signal LoadingFinish = new Signal();
        private AsyncOperation mLoadSceneAsync;

        public override void OnEnter()
        {
            base.OnEnter();
            mSlider = GetComponentInChildren<Slider>();
            StartCoroutine(LoadScene()); //开启协程异步加载场景
        }

        void Update()
        {
            mSlider.handleRect.transform.Rotate(new Vector3(0, 0, 3f), Space.Self);
        }

        IEnumerator LoadScene()
        {
            float showProgress = 0; //0~100
            mLoadSceneAsync = SceneManager.LoadSceneAsync(TargetSceneName);
            mLoadSceneAsync.allowSceneActivation = false; //关闭场景加载完成自动跳转

            while (mLoadSceneAsync.progress < 0.9f) //到0.9时场景加载已经完成
            {
                if (showProgress < mLoadSceneAsync.progress) //提供更好的视觉效果
                {
                    showProgress += 0.01f;
                    mSlider.value = showProgress;
                    yield return null;
                }
            }

            while (showProgress <= 1.0f)
            {
                showProgress += 0.01f;
                mSlider.value = showProgress;
                yield return null;
            }
            yield return new WaitForSeconds(0.2f);
            GC.Collect();               //场景切换时强制调用一次GC
            LoadingFinish.Dispatch();   //场景加载完成
        }

        public override void OnExit()
        {
            mLoadSceneAsync.allowSceneActivation = true; //场景加载完成并且完成当前场景信息存储及下一场景信息载入后再开启场景跳转
            Invoke("base.OnExit", 2.0f);
        }
    }
}