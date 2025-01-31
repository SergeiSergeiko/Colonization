using System;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    private const int NumberLeftMouseButton = 0;

    public event Action<RaycastHit> LeftMouseButtonClicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(NumberLeftMouseButton))
            ClickLeftMouseButton();
    }

    private void ClickLeftMouseButton()
    {
        if (Input.GetMouseButtonDown(NumberLeftMouseButton))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                LeftMouseButtonClicked?.Invoke(hit);
            }
        }
    }
}