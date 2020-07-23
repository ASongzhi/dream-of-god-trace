/*
 * FileName             : UIPanelType.cs
 * Author               : yqs
 * Creat Date           : 2017.12.26
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
namespace MYXZ
{
    /// <summary>
    /// 所有UIPanel的Type
    /// </summary>
    public enum UIPanelType
    {
        Default = 0,

        /// <summary>
        /// 打开游戏时的选项面板
        /// </summary>
        GameStartMenuPanel,

        /// <summary>
        /// 消息面板
        /// </summary>
        MessageBoxPanel,

        /// <summary>
        /// 游戏信息面板
        /// </summary>
        GameInfoPanel,

        /// <summary>
        /// 游戏存档面板
        /// </summary>
        GameSavePanel,

        /// <summary>
        /// 加载新场景的时候的读条Panel
        /// </summary>
        LoadingScenePanel,

        /// <summary>
        /// 按下ESC键时的设置弹框
        /// </summary>
        SmallSettingBoxPanel,

        /// <summary>
        /// 游戏场景下的背景场景
        /// </summary>
        WorldSpaceBackGroundPanel,

        /// <summary>
        /// 背包
        /// </summary>
        BagPanel,

        /// <summary>
        /// 交谈
        /// </summary>
        TalkPanel,
    }
}