using System.Reflection.Metadata.Ecma335;
using System.Xml.Xsl;
using Serilog;

namespace FractionNameSpace;

public class Fraction
{
    // Properties:
    
    /// <summary>
    /// Целая часть
    /// </summary>
    public int Integer { get; set; }
    /// <summary>
    /// Числитель
    /// </summary>
    public int Numerator { get; set; }

    /// <summary>
    /// Знаменатель
    /// </summary>
    private int _denominator;
    public int Denominator
    {
        get { return _denominator; }
        set
        {
            if (Denominator < 0) // проверка на ноль
            {
                _denominator = 1;
                Log.Error(new DivideByZeroException().Message);
                throw new DivideByZeroException();
            }
            else
            {
                _denominator = value;
            }
        }
    }
    
    // Constructors:
    public Fraction() // Default-constructor
    {
        Integer = 0;
        Numerator = 0;
        Denominator = 1;
    }

    public Fraction(int integer)
    {
        Integer = integer;
        Numerator = 0;
        Denominator = 1;
    }

    public Fraction(int numerator, int denominator)
    {
        Integer = 0;
        Numerator = numerator;
        Denominator = denominator;
    }

    public Fraction(int integer, int numerator, int denominator)
    {
        Integer = integer;
        Numerator = numerator;
        Denominator = denominator;
    }
    
    // Overriding unary operators:
    public static Fraction operator +(Fraction fraction) => fraction;

    public static Fraction operator -(Fraction fraction) =>
        new Fraction(-fraction.Integer, fraction.Numerator, fraction.Denominator);
    
    // Overriding binary operators:
    public static Fraction operator +(Fraction f1, Fraction f2) =>
        new Fraction(f1.Numerator * f2.Denominator + f2.Numerator * f1.Denominator, f1.Denominator * f2.Denominator);

    public static Fraction operator -(Fraction f1, Fraction f2) =>
        f1 + (-f2);

    public static Fraction operator *(Fraction f1, Fraction f2) =>
        new Fraction(f1.Numerator * f2.Numerator, f1.Denominator * f2.Denominator).ToProper().Reduce();

    public static Fraction operator /(Fraction f1, Fraction f2)
    {
        if (f2.Numerator == 0)
        {
            Log.Error(new DivideByZeroException().Message);
        }
        return new Fraction(f1.Numerator * f2.Denominator, f1.Denominator * f2.Numerator);
    }
    public override bool Equals(Object obj)
    {
        return this.ToString() == obj.ToString();
    }

    public static bool operator ==(Fraction f1, Fraction f2)
    {
        return f1.Equals(f2);
    }

    public static bool operator !=(Fraction f1, Fraction f2)
    {
        return !(f1 == f2);
    }

    public static bool operator >(Fraction left, Fraction right)
    {
        return left.ToImproper().Numerator * right.Denominator >
               right.ToImproper().Numerator * left.Denominator;
    }
    public static bool operator <(Fraction left, Fraction right)
    {
        return left.ToImproper().Numerator * right.Denominator <
               right.ToImproper().Numerator * left.Denominator;
    }

    public static bool operator >=(Fraction left, Fraction right)
    {
        return !(left < right);
    }

    public static bool operator <=(Fraction left, Fraction right)
    {
        return !(left > right);
    }
    
    // Overriding binary operators: (Actions with Fraction and natural number)
    public static Fraction operator +(Fraction fraction, int number)
    {
        fraction.Integer += number;
        return fraction;
    }

    public static Fraction operator -(Fraction fraction, int number)
    {
        fraction.Integer -= number;
        return fraction;
    }

    public static Fraction operator *(Fraction fraction, int number)
    {
        fraction.Numerator *= number;
        return fraction;
    }

    public static Fraction operator /(Fraction fraction, int number)
    {
        if (number == 0)
        {
            Log.Error(new DivideByZeroException().Message);
            throw new DivideByZeroException();
        }
        else
        {
            if (fraction.Numerator % number == 0)
            {
                fraction.Numerator /= number;
            }
            else if (fraction.Numerator % number != 0)
            {
                fraction.Denominator *= number;
            }
            return fraction;
        }
    }
    
    
    // METHODS:
    
    public Fraction ToImproper() // перевод дроби в неправильную
    {
        Numerator += Integer * Denominator;
        Integer = 0;
        return this;
    }

    public Fraction ToProper() // перевод дроби в правильную
    {
        Integer += Numerator / Denominator;
        Numerator %= Denominator;
        return this;
    }

    public Fraction Invert() // Инвертирование
    {
        ToImproper();
        return new Fraction(Denominator, Numerator);
    }
    public Fraction Reduce() // Сокращение
    {
        if (Numerator == 0)
            return this;
        int more, less;
        int rest;
        if (Numerator > Denominator)
        {
            more = Numerator;
            less = Denominator;
        }
        else
        {
            more = Denominator;
            less = Numerator;
        }

        do
        {
            rest = more % less;
            more = less;
            less = rest;
        } while (rest > 0);

        int GCD = more;
        Numerator /= GCD;
        Denominator /= GCD;
        return this;
    }
    public override string ToString()
    {
        return $" {Integer} ({Numerator} / {Denominator})";
    }
}  