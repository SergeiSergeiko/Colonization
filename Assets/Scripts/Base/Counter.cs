using System;

public class Counter
{
    private int _currentCount = 0;
    private int _markNumer;

    public event Action CountReached;

    public Counter(int markNumber)
    {
        _markNumer = markNumber;
    }

    public void Count()
    {
        _currentCount++;

        if (_currentCount >= _markNumer)
        {
            CountReached?.Invoke();
            _currentCount = 0;
        }
    }

    public void SetMarkNumber(int number)
    {
        _markNumer = number;
    }
}