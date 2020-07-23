/*
 * FileName             : SceneInfo.cs
 * Author               : yqs
 * Creat Date           : 2018.2.24
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MYXZ
{
    public class SceneInfo
    {
        public string Name;
        public string Id;

        public string[] Npcs;

        public SceneInfo()
        {
            Npcs = new string[0];
        }
    }
}