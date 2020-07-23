/*
 * FileName             : TeamMediator.cs
 * Author               : yqs
 * Creat Date           : 2018.9.17
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace MYXZ
{
    public class TeamMediator : Mediator
    {
        [Inject]
        public TeamView TeamView { get; set; }

        [Inject]
        public ResponseGetCharaInfoSignal ResGetCharaInfoSignal { get; set; }

        public override void OnRegister()
        {
            ResGetCharaInfoSignal.AddListener(GetCharaInfo);
        }

        public override void OnRemove()
        {
            ResGetCharaInfoSignal.RemoveListener(GetCharaInfo);
        }

        private void GetCharaInfo(BaseCharacter.Info characterInfo)
        {
            TeamView.HP.text = characterInfo.HP.ToString();
            TeamView.Level.text = characterInfo.Level.ToString();
            //TeamView.MagicAttack.text = characterInfo.MagicAttack.ToString();
            //TeamView.MagicDefense.text = characterInfo.MagicDefense.ToString();
        }
    }
}