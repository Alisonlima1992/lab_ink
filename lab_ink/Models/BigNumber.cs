using System;
using System.Text;

public class BigNumber
{
    private int[] number;
    private const int Base = 1000;

    public int ArrayLength => number.Length;

    public BigNumber(string numberStr)
    {
        if (string.IsNullOrEmpty(numberStr))
        {
            number = new int[] { 0 };
            return;
        }

        int len = (numberStr.Length + 2) / 3;
        number = new int[len];

        int index = 0;
        for (int i = numberStr.Length; i > 0; i -= 3)
        {
            int start = Math.Max(0, i - 3);
            string part = numberStr.Substring(start, i - start);
            number[index++] = int.Parse(part);
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = number.Length - 1; i >= 0; i--)
        {
            sb.Append(number[i]);
        }
        return sb.ToString();
    }

    public static BigNumber operator +(BigNumber a, BigNumber b)
    {
        return new BigNumber("0");
    }

    public static BigNumber operator -(BigNumber a, BigNumber b)
    {
        return new BigNumber("0");
    }

    public static BigNumber operator *(BigNumber a, double b)
    {
        return new BigNumber("0");
    }
}