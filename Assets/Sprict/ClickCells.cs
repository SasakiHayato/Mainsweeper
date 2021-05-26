using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCells : MonoBehaviour
{
    [SerializeField] Mainsweeper mainsweeper;

    public void PointDown()
    {
        //mainsweeper = GetComponent<Mainsweeper>();
        var cell = new Cell[mainsweeper.m_row, mainsweeper.m_colmns];

        for (int i = 0; 0 < mainsweeper.m_row; i++)
        {
            for (int j = 0; j < mainsweeper.m_colmns; j++)
            {
                for (int r = i - 1; r < i + 1; r++)
                {
                    for (int c = j - 1; c < j + 1; c++)
                    {
                        var tCell = cell[r, c];
                        if (tCell.m_cellsState != CellState.Mine)
                        {
                            Destroy(this.gameObject);
                        }
                    }
                }
            }
        }
    }
}
