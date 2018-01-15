﻿using iExpr;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExprSharp
{
    public interface IAdditive
    {
        object Add(object right);
    }
    public interface ISubtractive
    {
        object Subtract(object right);
    }
    public interface IMultiplicable
    {
        object Multiply(object right);
    }
    public interface IDivisible 
    {
        object Divide(object right);
    }
    public interface IMouldable
    {
        object Mod(object right);
    }
    public interface IPowerable
    {
        object Pow(object right);
    }
}
