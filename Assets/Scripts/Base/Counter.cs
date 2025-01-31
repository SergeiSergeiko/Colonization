using System;

public class Counter
{
    private int _currentCount = 0;
    private int _targetNumber;

    public event Action CountReached;

    public Counter(int targetNumber)
    {
        _targetNumber = targetNumber;
    }

    public void Count()
    {
        _currentCount++;

        if (_currentCount >= _targetNumber)
        {
            CountReached?.Invoke();
            _currentCount = 0;
        }
    }

    public void SetTargetNumber(int targetNumber)
    {
        _targetNumber = targetNumber;
    }
}