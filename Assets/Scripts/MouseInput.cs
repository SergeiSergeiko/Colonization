using UnityEngine;

public class MouseInput : MonoBehaviour
{
    private const int LeftMouseButton = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
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