/*
 * FileName             : RegisterSkillSignal.cs
 * Author               : yqs
 * Creat Date           : 2018.10.28
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
    public class RegisterSkillSignal : Signal<GameObject, Dictionary<KeyCode, string>>
    {
    }
}