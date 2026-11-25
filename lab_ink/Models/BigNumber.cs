using System;
using System.Text;

public class BigNumber
{
    private int[] number;
    private const int Base = 1000;

    public int ArrayLength => number.Length;

    public BigNumber(string numberStr)
    {
        if (string.IsNullOrWhiteSpace(numberStr) || numberStr == "0")
        {
            number = new int[] { 0 };
            return;
        }

        numberStr = numberStr.TrimStart('0');
        if (numberStr.Length == 0)
            numberStr = "0";

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

    private BigNumber(int[] arr)
    {
        number = TrimLeadingZeros(arr);
    }

    public BigNumber Clone()
    {
        return new BigNumber((int[])number.Clone());
    }

    public override string ToString()
    {
        if (number.Length == 1 && number[0] == 0)
            return "0";

        StringBuilder sb = new StringBuilder();
        sb.Append(number[number.Length - 1]);

        for (int i = number.Length - 2; i >= 0; i--)
        {
            sb.Append(number[i].ToString("D3"));
        }

        return sb.ToString();
    }

    private int[] TrimLeadingZeros(int[] arr)
    {
        int i = arr.Length - 1;
        while (i > 0 && arr[i] == 0)
            i--;

        int[] result = new int[i + 1];
        Array.Copy(arr, 0, result, 0, i + 1);
        return result;
    }

    private int CompareTo(BigNumber other)
    {
        if (number.Length != other.number.Length)
            return number.Length.CompareTo(other.number.Length);

        for (int i = number.Length - 1; i >= 0; i--)
        {
            if (number[i] != other.number[i])
                return number[i].CompareTo(other.number[i]);
        }
        return 0;
    }

    private BigNumber Add(BigNumber bnum)
    {
        int maxLen = Math.Max(number.Length, bnum.number.Length);
        int[] result = new int[maxLen + 1];
        int carry = 0;

        for (int i = 0; i < maxLen; i++)
        {
            int a = i < number.Length ? number[i] : 0;
            int b = i < bnum.number.Length ? bnum.number[i] : 0;
            int sum = a + b + carry;

            result[i] = sum % Base;
            carry = sum / Base;
        }

        if (carry > 0)
            result[maxLen] = carry;

        return new BigNumber(result);
    }

    private BigNumber Substract(BigNumber bnum)
    {
        if (CompareTo(bnum) < 0)
            return new BigNumber("0");

        int[] result = new int[number.Length];
        int borrow = 0;

        for (int i = 0; i < number.Length; i++)
        {
            int a = number[i] - borrow;
            int b = i < bnum.number.Length ? bnum.number[i] : 0;

            if (a < b)
            {
                a += Base;
                borrow = 1;
            }
            else
            {
                borrow = 0;
            }

            result[i] = a - b;
        }

        return new BigNumber(result);
    }

    private BigNumber Multiply(double multiplier)
    {
        if (double.IsNaN(multiplier) ||
            double.IsInfinity(multiplier) ||
            multiplier <= 0)
        {
            return new BigNumber("0");
        }

        long mult = (long)Math.Round(
            multiplier * 1000,
            MidpointRounding.AwayFromZero
        );

        if (mult <= 0)
            return new BigNumber("0");

        int[] result = new int[number.Length + 10];
        long carry = 0;

        for (int i = 0; i < number.Length; i++)
        {
            long prod = (long)number[i] * mult + carry;

            result[i] = (int)(prod % Base);
            carry = prod / Base;
        }

        int pos = number.Length;

        while (carry > 0)
        {
            result[pos++] = (int)(carry % Base);
            carry /= Base;
        }

        BigNumber product = new BigNumber(result);

        return (product + new BigNumber("500")) / 1000.0;
    }

    private BigNumber Divide(double divisor)
    {
        long div = (long)Math.Round(divisor);
        if (div == 0) throw new DivideByZeroException();

        int[] result = new int[number.Length];
        long remainder = 0;

        for (int i = number.Length - 1; i >= 0; i--)
        {
            long current = remainder * Base + number[i];
            result[i] = (int)(current / div);
            remainder = current % div;
        }

        return new BigNumber(result);
    }

    public static BigNumber operator /(BigNumber a, double b) => a.Divide(b);

    public static BigNumber operator +(BigNumber a, BigNumber b) => a.Add(b);
    public static BigNumber operator -(BigNumber a, BigNumber b) => a.Substract(b);
    public static BigNumber operator *(BigNumber a, double b) => a.Multiply(b);

    public static bool operator >(BigNumber a, BigNumber b) => a.CompareTo(b) > 0;
    public static bool operator <(BigNumber a, BigNumber b) => a.CompareTo(b) < 0;
    public static bool operator >=(BigNumber a, BigNumber b) => a.CompareTo(b) >= 0;
    public static bool operator <=(BigNumber a, BigNumber b) => a.CompareTo(b) <= 0;
    public static bool operator ==(BigNumber a, BigNumber b) => a.CompareTo(b) == 0;
    public static bool operator !=(BigNumber a, BigNumber b) => a.CompareTo(b) != 0;

    public override bool Equals(object? obj) => obj is BigNumber other && this == other;
    public override int GetHashCode() => ToString().GetHashCode();
}