using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Mainsweeper : MonoBehaviour
{
    [SerializeField] private Cell m_cellPrefab = null;
    [SerializeField] private GridLayoutGroup m_container = null;

    [SerializeField] public int m_row = 1;
    [SerializeField] public int m_colmns = 1;

    [SerializeField] private int m_mainCount = 0;

    private Cell[,] cells;

    void Start()
    {
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
            for (int i = 0; i < m_mainCount;)
            {
                var row = Random.Range(0, m_row);
                var colmns = Random.Range(0, m_colmns);

                if (cells[row, colmns].m_cellsState != CellState.Mine)
                {
                    cells[row, colmns].m_cellsState = CellState.Mine;
                    i++;
                }
            }
        }

        for (int i = 0; i < m_row; i++)
        {
            for (int j = 0; j < m_colmns; j++)
            {
                int count = 0;
                var cell = cells[i, j];

                if (cell.m_cellsState == CellState.Mine) continue;

                for (int r = i - 1; r <= i + 1; r++)
                {
                    for (int c = j - 1; c <= j + 1; c++)
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
                    }
                }

                cell.m_cellsState = (CellState)count;
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < m_row; i++)
        {
            for (int j = 0; j < m_colmns; j++)
            {
                cells[i, j].Destroy();

                if (cells[i, j].m_cellsState == CellState.None && cells[i, j].m_open == true)
                {
                    for (int r = i - 1; r <= i + 1; r++)
                    {
                        for (int c = j - 1; c <= j + 1; c++)
                        {
                            if (r < 0 || r >= m_row)
                            {
                                continue;
                            }
                            else if (c < 0 || c >= m_colmns)
                            {
                                continue;
                            }
                            cells[r, c].m_open = true;
                            cells[r, c].Destroy();
                        }
                    }
                }
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
