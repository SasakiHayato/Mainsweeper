using UnityEngine;
using UnityEngine.UI;

public enum CellState
{
    None = 0,
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,

    Mine = -1,
}

public class Cell : MonoBehaviour
{
    [SerializeField] private GameObject m_bottan;
    [SerializeField] private Text m_view = null;
    private CellState cellState = CellState.None;

    public CellState m_cellsState
    {
        get => cellState;
        set
        {
            cellState = value;
            OnCellStateChanged();
        }
    }

    private void OnValidate()
    {
        OnCellStateChanged();
    }

    private void OnCellStateChanged()
    {
        if (m_view == null) return;

        if (cellState == CellState.None)
        {
            m_view.text = "";
        }
        else if (cellState == CellState.Mine)
        {
            m_view.text = "M";
            m_view.color = Color.white;
        }
        else
        {
            m_view.text = ((int)cellState).ToString();
            m_view.color = Color.blue;
        }
    }


    public bool m_open = false;

    public void PointDown()
    {
        m_open = true;
    }

    public void Destroy()
    {
        if (m_open)
            Destroy(m_bottan);
    }
}
