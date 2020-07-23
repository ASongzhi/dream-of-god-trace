/*
 * FileName             : UIPanelInfo.cs
 * Author               : yqs
 * Creat Date           : 2017.12.26
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections.Generic;

namespace MYXZ
{
    /// <summary>
    /// 与UI的json中的数据一一对应,分别是AssetBundle的路径、Type和Name
    /// </summary>
    public class UIPanelInfo
    {
        public UIPanelType Type { get; set; }
        public string AssetBundlePath { get; set; }
        public string Name { get; set; }
    }
}