/*
 * FileName             : TalkPanelView.cs
 * Author               : yqs
 * Creat Date           : 2018.2.20
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections.Generic;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace MYXZ
{
    /// <summary>
    /// 交谈的Panel
    /// </summary>
    public class TalkPanelView : BasePanelView
    {
        /// <summary>
        /// 说话者的Image
        /// </summary>
        public Image HeadImage;

        public Sprite NpcSprite;
        public Sprite PlayerSprite;

        /// <summary>
        /// 当前此Panel上显示的文字
        /// </summary>
        [SerializeField]
        private Text mWordText;

        [HideInInspector]
        public Talk[] CurrentTalks;
        public string CurrentTask;

        /// <summary>
        /// 是否是一个接取任务的交谈
        /// </summary>
        public bool IsTaskTalk;

        /// <summary>
        /// 当前正在说的是CurrentTalks中的第几句
        /// </summary>
        private int mIndex;

        public Signal TalkFinishSignal = new Signal();

        public override void OnEnter()
        {
            base.OnEnter();
            mIndex = 0; //初始化
            ShowNextWord();
        }

        public void ShowNextWord()
        {
            if (mIndex >= CurrentTalks.Length)
            {
                TalkFinishSignal.Dispatch();
                return;
            }
            HeadImage.sprite = MYXZGameDataManager.Instance.GetNpcInfoById(CurrentTalks[mIndex].Speaker).Sprite;    //TODO 需要给主角也提供一个配置文件
            mWordText.text = CurrentTalks[mIndex].Words;
            mIndex++;
        }
    }
}