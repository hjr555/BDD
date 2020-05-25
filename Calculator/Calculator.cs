using System;

public class Calculator
{
    public int Result { get; set; }
    private int _firstNumber;
    private int _secondNumber;

    public void SetFirstNumber(int firstNumber)
    {
        _firstNumber = firstNumber;
    }

    public void SetSecondNumber(int secondNumber)
    {
        _secondNumber = secondNumber;
    }

    public void AddNumbers()
    {
        Result = _firstNumber + _secondNumber;
    }
}
