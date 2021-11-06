﻿using UnityEngine;

namespace Main
{
    public class Timer
    {
        protected float m_ExpiredTime = 0;

        public void Reset()
        {
            m_ExpiredTime = -1;
        }

        public void SetExpiredTime(float expiredTime)
        {
            m_ExpiredTime = expiredTime;
        }

        public bool IsExpired(float gameTime)
        {
            return m_ExpiredTime >= 0 && gameTime >= m_ExpiredTime;
        }

        public float GetRemainTime(float gameTime)
        {
            return Mathf.Max(0, m_ExpiredTime - gameTime);
        }

        public void Copy(Timer t)
        {
            m_ExpiredTime = t.m_ExpiredTime;
        }
    }
}