using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FragCheck : MonoBehaviour
{
    [SerializeField] Text m_object = null;
    private bool fragCheck;
    private void Start()
    {
        fragCheck = false;
        m_object.enabled = fragCheck;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            m_object.enabled = true;
        }
    }
}
