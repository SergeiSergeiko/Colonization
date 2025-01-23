using System;
using UnityEngine;

public class Builder : MonoBehaviour
{
    private Building _building;
    private Camera _camera;
    private bool _isMovingBuilding;
    private Vector3 _positionBuilding;

    public event Action<Building> BuildingPlaced;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_building != null)
            PlaceBuilding();
    }

    public Building Build(Building building, Vector3 position)
    {
        return Instantiate(building, position, Quaternion.identity);
    }

    public void StartPlacingNewBuilding(Building prefab)
    {
        CancelPlacing();

        _isMovingBuilding = false;
        _building = Instantiate(prefab);
    }

    public void StartReplacingBuilding(Building building)
    {
        CancelPlacing();

        _isMovingBuilding = true;
        _building = building;
        _positionBuilding = _building.transform.position;
    }

    private void PlaceBuilding()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            _building.transform.position = hit.point;

            if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform.TryGetComponent(out Plane _))
                {
                    BuildingPlaced?.Invoke(_building);
                    _building = null;
                }
            }
        }
    }

    private void CancelPlacing()
    {
        if (_building != null)
        {
            if (_isMovingBuilding)
            {
                _building.transform.position = _positionBuilding;
                BuildingPlaced.Invoke(_building);
            }
            else
            {
                Destroy(_building.gameObject);
                BuildingPlaced.Invoke(null);
            }
        }
    }
}