/*
 * FileName             : PlayerMediator.cs
 * Author               : yqs
 * Creat Date           : 2018.1.31
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 玩家的Mediator
    /// </summary>
    public class PlayerMediator : Mediator
    {
        [Inject]
        public PlayerView PlayerView { get; set; }

        [Inject]
        public PushPanelSignal PushPanelSignal { get; set; }

        [Inject]
        public FinishTalkAndChooseSignal FinishTalkSignal { get; set; }

        [Inject]
        public ResponseGetCharaInfoSignal ResGetCharaInfoSignal { get; set; }

        [Inject]
        public ResponsePlayerTransformSignal ResGetPlayerTransformSignal { get; set; }
        
        [Inject]
        public RequestGetPlayerTransformSignal ReqGetPlayerTransformSignal { get; set; }

        [Inject]
        public ChangeSceneSignal ChangeSceneSignal { get; set; }

        [Inject]
        public RefreshAOISignal RefreshAoiSignal { get; set; }

        [Inject]
        public RegisterSkillSignal RegisterSkillSignal { get; set; }

        [Inject]
        public GetSkillInputSignal GetSkillInputSignal { get; set; }

        private int mTimer = -1;
        private Dictionary<int, bool> mCoroutineIndexs = new Dictionary<int, bool>();

        public override void OnRegister()
        {
            ResGetPlayerTransformSignal.AddListener(SetPlayerPosition);
            PlayerView.EscSignal.AddListener(KeyEscDown);
            PlayerView.UseSkillSignal.AddListener(UseSkill);
            FinishTalkSignal.AddListener(TalkFinish);
            ResGetCharaInfoSignal.AddListener(GetCharaInfo);
            RegisterSkillSignal.Dispatch(this.gameObject, PlayerView.SkillTreeID);

            ReqGetPlayerTransformSignal.Dispatch();
            mTimer = 0;
            PlayerView.Character.UseSkill = () => StartCoroutine(SkillUsing());
        }

        public override void OnRemove()
        {
            PlayerView.EscSignal.RemoveListener(KeyEscDown);
            PlayerView.UseSkillSignal.RemoveListener(UseSkill);
            FinishTalkSignal.RemoveListener(TalkFinish);
            ResGetCharaInfoSignal.RemoveListener(GetCharaInfo);
            ResGetPlayerTransformSignal.RemoveListener(SetPlayerPosition);

        }

        public IEnumerator SkillUsing()
        {
            float timer = 0.0f;
            this.PlayerView.Character.Rate = 0.0f;

            while (timer < this.PlayerView.Character.CurrentSkill.SkillTime)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            this.PlayerView.Character.Rate = 1.0f;
            this.PlayerView.Character.CurrentSkill.SkillExit();
            this.PlayerView.Character.SetCurrentSkill(null);
            this.mCoroutineIndexs.Remove(0);
        }

        void FixedUpdate()
        {
            if (mTimer >= 0)
            {
                if (mTimer % 4 == 0)
                {
                    RefreshAoiSignal.Dispatch(this.transform);
                }
                mTimer = (mTimer + 1) % 5;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("TeleportationCircle"))
            {
                ChangeSceneSignal.Dispatch("Scene02");
            }
        }

        private void KeyEscDown()
        {
            PushPanelSignal.Dispatch(UIPanelType.SmallSettingBoxPanel);
        }

        /// <summary>
        /// 对话结束
        /// </summary>
        private void TalkFinish()
        {
            this.PlayerView.Character.IsTalking = false;            //Player离开对话状态
            this.PlayerView.Character.TalkingNpc.IsTalking = false; //与Player对话的NPC离开对话状态
        }

        /// <summary>
        /// 使用物品
        /// </summary>
        /// <param name="info"></param>
        private void GetCharaInfo(BaseCharacter.Info info)
        {
            this.PlayerView.Character.CharaInfo = info;
        }

        /// <summary>
        /// 人物位置赋值
        /// </summary>
        private void SetPlayerPosition(SaveInfo.Transform transform)
        {
            PlayerView.transform.position = transform.Position;
            PlayerView.transform.rotation = Quaternion.Euler(transform.Rotation);
        }

        private void UseSkill(KeyCode input)
        {
            GetSkillInputSignal.Dispatch(this.gameObject, input);
        }
    }
}