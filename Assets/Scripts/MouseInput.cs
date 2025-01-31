using System;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    private const int NumberLeftMouseButton = 0;

    public event Action<RaycastHit> LeftMouseButtonClicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(NumberLeftMouseButton))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                LeftMouseButtonClicked?.Invoke(hit);
            }
        }
    }
}