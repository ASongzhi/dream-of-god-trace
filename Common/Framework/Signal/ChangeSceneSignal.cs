/*
 * FileName             : ChangeSceneSignal.cs
 * Author               : yqs
 * Creat Date           : 2018.1.31
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
    /// 跳转场景的Signal，传递要加载的场景的Name， Bind To ChangeSceneCommand
    /// </summary>
    public class ChangeSceneSignal : Signal<string>
    {
    }
}