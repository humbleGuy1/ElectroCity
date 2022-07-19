using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Building : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private int _points;
    [SerializeField] private bool _isNeutral;
    [SerializeField] private bool _isCapturedByEnemy;
    [SerializeField] private bool _isCapturedByPlayer;
    [SerializeField] private bool _isConnected;

    private int _connectionCounter;
    private readonly int _maxPoints = 100;

    public bool IsCapturedByPlayer => _isCapturedByPlayer;
    public bool IsConnected => _isConnected;

    public UnityAction<int> PointsChanged;
    public UnityAction ColorChanged;

    private void Start()
    {
        PointsChanged?.Invoke(_points);
    }

    public void TryCapture()
    {
        _isConnected = true;
        _connectionCounter++;

        if (_isNeutral || _isCapturedByEnemy)
        {
            StartCoroutine(Capturing());
        }
    }

    public void StopIncreasingPoints()
    {
        _isConnected = false;
    }

    private void ChangePoints(int value)
    {
        _points += value;
        _points = Mathf.Clamp(_points, 0, _maxPoints);
        PointsChanged?.Invoke(_points);
    }

    private IEnumerator Increasing()
    {
        while(_isConnected)
        {
            ChangePoints(1);
            yield return new WaitForSeconds(0.2f/ _connectionCounter);
        }
    }

    private IEnumerator Capturing()
    {
        while (_points > 0 && _isConnected)
        {
            ChangePoints(-1);
            yield return new WaitForSeconds(0.2f/ _connectionCounter);
        }

        ColorChanged?.Invoke();
        _isNeutral = false;
        _isCapturedByPlayer = true;
        StartCoroutine(Increasing());
    }
}
