/*
 * FileName             : RegisterSkillCommand.cs
 * Author               : 
 * Creat Date           : 
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
    public class RegisterSkillCommand : Command
    {
        [Inject]
        public GameObject Go { get; set; }

        [Inject]
        public Dictionary<KeyCode, string> Input2SkillTreeId { get; set; }

        [Inject]
        public SkillService SkillService { get; set; }

        public override void Execute()
        {
            SkillService.AddSkillTree(Go, Input2SkillTreeId);
        }
    }
}