using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static Action<ObjectController> OnSelectedObject;

    [SerializeField] private LayerMask _selectableLayer;
    private Transform _selection;
     private ObjectController _currentObject;

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

                    OnSelectedObject?.Invoke(_currentObject);
                    _currentObject.HandleSelection();
                }
            }
        }
        else
        {
            if(_currentObject != null)
            {
                _currentObject.HandleDeselection();
                _currentObject = null;
            }
        }
    }
}