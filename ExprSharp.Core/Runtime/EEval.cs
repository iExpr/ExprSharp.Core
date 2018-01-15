﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using iExpr.Evaluators;
using iExpr.Values;

namespace ExprSharp.Runtime
{
    public class EEContext : EvalContext
    {
        protected EEContext() { }

        public override EvalContext GetChild()
        {
            return new EEContext() { Evaluator = Evaluator, CancelToken = CancelToken, Parent = this };
        }

        protected override T GetValue<T>(ConcreteValue exp)
        {
            if (typeof(T).IsAssignableFrom(typeof(RealNumber))) {
                try
                {
                    double d = Convert.ToDouble(exp.Value);
                    return (T)(object)(new RealNumber(d));
                }
                catch { }
            }
            if(exp.Value is RealNumber)
            {
                try
                {
                    return (T)Convert.ChangeType((double)((RealNumber)exp.Value),typeof(T));
                }
                catch { }
            }
            return base.GetValue<T>(exp);
        }

        public new static EvalContext Create(CancellationTokenSource cancel)
        {
            var res = new EEContext
            {
                CancelToken = cancel
            };
            return res;
        }
    }

    public class EEval : iExpr.Evaluators.EvalEnvironment
    {
        public EEval()
        {
            base.Evaluator = new iExpr.Evaluators.ExprEvaluator();
        }

        public override EvalContext CreateContext(CancellationTokenSource cancel = null)
        {
            var res = EEContext.Create(cancel ?? new System.Threading.CancellationTokenSource());
            res.Evaluator = Evaluator;
            res.Variables = Variables;
            return res;
        }
    }
}