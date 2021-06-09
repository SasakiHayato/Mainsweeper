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

    Mine = 9,
}

public class Cell : MonoBehaviour
{
    [SerializeField] public GameObject m_bottan = null;
  
    [SerializeField] private Text m_view = null;
    [SerializeField] public Text frag = null;

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
            m_view.color = Color.black;
        }
        else
        {
            m_view.text = ((int)cellState).ToString();
            m_view.color = Color.blue;
        }
    }

    public bool isOpen = false;

    private void Start()
    {
        frag.gameObject.SetActive(false);
    }

    public void PointDown()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isOpen = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            frag.gameObject.SetActive(true);
        }
    }

    public void Open()
    {
        Destroy(m_bottan);
    }

    public void DestroyButton()
    {
        if (isOpen)
        {
            Destroy(m_bottan);
        }
    }
}
