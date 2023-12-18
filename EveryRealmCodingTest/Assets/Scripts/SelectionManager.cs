using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private LayerMask _selectableLayer;
    [SerializeField] private Transform _selection;
    [SerializeField] private ObjectController _currentObject;

    private void Awake()
    {
        _selection = null;
    }
    private void Update()
    {
        HandeSelection();
    }

    private void HandeSelection()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        

        if (Physics.Raycast(ray, out var hit))
        {
            var selection = hit.transform;
            if ((_selectableLayer.value & (1 << selection.gameObject.layer)) != 0)
            {
                _selection = selection;

                ObjectController objectController = _selection.gameObject.GetComponent<ObjectController>();

                if(objectController != null && _currentObject != objectController)
                {
                    _currentObject = objectController;
                    _currentObject.isSelected = true;
                    _currentObject.DoActions();
                }
            }
        }
        else
        {
            if(_currentObject != null)
            {
                _currentObject.isSelected = false;
                _currentObject.StopActions();
                _currentObject = null;
            }
        }
    }
}