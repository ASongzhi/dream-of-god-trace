/*
 * FileName             : PlayerView.cs
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
    /// 玩家所见的View
    /// </summary>
    public class PlayerView : View
    {
        public BaseCharacter Character;

        public Signal EscSignal = new Signal();
        public Signal<KeyCode> UseSkillSignal = new Signal<KeyCode>();
        public UniStormWeatherSystem_C WeatherSystem;
        private float mTimer = 0.0f;
        [SerializeField]
        public Dictionary<KeyCode, string> SkillTreeID = new Dictionary<KeyCode, string>()
        {
            { KeyCode.Alpha1, "110001"}
        };

        public int[] CurrentSceneWeather;

        protected override void Start()
        {
            base.Start();
            Character.Init(this.gameObject);
            WeatherSystem.SetDate(12, 12, 2018);
            WeatherSystem.ChangeWeatherInstant(12);
        }

        void Update()
        {
            Character.Update();
            if (MYXZInputManager.Instance.GetKeyDown(KeyCode.Escape))
            {
                EscSignal.Dispatch();
            }
            mTimer += Time.deltaTime;
            if (mTimer > 60)    //测试使用，每60s随机变换一次天气
            {
                WeatherSystem.ChangeWeather(CurrentSceneWeather[Random.Range(0, CurrentSceneWeather.Length)]);
                mTimer = 0;
            }

            if (MYXZInputManager.Instance.GetKeyDown(KeyCode.Alpha1))
            {
                UseSkillSignal.Dispatch(KeyCode.Alpha1);
            }
        }

        void FixedUpdate()
        {
            this.Character.FixedUpdate();
        }
    }
}