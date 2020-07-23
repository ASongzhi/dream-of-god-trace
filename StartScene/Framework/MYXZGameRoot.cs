/*
 * FileName             : MYXZGameRoot.cs
 * Author               : yqs
 * Creat Date           : 2017.12.10
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using strange.extensions.context.impl;

namespace MYXZ
{
    /// <summary>
    /// 游戏入口
    /// </summary>
    public class MYXZGameRoot : ContextView
    {
        void Awake()
        {
            this.context = new MYXZContext(this);
        }
    }
}