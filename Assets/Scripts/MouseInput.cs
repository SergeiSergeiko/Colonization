using UnityEngine;

public class MouseInput : MonoBehaviour
{
    private const int NumberButton = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(NumberButton))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.TryGetComponent(out Base @base))
                {
                    @base.StartSetFlag();
                }
            }
        }
    }
}