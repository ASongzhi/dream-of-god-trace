/*
 * FileName             : WorldSpaceGameRoot.cs
 * Author               : yqs
 * Creat Date           : 2018.1.30
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using strange.extensions.context.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 世界场景的GameRoot，绑定在对应的物体上
    /// </summary>
    public class WorldSpaceGameRoot : ContextView
    {
        void Awake()
        {
            this.context = new WorldSpaceContext(this);
        }
    }
}
