﻿using System;
using UnityEngine;

namespace AI.Grid
{
    public abstract class Grid2DBase<T>
    {
        public delegate void IteratorAction(T arg1, int centerX, int centerY, int curX, int curY);

        protected int m_Row;
        protected int m_Column;
        protected float m_GridWidth;
        protected float m_GridLength;
        protected Vector3 m_GridOrig;

        public Grid2DBase(int row, int column, float gridWidth, float gridLength, Vector3 gridOrig)
        {
            m_Row = row;
            m_Column = column;
            m_GridWidth = gridWidth;
            m_GridLength = gridLength;
            m_GridOrig = gridOrig;
        }

        public void Set(int x, int y, T value)
        {
            if (IsCoordValid(x, y) == false)
            {
                return;
            }

            SetInternal(x, y, value);
        }

        public T Get(int x, int y)
        {
            if (IsCoordValid(x, y) == false)
            {
                return default(T);
            }

            return GetInternal(x, y);
        }

        public void Set(Vector3 pos, T value)
        {
            int x, y;
            if (PosToGridCoord(pos, out x, out y) == false)
            {
                return;
            }

            Set(x, y, value);
        }

        public T Get(Vector3 pos)
        {
            int x, y;
            if (PosToGridCoord(pos, out x, out y) == false)
            {
                return default(T);
            }

            return Get(x, y);
        }

        public bool PosToGridCoord(Vector3 pos, out int x, out int y)
        {
            float gridPosX = pos.x - m_GridOrig.x;
            float gridPosY = pos.z - m_GridOrig.z;
            x = (int) (gridPosX / m_GridWidth);
            y = (int) (gridPosY / m_GridLength);
            return IsCoordValid(x, y);
        }

        public bool GridCoordToPos(int x, int y, ref Vector3 pos)
        {
            if (IsCoordValid(x, y) == false)
            {
                return false;
            }

            pos.x = x * m_GridWidth + 0.5f * m_GridWidth + m_GridOrig.x;
            pos.z = y * m_GridWidth + 0.5f * m_GridLength + m_GridOrig.z;
            return true;
        }

        public bool IteratorGrid(Vector3 pos, int rangeX, int rangeY, IteratorAction action)
        {
            int centerX, centerY;
            if (PosToGridCoord(pos, out centerX, out centerY) == false)
            {
                return false;
            }

            for (int i = -rangeX; i <= rangeX; i++)
            {
                for (int j = -rangeY; j <= rangeY; j++)
                {
                    int curX = centerX + i;
                    int curY = centerY + j;
                    if (IsCoordValid(curX, curY) == false)
                    {
                        continue;
                    }

                    action.Invoke(Get(curX, curY), centerX, centerY, curX, curY);
                }
            }

            return true;
        }

        protected bool IsCoordValid(int x, int y)
        {
            return x >= 0 && x < m_Column && y >= 0 && y < m_Row;
        }

        public abstract void Clear();
        protected abstract T GetInternal(int x, int y);
        protected abstract void SetInternal(int x, int y, T value);
    }
}