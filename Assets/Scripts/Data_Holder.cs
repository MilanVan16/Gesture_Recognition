using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Data_Holder : MonoBehaviour
{
    #region Keeping track of data
    [Header("Holding data")]
    [SerializeField]
    private int _mouseButton;
    [SerializeField]
    private int _targetCount;

    private bool _canStartComputingData;

    private List<Vector2> _coordinatesMouseInput = new List<Vector2>();
    private List<Vector2> _normalizedSquaredVectors = new List<Vector2>();
    #endregion
    void Start()
    {
        
    }


    void Update()
    {
        ClearDataOnNewInput();
        CheckNewInputToHoldData();
        StartComputingDataOnExitingInput();

        if(_canStartComputingData)
        {
            ResampleCoordinates();
            NormalizeSquareCoordinates();
        }
    }

    private void ClearDataOnNewInput()
    {
        if (Input.GetMouseButtonDown(_mouseButton))
        {
            _canStartComputingData = false;

            _coordinatesMouseInput.Clear();
        }
    }

    private void CheckNewInputToHoldData()
    {
        if(Input.GetMouseButton(_mouseButton))
        {
            StoreCoordinationsOfMousePosition();
        }
    }
    private void StartComputingDataOnExitingInput()
    {
        if(Input.GetMouseButtonUp(_mouseButton))
        {
            _canStartComputingData = true;
        }
    }

    private void StoreCoordinationsOfMousePosition()
    {
        Vector2 mousePosition = Input.mousePosition;
        _coordinatesMouseInput.Add(mousePosition);
    }

    private void ResampleCoordinates()
    {
        float completeLength = 0;

        foreach(Vector2 coordinate in _coordinatesMouseInput)
        {
            completeLength += coordinate.magnitude;
        }

        float interval = completeLength / _targetCount -1;

        float currentdistance = 0;
    }
    private void NormalizeSquareCoordinates()
    {
        _normalizedSquaredVectors.Clear();

        float top = 0;
        float bottom = 0;

        float left = 0;
        float right = 0;

        foreach (Vector2 coordinate in _coordinatesMouseInput)
        {
            top = Math.Max(top, coordinate.y);
            bottom = Mathf.Min(bottom, coordinate.y);

            right = Math.Max(right, coordinate.x);
            left = Mathf.Min(left, coordinate.x);
        }

        float height = top - bottom;
        float width = right - left;

        float longestSide = Mathf.Max(height, width);

        foreach (Vector2 coordinate in _coordinatesMouseInput)
        {
            float normalizedSquaredX = (coordinate.x - left) /longestSide;
            float normalizedSquaredY = (coordinate.y - bottom) / longestSide;

            _normalizedSquaredVectors.Add(new Vector2(normalizedSquaredX, normalizedSquaredY));
        }
    }

}
