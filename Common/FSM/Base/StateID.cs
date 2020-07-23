/*
 * FileName             : StateID.cs
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
    /// FSM里状态的ID
    /// </summary>
    public enum StateID
    {
        NullStateID = 0,
        WalkState,
        IdleState,
        ChatState,
        PatrolState,
        JumpState,
        RunState
    }
}