/*
 * FileName             : ItemMediator.cs
 * Author               : yqs
 * Creat Date           : 2018.3.18
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
    /// ItemView的Mediator
    /// </summary>
    public class ItemMediator : Mediator
    {
        [Inject]
        public ItemView ItemView { get; set; }

        public override void OnRegister()
        {
        }
    }
}