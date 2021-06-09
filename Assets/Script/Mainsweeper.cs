using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Mainsweeper : MonoBehaviour
{
    [SerializeField] private Cell m_cellPrefab = null;
    [SerializeField] private GridLayoutGroup m_container = null;
    [SerializeField] private GameManager manager = null;

    [SerializeField] public int m_row = 1;
    [SerializeField] public int m_colmns = 1;

    private int m_mainCount = 25;

    private Cell[,] cells;

    private int openCount = 0;
    private int timeCount = 0;
    public int maxCount = 0;
    
    void Start()
    {
        manager.isPlay = true;
        
        cells = new Cell[m_row, m_colmns];
        for (int i = 0; i < m_row; i++)
        {
            for (int j = 0; j < m_colmns; j++)
            {
                CreateCells(i, j, cells);
            }
        }

        if (cells.Length < m_mainCount) m_mainCount = cells.Length;
        if (cells.Length >= m_mainCount)
        {
            for (int count = 0; count < m_mainCount;)
            {
                CreateMain(count);
                count++;
            }
        }

        for (int i = 0; i < m_row; i++)
        {
            for (int j = 0; j < m_colmns; j++)
            {
                int countCell = 0;

                if (cells[i, j].m_cellsState == CellState.Mine) continue;
                CountAround(i, j, countCell);
            }
        }
    }

    void CreateCells(int x, int y, object[,] cells)
    {
        var cell = Instantiate(m_cellPrefab);
        var pearent = m_container.gameObject.transform;
        cell.transform.SetParent(pearent);

        cells[x, y] = cell;
    }

    void CreateMain(int count)
    {
        var row = Random.Range(0, m_row);
        var colmns = Random.Range(0, m_colmns);

        if (cells[row, colmns].m_cellsState != CellState.Mine)
        {
            cells[row, colmns].m_cellsState = CellState.Mine;
        }
        else
        {
            count--;
        }
    }

    void CountAround(int x, int y, int count)
    {
        for (int r = x - 1; r <= x + 1; r++)
        {
            for (int c = y - 1; c <= y + 1; c++)
            {
                if (r < 0 || r >= m_row)
                {
                    continue;
                }
                else if (c < 0 || c >= m_colmns)
                {
                    continue;
                }

                var tCell = cells[r, c];
                if (tCell.m_cellsState == CellState.Mine)
                {
                    count++;
                }

                cells[x, y].m_cellsState = (CellState)count;
            }
        }
    }

    void Update()
    {
        if (manager.isPlay != true) return;

        for (int i = 0; i < m_row; i++)
        {
            for (int j = 0; j < m_colmns; j++)
            {
                
                cells[i, j].DestroyButton();

                if (cells[i, j].m_cellsState == CellState.None && cells[i, j].isOpen)
                {
                    OpenCells(i, j);
                }

                if (cells[i, j].m_cellsState == CellState.Mine && cells[i, j].isOpen)
                {
                    manager.isPlay = false;
                }
            }
        }
    }

    void OpenCells(int x, int y)
    {
        for (int r = x - 1; r <= x + 1; r++)
        {
            for (int c = y - 1; c <= y + 1; c++)
            {
                if (r < 0 || r >= m_row)
                {
                    continue;
                }
                else if (c < 0 || c >= m_colmns)
                {
                    continue;
                }
                cells[r, c].isOpen = true;
                CellsCount();
                cells[r, c].DestroyButton();
            }
        }
    }
    int timeUp = 5;
    private void CellsCount()
    {
        foreach (var cell in cells)
        {
            if (cell.isOpen)
            {
                openCount++;
            }
        }
        //‚±‚±‚â‚é
        if (timeCount < openCount)
        {
            timeCount = openCount;
            if (timeCount == timeUp)
            {
                timeUp += 5; 
            }
        }

        if (maxCount < openCount)
        {
            maxCount = openCount;
        }

        if (openCount == m_colmns * m_row - m_mainCount)
        {
            manager.isPlay = false;
        }
        openCount = 0;
    }

    private void OnValidate()
    {
        if (m_colmns < m_row)
        {
            m_container.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            m_container.constraintCount = m_colmns;
        }
        else
        {
            m_container.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            m_container.constraintCount = m_row;
        }
    }
}
