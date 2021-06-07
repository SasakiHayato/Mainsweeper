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

    [SerializeField] private int m_mainCount = 0;

    private Cell[,] cells;

    void Start()
    {
        manager.isPlay = true;
        m_cellPrefab.frag.enabled = false;

        cells = new Cell[m_row, m_colmns];
        for (int i = 0; i < m_row; i++)
        {
            for (int j = 0; j < m_colmns; j++)
            {
                CreatCells(i, j, cells);
            }
        }

        if (cells.Length < m_mainCount) m_mainCount = cells.Length;
        if (cells.Length >= m_mainCount)
        {
            for (int count = 0; count < m_mainCount;)
            {
                CreateMain();
                count++;
            }
        }

        for (int i = 0; i < m_row; i++)
        {
            for (int j = 0; j < m_colmns; j++)
            {
                int countCell = 0;

                if (cells[i, j].m_cellsState == CellState.Mine) continue;
                CountCells(i, j, countCell);
            }
        }
    }

    void CreatCells(int x, int y, object[,] cells)
    {
        var cell = Instantiate(m_cellPrefab);
        var pearent = m_container.gameObject.transform;
        cell.transform.SetParent(pearent);

        cells[x, y] = cell;
    }

    void CreateMain()
    {
        var row = Random.Range(0, m_row);
        var colmns = Random.Range(0, m_colmns);

        if (cells[row, colmns].m_cellsState != CellState.Mine)
        {
            cells[row, colmns].m_cellsState = CellState.Mine;
        }
    }

    void CountCells(int x, int y, int count)
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

                if(cells[i, j].m_cellsState == CellState.Mine && cells[i, j].isOpen)
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
                cells[r, c].DestroyButton();
            }
        }
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
