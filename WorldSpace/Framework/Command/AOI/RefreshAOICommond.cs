/*
 * FileName             : AOICommond.cs
 * Author               : zsz
 * Creat Date           : 2018.10.6
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
    /// 更新AOI信息的Command， Bind From RefreshAOISignal
    /// </summary>
    public class RefreshAOICommond : Command
    {
        [Inject]
        public Transform Transform { get; set; }

        [Inject]
        public AOIService AOIService { get; set; }

        public override void Execute()
        {
            AOIService.RefreshAoi(Transform);
        }
    }
}
