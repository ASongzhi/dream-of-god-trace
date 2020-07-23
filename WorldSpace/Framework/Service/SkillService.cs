/*
 * FileName             : SkillService.cs
 * Author               : yqs
 * Creat Date           : 2018.10.27
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
    /// 技能树的Service
    /// </summary>
    public class SkillService
    {
        [Inject]
        public AOIService AoiService { get; set; }

        private Dictionary<GameObject, List<SkillTree>> mGo2Skills = new Dictionary<GameObject, List<SkillTree>>(10);

        /// <summary>
        /// 添加一个物体的所有技能至SkillService中
        /// </summary>
        /// <param name="go"></param>
        /// <param name="input2SkillTreeId"></param>
        public void AddSkillTree(GameObject go, Dictionary<KeyCode, string> input2SkillTreeId)
        {
            List<SkillTree> skillTrees = new List<SkillTree>();
            foreach (KeyValuePair<KeyCode, string> keyValuePair in input2SkillTreeId)
            {
                SkillTree skillTree = new SkillTree(keyValuePair.Value)
                {
                    ShortCut = keyValuePair.Key
                };
                skillTrees.Add(skillTree);
            }
            mGo2Skills.Add(go, skillTrees);
        }

        public void GetInput(GameObject skillUser, KeyCode input)
        {
            SkillTree targetSkillTree = mGo2Skills[skillUser].Find(skillTree => skillTree.ShortCut == input);
            if (targetSkillTree != null)
            {
                targetSkillTree.Use(AoiService.GetEntity(skillUser.transform));
            }
        }
    }
}