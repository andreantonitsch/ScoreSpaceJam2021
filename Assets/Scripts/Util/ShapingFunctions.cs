/// Based on functions made by Inigo Quilez
/// https://www.iquilezles.org/www/articles/functions/functions.htm

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public static class ShapingFunctions
    {
        public static float Linear(float t, float k)
        {
            return t * k;
        }

        public static float Impulse(float t, float k)
        {
            float h = t * k;
            return h * Mathf.Exp(1.0f - h);
        }

        public static float SustImpulse(float t, float f, float k)
        {
            float h = Mathf.Max(t - f, 0.0f);
            return Mathf.Min(t * t / (f * f), 1 + (2.0f / f) * h * Mathf.Exp(-k * h));
        }

        public static float PolyImpulse(float t, float n, float k)
        {
            return (n / (n - 1.0f)) * Mathf.Pow((n - 1.0f) * k, 1.0f / n) * t / (1.0f + k * Mathf.Pow(t, n));
        }

        public static float CubicPulse(float t, float w, float c)
        {
            t = Mathf.Abs(t - c);
            if (t > w) return 0.0f;
            t /= w;
            return 1.0f - t * t * (3.0f - 2.0f * t);
        } 
        
        public static float ExpStep(float t, float k, float n)
        {
            return Mathf.Exp(-k * Mathf.Pow(t, n));
        }

        public static float Gain(float t, float k)
        {
            float a = 0.5f * Mathf.Pow(2.0f * ((t < 0.5f) ? t : 1.0f - t), k);
            return (t < 0.5f) ? a : 1.0f - a;
        }

        public static float Parabola(float t, float k)
        {
            float a = 0.5f * Mathf.Pow(2.0f * ((t < 0.5f) ? t : 1.0f - t), k);
            return (t < 0.5f) ? a : 1.0f - a;
        }
        
        public static float PowerCurve(float t, float a, float b)
        {
            float k = Mathf.Pow(a + b, a + b) / (Mathf.Pow(a, a) * Mathf.Pow(b, b));
            return k * Mathf.Pow(t, a) * Mathf.Pow(1.0f - t, b);
        }

        public static float SincCurve(float t, float k)
        {
            float a = Mathf.PI * ((k * t) - 1.0f);
            return Mathf.Sin(a) / a;
        }

    }
