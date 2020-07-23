/*
 * FileName             : GameObjectPoolList.cs
 * Author               : yqs
 * Creat Date           : 2018.2.13
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
    /// <summary>
    /// 被序列化的类，继承自ScriptableObject
    /// </summary>
    public class GameObjectPoolList : ScriptableObject
    {
        public List<GameObjectPoolModel> PoolList;
    }
}