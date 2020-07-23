/*
 * FileName             : UseItemView.cs
 * Author               : yqs
 * Creat Date           : 2018.3.27
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;
using strange.extensions.signal.impl;

namespace MYXZ
{
    /// <summary>
    /// 使用可以被使用的物品时弹出的Box的View
    /// </summary>
    public class UseItemView : View
    {
        public Button UseItemButton;
        public Button SetShortcutButton;
        [HideInInspector]
        public string CurrentItemId;

        public Signal UseItemSignal = new Signal();

        protected override void Start()
        {
            base.Start();
            UseItemButton.onClick.AddListener(delegate { UseItemSignal.Dispatch(); });
        }
    }
}