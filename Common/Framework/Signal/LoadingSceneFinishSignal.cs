/*
 * FileName             : LoadingSceneFinishSignal.cs
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
    /// 场景加载完成，Bind To LoadingSceneFinishCommand,传递下一场景的场景名
    /// </summary>
    public class LoadingSceneFinishSignal : Signal<string>
    {
    }
}