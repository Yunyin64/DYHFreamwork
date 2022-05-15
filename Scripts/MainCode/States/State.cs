using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ShuShan;

namespace ShuShan
{
    /// <summary>
    /// 用于状态的条件委托
    /// </summary>
    /// <param name="nextState">下个状态</param>
    /// <returns></returns>
    public delegate bool StateCheckCondition(out string nextState);


    /// <summary>
    /// 状态基类
    /// </summary>
    abstract public class State
    {
        public event StateCheckCondition Conditions;

        public bool CheckConditions(out string nextState)
        {
            if (Conditions == null)
            {
                nextState = null;
                return false;
            }
            if (Conditions(out nextState)) return true;
            return false;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        abstract public void Init();
        /// <summary>
        /// 进入状态后的触发
        /// </summary>
        abstract public void Enter();
        /// <summary>
        /// 状态的物理、图形渲染
        /// </summary>
        public virtual void Render() { }
        /// <summary>
        /// 状态的逻辑更新
        /// </summary>
        abstract public  void Step();
        /// <summary>
        /// 状态退出的触发
        /// </summary>
        abstract public void End();
    }
    /// <summary>
    /// 状态机基类，一般用于mgr标记
    /// </summary>
    public abstract class StateMacine
    {
        public Dictionary<string, State> m_States = new();

        protected State _CurState;

        /// <summary>
        /// 对当前状态进行逻辑运算，并判定跳转
        /// </summary>
        public void Step()
        {
            if (_CurState == null) return;
            _CurState.Step();
            if (!_CurState.CheckConditions(out string nextState)) return;
            if (!m_States.TryGetValue(nextState,out State ToState)) return;
            _CurState.End();
            _CurState = ToState;
            _CurState.Enter();
        }

        /// <summary>
        /// 对当前状态进行物理与渲染运算
        /// </summary>
        public void Render()
        {
            if (_CurState == null) return;
            _CurState.Render();
        }
    }
}