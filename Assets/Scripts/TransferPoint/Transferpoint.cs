using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferPoint : MonoBehaviour
{

    [SerializeField] private Vector3[] points;

    public Vector3[] Points => points;
    public Vector3 CurrentPosition => _currentPosition;

    private Vector3 _currentPosition;
    private bool _gameStarted;

   
    void Start()
    {
        _gameStarted = true;
        _currentPosition = transform.position;
    }

    public Vector3 GetTransferPointPosition(int index)
    {
        return CurrentPosition + Points[index];
    }

    private void OnDrawGizmos() //update gibi calisir.istenilen kordinatlari ayarlanilacak.
    {
        if (_gameStarted == false && transform.hasChanged) //kordinatlarin birlikte hareket etmesi
        {
            _currentPosition = transform.position;
        }

        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(center: points[i] + _currentPosition, radius:0.5f) ;

            if (i < points.Length - 1)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(points[i] + _currentPosition, points[i + 1] + _currentPosition);
            }
        }
    }
}
