using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ShuShan;

namespace ShuShan
{
    /// <summary>
    /// ����״̬������ί��
    /// </summary>
    /// <param name="nextState">�¸�״̬</param>
    /// <returns></returns>
    public delegate bool StateCheckCondition(out string nextState);


    /// <summary>
    /// ״̬����
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
        /// ��ʼ��
        /// </summary>
        abstract public void Init();
        /// <summary>
        /// ����״̬��Ĵ���
        /// </summary>
        abstract public void Enter();
        /// <summary>
        /// ״̬������ͼ����Ⱦ
        /// </summary>
        public virtual void Render() { }
        /// <summary>
        /// ״̬���߼�����
        /// </summary>
        abstract public  void Step();
        /// <summary>
        /// ״̬�˳��Ĵ���
        /// </summary>
        abstract public void End();
    }
    /// <summary>
    /// ״̬�����࣬һ������mgr���
    /// </summary>
    public abstract class StateMacine
    {
        public Dictionary<string, State> m_States = new();

        protected State _CurState;

        /// <summary>
        /// �Ե�ǰ״̬�����߼����㣬���ж���ת
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
        /// �Ե�ǰ״̬������������Ⱦ����
        /// </summary>
        public void Render()
        {
            if (_CurState == null) return;
            _CurState.Render();
        }
    }
}