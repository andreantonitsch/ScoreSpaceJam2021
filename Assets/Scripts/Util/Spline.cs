using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Splines
{
    public abstract class CatMullSpline<T>
    {
        public List<T> knots;

        public int PointsPerSegment = 10;

        public float Alpha = 0.0f;

        public abstract T Add(T p1, T p2);

        public abstract T ScalarMult(float t, T p1);

        public abstract float ti(float t, T t1, T t2);

        public  T[] Chain()
        {
            int segments = knots.Count - 3;
            T[] ret = new T[(PointsPerSegment+1) * segments ];

            int count = 0;

            for (int i = 0; i < segments; i++)
            {
                
                float t0 = 0.0f;
                float t1 = ti(t0, knots[i], knots[i + 1]);
                float t2 = ti(t1, knots[i + 1], knots[i + 2]);
                float t3 = ti(t2, knots[i + 2], knots[i + 3]);

                for (float t = t1; t < t2; t+= (t2-t1 ) / PointsPerSegment)
                {
                    T A1 = Add(ScalarMult((t1 - t) / 
                                          (t1 - t0), knots[i]),
                               ScalarMult((t - t0) /
                                          (t1 - t0), knots[i+1]));

                    T A2 = Add(ScalarMult((t2 - t) /
                                          (t2 - t1), knots[i+1]),
                               ScalarMult((t - t1) /
                                          (t2 - t1), knots[i + 2]));

                    T A3 = Add(ScalarMult((t3 - t) / 
                                          (t3 - t2), knots[i+2]),
                               ScalarMult((t - t2) / 
                                          (t3 - t2), knots[i + 3]));


                    T B1 = Add(ScalarMult((t2 - t) / 
                                          (t2 - t0), A1),
                               ScalarMult((t - t0) / 
                                          (t2 - t0), A2));

                    T B2 = Add(ScalarMult((t3 - t) / 
                                          (t3 - t1), A2),
                               ScalarMult((t - t1) / 
                                          (t3 - t1), A3));


                    T C = Add(ScalarMult((t2 - t) / 
                                         (t2 - t1), B1),
                              ScalarMult((t - t1) / 
                                         (t2 - t1), B2));

                    ret[count] = C;
                    count++;
                }

            }     
           

            return ret;
        }

        public List<T> ListChain()
        {
            return new List<T>(Chain());
        }


    }

    public class CatMullSplineV2 : CatMullSpline<Vector2>
    {
        public override Vector2 Add(Vector2 p1, Vector2 p2)
        {
            return p1+p2;
        }

        public override Vector2 ScalarMult(float t, Vector2 p1)
        {
            return p1 * t;
        }

        public override float ti(float t, Vector2 t1, Vector2 t2)
        {
            Vector2 v = t2 - t1;

            float ti = Vector2.Dot(v, v);

            ti = Mathf.Pow(Mathf.Sqrt(ti), Alpha);

            return ti + t;

        }
    }

    public class CatMullSplineV3 : CatMullSpline<Vector3>
    {
        public override Vector3 Add(Vector3 p1, Vector3 p2)
        {
            return p1 + p2;
        }

        public override Vector3 ScalarMult(float t, Vector3 p1)
        {
            return p1 * t;
        }

        public override float ti(float t, Vector3 t1, Vector3 t2)
        {
            Vector3 v = t2 - t1;

            float ti = Vector3.Dot(v, v);

            ti = Mathf.Pow(Mathf.Sqrt(ti), Alpha);

            return ti + t;

        }
    }



}
