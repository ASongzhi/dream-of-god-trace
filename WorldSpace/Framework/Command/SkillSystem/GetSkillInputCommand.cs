/*
 * FileName             : GetSkillInputCommand.cs
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
using strange.extensions.command.impl;
using UnityEngine;

namespace MYXZ
{
    public class GetSkillInputCommand : Command
    {
        [Inject]
        public GameObject Go { get; set; }

        [Inject]
        public KeyCode Input { get; set; }

        [Inject]
        public SkillService SkillService { get; set; }

        public override void Execute()
        {
            SkillService.GetInput(Go, Input);
        }
    }
}