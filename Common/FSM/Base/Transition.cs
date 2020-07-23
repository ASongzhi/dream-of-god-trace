/*
 * FileName             : Transition.cs
 * Author               : yqs
 * Creat Date           : 2018.1.28
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
namespace MYXZ
{
    /// <summary>
    /// FSM状态机的转化条件
    /// </summary>
    public enum Transition
    {
        NullTransition = 0,
        ReadytoWalk,
        ReadytoIdle,
        ReadytoChat,
        ReadytoPatrol,
        ReadytoJump,
        ReadytoRun
    }
}