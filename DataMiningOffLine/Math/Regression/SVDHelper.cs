using System;

namespace DataMiningOffLine
{
    class SVDHelper
    {
        const double minrealnumber = 1E-300;
        const double machineepsilon = 5E-16;
        const double maxrealnumber = 1E300;
        public static void GetMultipleRegressionСoefficients(double[,] input, double[,] output, out double[,] coefficients)
        {
            var inputTransposed = TransposeMatrix(input);
            var XTX = MultiplyMatrices(inputTransposed, input);
            var epsilon = new double[0];
            var u = new double[0, 0];
            var vt = new double[0, 0];
            Svd.rmatrixsvd(XTX, XTX.GetLength(0), XTX.GetLength(1), ref epsilon, ref u, ref vt);
            var epsilonPseudoInversed = new double[epsilon.Length, epsilon.Length];
            for (int i = 0; i < epsilon.Length; i++)
                epsilonPseudoInversed[i, i] = epsilon[i] < minrealnumber ? 0 : 1 / epsilon[i];
            double[] workTmp = new double[System.Math.Max(u.GetLength(0), u.GetLength(1))];
            Blas.inplacetranspose(ref u, 0, u.GetLength(0) - 1, 0, u.GetLength(1) - 1, ref workTmp);//transpose U-matrix in-place
            workTmp = new double[System.Math.Max(vt.GetLength(0), vt.GetLength(1))];
            Blas.inplacetranspose(ref vt, 0, vt.GetLength(0) - 1, 0, vt.GetLength(1) - 1, ref workTmp);//transpose VT-matrix in-place
            var XTXInversed = MultiplyMatrices(vt, epsilonPseudoInversed);//M+ = V * Epsilon+ * UT
            XTXInversed = MultiplyMatrices(XTXInversed, u);//M+ = V * Epsilon+ * UT
            var XTY = MultiplyMatrices(inputTransposed, output);
            coefficients = MultiplyMatrices(XTXInversed, XTY);
        }

        private static double[,] MultiplyMatrices(double[,] left, double[,] right)
        {
            if (left.GetLength(1) != right.GetLength(0))
                throw new Exception("Impossible operation");
            double[,] newMatrix = new double[left.GetLength(0), right.GetLength(1)];
            for (int row = 0; row < left.GetLength(0); row++)
            {
                for (int column = 0; column < right.GetLength(1); column++)
                {
                    double sum = 0;
                    for (int k = 0; k < left.GetLength(1); k++)
                        sum += left[row, k] * right[k, column];
                    newMatrix[row, column] = sum;
                }
            }
            return newMatrix;
        }

        private static double[,] TransposeMatrix(double[,] matrix)
        {
            var result = new double[matrix.GetLength(1), matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(1); i++)
                for (int j = 0; j < matrix.GetLength(0); j++)
                    result[i, j] = matrix[j, i];
            return result;
        }

        private class Svd
        {
            public static bool rmatrixsvd(double[,] a,
                int m,
                int n,
                ref double[] w,
                ref double[,] u,
                ref double[,] vt)
            {
                bool result = new bool();
                double[] tauq = new double[0];
                double[] taup = new double[0];
                double[] tau = new double[0];
                double[] e = new double[0];
                double[] work = new double[0];
                double[,] t2 = new double[0, 0];
                int minmn = 0;
                int ncu = 0;
                int nrvt = 0;
                int nru = 0;
                int ncvt = 0;
                a = (double[,])a.Clone();
                w = new double[0];
                u = new double[0, 0];
                vt = new double[0, 0];
                result = true;
                if (m == 0 || n == 0)
                {
                    return result;
                }
                minmn = System.Math.Min(m, n);
                w = new double[minmn + 1];
                ncu = 0;
                nru = 0;
                nru = m;
                ncu = m;
                u = new double[nru - 1 + 1, ncu - 1 + 1];
                nrvt = 0;
                ncvt = 0;
                nrvt = n;
                ncvt = n;
                vt = new double[nrvt - 1 + 1, ncvt - 1 + 1];
                Ortfac.rmatrixbd(ref a, m, n, ref tauq, ref taup);
                Ortfac.rmatrixbdunpackq(a, m, n, tauq, ncu, ref u);
                Ortfac.rmatrixbdunpackpt(a, m, n, taup, nrvt, ref vt);
                Ortfac.rmatrixbdunpackdiagonals(a, m, n, ref w, ref e);
                work = new double[m + 1];
                Blas.inplacetranspose(ref u, 0, nru - 1, 0, ncu - 1, ref work);
                result = Bdsvd.rmatrixbdsvd(ref w, e, minmn, ref a, 0, ref u, nru, ref vt, ncvt);
                Blas.inplacetranspose(ref u, 0, nru - 1, 0, ncu - 1, ref work);
                return result;
            }
        }
        private class Ablas
        {
            public const int blas2minvendorkernelsize = 8;
            public static void generatereflection(ref double[] x,
                int n,
                ref double tau)
            {
                int j = 0;
                double alpha = 0;
                double xnorm = 0;
                double v = 0;
                double beta = 0;
                double mx = 0;
                double s = 0;
                int i_ = 0;
                tau = 0;
                if (n <= 1)
                {
                    tau = 0;
                    return;
                }
                mx = 0;
                for (j = 1; j <= n; j++)
                {
                    mx = System.Math.Max(System.Math.Abs(x[j]), mx);
                }
                s = 1;
                if (mx != 0)
                {
                    if (mx <= minrealnumber / machineepsilon)
                    {
                        s = minrealnumber / machineepsilon;
                        v = 1 / s;
                        for (i_ = 1; i_ <= n; i_++)
                        {
                            x[i_] = v * x[i_];
                        }
                        mx = mx * v;
                    }
                    else
                    {
                        if (mx >= maxrealnumber * machineepsilon)
                        {
                            s = maxrealnumber * machineepsilon;
                            v = 1 / s;
                            for (i_ = 1; i_ <= n; i_++)
                            {
                                x[i_] = v * x[i_];
                            }
                            mx = mx * v;
                        }
                    }
                }
                alpha = x[1];
                xnorm = 0;
                if (mx != 0)
                {
                    for (j = 2; j <= n; j++)
                    {
                        xnorm = xnorm + System.Math.Pow(x[j] / mx, 2);
                    }
                    xnorm = System.Math.Sqrt(xnorm) * mx;
                }
                if (xnorm == 0)
                {
                    tau = 0;
                    x[1] = x[1] * s;
                    return;
                }
                mx = System.Math.Max(System.Math.Abs(alpha), System.Math.Abs(xnorm));
                beta = -(mx * System.Math.Sqrt(System.Math.Pow(alpha / mx, 2) + System.Math.Pow(xnorm / mx, 2)));
                if (alpha < 0)
                {
                    beta = -beta;
                }
                tau = (beta - alpha) / beta;
                v = 1 / (alpha - beta);
                for (i_ = 2; i_ <= n; i_++)
                {
                    x[i_] = v * x[i_];
                }
                x[1] = beta;
                x[1] = x[1] * s;
            }
            public static void applyreflectionfromtheleft(ref double[,] c,
                double tau,
                double[] v,
                int m1,
                int m2,
                int n1,
                int n2,
                ref double[] work)
            {
                if ((tau == 0 || n1 > n2) || m1 > m2)
                {
                    return;
                }
                if (work.Length < n2 - n1 + 1)
                    work = new double[n2 - n1 + 1];
                rmatrixgemv(n2 - n1 + 1, m2 - m1 + 1, 1.0, c, m1, n1, 1, v, 1, 0.0, work, 0);
                rmatrixger(m2 - m1 + 1, n2 - n1 + 1, c, m1, n1, -tau, v, 1, work, 0);
            }
            public static void rmatrixgemv(int m,
                int n,
                double alpha,
                double[,] a,
                int ia,
                int ja,
                int opa,
                double[] x,
                int ix,
                double beta,
                double[] y,
                int iy)
            {
                int i = 0;
                double v = 0;
                int i_ = 0;
                int i1_ = 0;
                if (m <= 0)
                {
                    return;
                }
                if (n <= 0 || alpha == 0.0)
                {
                    if (beta != 0)
                    {
                        for (i = 0; i <= m - 1; i++)
                        {
                            y[iy + i] = beta * y[iy + i];
                        }
                    }
                    else
                    {
                        for (i = 0; i <= m - 1; i++)
                        {
                            y[iy + i] = 0.0;
                        }
                    }
                    return;
                }
                if (opa == 0)
                {
                    for (i = 0; i <= m - 1; i++)
                    {
                        i1_ = (ix) - (ja);
                        v = 0.0;
                        for (i_ = ja; i_ <= ja + n - 1; i_++)
                        {
                            v += a[ia + i, i_] * x[i_ + i1_];
                        }
                        if (beta == 0.0)
                        {
                            y[iy + i] = alpha * v;
                        }
                        else
                        {
                            y[iy + i] = alpha * v + beta * y[iy + i];
                        }
                    }
                    return;
                }
                if (opa == 1)
                {
                    if (beta == 0.0)
                    {
                        for (i = 0; i <= m - 1; i++)
                        {
                            y[iy + i] = 0;
                        }
                    }
                    else
                    {
                        for (i = 0; i <= m - 1; i++)
                        {
                            y[iy + i] = beta * y[iy + i];
                        }
                    }
                    for (i = 0; i <= n - 1; i++)
                    {
                        v = alpha * x[ix + i];
                        i1_ = (ja) - (iy);
                        for (i_ = iy; i_ <= iy + m - 1; i_++)
                        {
                            y[i_] = y[i_] + v * a[ia + i, i_ + i1_];
                        }
                    }
                    return;
                }
            }
            public static void rmatrixger(int m,
                int n,
                double[,] a,
                int ia,
                int ja,
                double alpha,
                double[] u,
                int iu,
                double[] v,
                int iv)
            {
                int i = 0;
                double s = 0;
                int i_ = 0;
                int i1_ = 0;
                if (m <= 0 || n <= 0)
                {
                    return;
                }
                for (i = 0; i <= m - 1; i++)
                {
                    s = alpha * u[iu + i];
                    i1_ = (iv) - (ja);
                    for (i_ = ja; i_ <= ja + n - 1; i_++)
                    {
                        a[ia + i, i_] = a[ia + i, i_] + s * v[i_ + i1_];
                    }
                }
            }
            public static void applyreflectionfromtheright(ref double[,] c,
                double tau,
                double[] v,
                int m1,
                int m2,
                int n1,
                int n2,
                ref double[] work)
            {
                if ((tau == 0 || n1 > n2) || m1 > m2)
                {
                    return;
                }
                if (work.Length < m2 - m1 + 1)
                    work = new double[m2 - m1 + 1];
                rmatrixgemv(m2 - m1 + 1, n2 - n1 + 1, 1.0, c, m1, n1, 0, v, 1, 0.0, work, 0);
                rmatrixger(m2 - m1 + 1, n2 - n1 + 1, c, m1, n1, -tau, work, 0, v, 1);
            }
        }
        private class Bdsvd
        {
            public static bool rmatrixbdsvd(ref double[] d,
                double[] e,
                int n,
                ref double[,] u,
                int nru,
                ref double[,] c,
                int ncc,
                ref double[,] vt,
                int ncvt)
            {
                bool result = new bool();
                int i = 0;
                double[] d1 = new double[0];
                double[] e1 = new double[0];
                int i_ = 0;
                int i1_ = 0;
                e = (double[])e.Clone();
                result = false;
                d1 = new double[n + 1];
                i1_ = (0) - (1);
                for (i_ = 1; i_ <= n; i_++)
                {
                    d1[i_] = d[i_ + i1_];
                }
                if (n > 1)
                {
                    e1 = new double[n - 1 + 1];
                    i1_ = (0) - (1);
                    for (i_ = 1; i_ <= n - 1; i_++)
                    {
                        e1[i_] = e[i_ + i1_];
                    }
                }
                result = bidiagonalsvddecompositioninternal(d1, e1, n, u, c, 0, ncc, vt, 0, ncvt);
                i1_ = (1) - (0);
                for (i_ = 0; i_ <= n - 1; i_++)
                {
                    d[i_] = d1[i_ + i1_];
                }
                return result;
            }
            private static bool bidiagonalsvddecompositioninternal(double[] d,
                double[] e,
                int n,
                double[,] uu,
                double[,] c,
                int cstart,
                int ncc,
                double[,] vt,
                int vstart,
                int ncvt)
            {
                bool result = new bool();
                int i = 0;
                int idir = 0;
                int isub = 0;
                int iter = 0;
                int j = 0;
                int ll = 0;
                int lll = 0;
                int m = 0;
                int maxit = 0;
                int oldll = 0;
                int oldm = 0;
                double abse = 0;
                double abss = 0;
                double cosl = 0;
                double cosr = 0;
                double cs = 0;
                double eps = 0;
                double f = 0;
                double g = 0;
                double h = 0;
                double mu = 0;
                double oldcs = 0;
                double oldsn = 0;
                double r = 0;
                double shift = 0;
                double sigmn = 0;
                double sigmx = 0;
                double sinl = 0;
                double sinr = 0;
                double sll = 0;
                double smax = 0;
                double smin = 0;
                double sminl = 0;
                double sminoa = 0;
                double sn = 0;
                double thresh = 0;
                double tol = 0;
                double tolmul = 0;
                double unfl = 0;
                double[] work0 = new double[0];
                double[] work1 = new double[0];
                double[] work2 = new double[0];
                double[] work3 = new double[0];
                int maxitr = 0;
                bool matrixsplitflag = new bool();
                bool iterflag = new bool();
                double[] vttemp = new double[0];
                double[] ctemp = new double[0];
                double[] etemp = new double[0];
                double[,] ut = new double[0, 0];
                bool fwddir = new bool();
                double tmp = 0;
                int mm1 = 0;
                int mm0 = 0;
                bool bchangedir = new bool();
                int cend = 0;
                int vend = 0;
                int i_ = 0;
                e = (double[])e.Clone();
                result = true;
                if (n == 0)
                {
                    return result;
                }
                if (n == 1)
                {
                    if (d[1] < 0)
                    {
                        d[1] = -d[1];
                        if (ncvt > 0)
                        {
                            for (i_ = vstart; i_ <= vstart + ncvt - 1; i_++)
                            {
                                vt[vstart, i_] = -1 * vt[vstart, i_];
                            }
                        }
                    }
                    return result;
                }
                ll = 0;
                oldsn = 0;
                work0 = new double[n - 1 + 1];
                work1 = new double[n - 1 + 1];
                work2 = new double[n - 1 + 1];
                work3 = new double[n - 1 + 1];
                vend = vstart + System.Math.Max(ncvt - 1, 0);
                cend = cstart + System.Math.Max(ncc - 1, 0);
                vttemp = new double[vend + 1];
                ctemp = new double[cend + 1];
                maxitr = 12;
                fwddir = true;
                etemp = new double[n + 1];
                for (i = 1; i <= n - 1; i++)
                {
                    etemp[i] = e[i];
                }
                e = new double[n + 1];
                for (i = 1; i <= n - 1; i++)
                {
                    e[i] = etemp[i];
                }
                e[n] = 0;
                idir = 0;
                eps = machineepsilon;
                unfl = minrealnumber;
                tolmul = System.Math.Max(10, System.Math.Min(100, System.Math.Pow(eps, -0.125)));
                tol = tolmul * eps;
                smax = 0;
                for (i = 1; i <= n; i++)
                {
                    smax = System.Math.Max(smax, System.Math.Abs(d[i]));
                }
                for (i = 1; i <= n - 1; i++)
                {
                    smax = System.Math.Max(smax, System.Math.Abs(e[i]));
                }
                sminl = 0;
                sminoa = System.Math.Abs(d[1]);
                if (sminoa != 0)
                {
                    mu = sminoa;
                    for (i = 2; i <= n; i++)
                    {
                        mu = System.Math.Abs(d[i]) * (mu / (mu + System.Math.Abs(e[i - 1])));
                        sminoa = System.Math.Min(sminoa, mu);
                        if (sminoa == 0)
                        {
                            break;
                        }
                    }
                }
                sminoa = sminoa / System.Math.Sqrt(n);
                thresh = System.Math.Max(tol * sminoa, maxitr * n * n * unfl);
                maxit = maxitr * n * n;
                iter = 0;
                oldll = -1;
                oldm = -1;
                m = n;
                while (true)
                {
                    if (m <= 1)
                    {
                        break;
                    }
                    if (iter > maxit)
                    {
                        result = false;
                        return result;
                    }
                    smax = System.Math.Abs(d[m]);
                    smin = smax;
                    matrixsplitflag = false;
                    for (lll = 1; lll <= m - 1; lll++)
                    {
                        ll = m - lll;
                        abss = System.Math.Abs(d[ll]);
                        abse = System.Math.Abs(e[ll]);
                        if (abse <= thresh)
                        {
                            matrixsplitflag = true;
                            break;
                        }
                        smin = System.Math.Min(smin, abss);
                        smax = System.Math.Max(smax, System.Math.Max(abss, abse));
                    }
                    if (!matrixsplitflag)
                    {
                        ll = 0;
                    }
                    else
                    {
                        e[ll] = 0;
                        if (ll == m - 1)
                        {
                            m = m - 1;
                            continue;
                        }
                    }
                    ll = ll + 1;
                    if (ll == m - 1)
                    {
                        svdv2x2(d[m - 1], e[m - 1], d[m], ref sigmn, ref sigmx, ref sinr, ref cosr, ref sinl, ref cosl);
                        d[m - 1] = sigmx;
                        e[m - 1] = 0;
                        d[m] = sigmn;
                        if (ncvt > 0)
                        {
                            mm0 = m + (vstart - 1);
                            mm1 = m - 1 + (vstart - 1);
                            for (i_ = vstart; i_ <= vend; i_++)
                            {
                                vttemp[i_] = cosr * vt[mm1, i_];
                            }
                            for (i_ = vstart; i_ <= vend; i_++)
                            {
                                vttemp[i_] = vttemp[i_] + sinr * vt[mm0, i_];
                            }
                            for (i_ = vstart; i_ <= vend; i_++)
                            {
                                vt[mm0, i_] = cosr * vt[mm0, i_];
                            }
                            for (i_ = vstart; i_ <= vend; i_++)
                            {
                                vt[mm0, i_] = vt[mm0, i_] - sinr * vt[mm1, i_];
                            }
                            for (i_ = vstart; i_ <= vend; i_++)
                            {
                                vt[mm1, i_] = vttemp[i_];
                            }
                        }
                        if (ncc > 0)
                        {
                            mm0 = m + cstart - 1;
                            mm1 = m - 1 + cstart - 1;
                            for (i_ = cstart; i_ <= cend; i_++)
                            {
                                ctemp[i_] = cosl * c[mm1, i_];
                            }
                            for (i_ = cstart; i_ <= cend; i_++)
                            {
                                ctemp[i_] = ctemp[i_] + sinl * c[mm0, i_];
                            }
                            for (i_ = cstart; i_ <= cend; i_++)
                            {
                                c[mm0, i_] = cosl * c[mm0, i_];
                            }
                            for (i_ = cstart; i_ <= cend; i_++)
                            {
                                c[mm0, i_] = c[mm0, i_] - sinl * c[mm1, i_];
                            }
                            for (i_ = cstart; i_ <= cend; i_++)
                            {
                                c[mm1, i_] = ctemp[i_];
                            }
                        }
                        m = m - 2;
                        continue;
                    }
                    bchangedir = false;
                    if (idir == 1 && System.Math.Abs(d[ll]) < 1.0E-3 * System.Math.Abs(d[m]))
                    {
                        bchangedir = true;
                    }
                    if (idir == 2 && System.Math.Abs(d[m]) < 1.0E-3 * System.Math.Abs(d[ll]))
                    {
                        bchangedir = true;
                    }
                    if ((ll != oldll || m != oldm) || bchangedir)
                    {
                        if (System.Math.Abs(d[ll]) >= System.Math.Abs(d[m]))
                        {
                            idir = 1;
                        }
                        else
                        {
                            idir = 2;
                        }
                    }
                    if (idir == 1)
                    {
                        if (System.Math.Abs(e[m - 1]) <= System.Math.Abs(tol) * System.Math.Abs(d[m]))
                        {
                            e[m - 1] = 0;
                            continue;
                        }
                        mu = System.Math.Abs(d[ll]);
                        sminl = mu;
                        iterflag = false;
                        for (lll = ll; lll <= m - 1; lll++)
                        {
                            if (System.Math.Abs(e[lll]) <= tol * mu)
                            {
                                e[lll] = 0;
                                iterflag = true;
                                break;
                            }
                            mu = System.Math.Abs(d[lll + 1]) * (mu / (mu + System.Math.Abs(e[lll])));
                            sminl = System.Math.Min(sminl, mu);
                        }
                        if (iterflag)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (System.Math.Abs(e[ll]) <= System.Math.Abs(tol) * System.Math.Abs(d[ll]))
                        {
                            e[ll] = 0;
                            continue;
                        }
                        mu = System.Math.Abs(d[m]);
                        sminl = mu;
                        iterflag = false;
                        for (lll = m - 1; lll >= ll; lll--)
                        {
                            if (System.Math.Abs(e[lll]) <= tol * mu)
                            {
                                e[lll] = 0;
                                iterflag = true;
                                break;
                            }
                            mu = System.Math.Abs(d[lll]) * (mu / (mu + System.Math.Abs(e[lll])));
                            sminl = System.Math.Min(sminl, mu);
                        }
                        if (iterflag)
                        {
                            continue;
                        }
                    }
                    oldll = ll;
                    oldm = m;
                    if (n * tol * (sminl / smax) <= System.Math.Max(eps, 0.01 * tol))
                    {
                        shift = 0;
                    }
                    else
                    {
                        if (idir == 1)
                        {
                            sll = System.Math.Abs(d[ll]);
                            svd2x2(d[m - 1], e[m - 1], d[m], ref shift, ref r);
                        }
                        else
                        {
                            sll = System.Math.Abs(d[m]);
                            svd2x2(d[ll], e[ll], d[ll + 1], ref shift, ref r);
                        }
                        if (sll > 0)
                        {
                            if (System.Math.Pow(shift / sll, 2) < eps)
                            {
                                shift = 0;
                            }
                        }
                    }
                    iter = iter + m - ll;
                    if (shift == 0)
                    {
                        if (idir == 1)
                        {
                            cs = 1;
                            oldcs = 1;
                            for (i = ll; i <= m - 1; i++)
                            {
                                Rotations.generaterotation(d[i] * cs, e[i], ref cs, ref sn, ref r);
                                if (i > ll)
                                {
                                    e[i - 1] = oldsn * r;
                                }
                                Rotations.generaterotation(oldcs * r, d[i + 1] * sn, ref oldcs, ref oldsn, ref tmp);
                                d[i] = tmp;
                                work0[i - ll + 1] = cs;
                                work1[i - ll + 1] = sn;
                                work2[i - ll + 1] = oldcs;
                                work3[i - ll + 1] = oldsn;
                            }
                            h = d[m] * cs;
                            d[m] = h * oldcs;
                            e[m - 1] = h * oldsn;
                            if (ncvt > 0)
                            {
                                Rotations.applyrotationsfromtheleft(fwddir, ll + vstart - 1, m + vstart - 1, vstart, vend, work0, work1, vt, vttemp);
                            }
                            if (ncc > 0)
                            {
                                Rotations.applyrotationsfromtheleft(fwddir, ll + cstart - 1, m + cstart - 1, cstart, cend, work2, work3, c, ctemp);
                            }
                            if (System.Math.Abs(e[m - 1]) <= thresh)
                            {
                                e[m - 1] = 0;
                            }
                        }
                        else
                        {
                            cs = 1;
                            oldcs = 1;
                            for (i = m; i >= ll + 1; i--)
                            {
                                Rotations.generaterotation(d[i] * cs, e[i - 1], ref cs, ref sn, ref r);
                                if (i < m)
                                {
                                    e[i] = oldsn * r;
                                }
                                Rotations.generaterotation(oldcs * r, d[i - 1] * sn, ref oldcs, ref oldsn, ref tmp);
                                d[i] = tmp;
                                work0[i - ll] = cs;
                                work1[i - ll] = -sn;
                                work2[i - ll] = oldcs;
                                work3[i - ll] = -oldsn;
                            }
                            h = d[ll] * cs;
                            d[ll] = h * oldcs;
                            e[ll] = h * oldsn;
                            if (ncvt > 0)
                            {
                                Rotations.applyrotationsfromtheleft(!fwddir, ll + vstart - 1, m + vstart - 1, vstart, vend, work2, work3, vt, vttemp);
                            }
                            if (ncc > 0)
                            {
                                Rotations.applyrotationsfromtheleft(!fwddir, ll + cstart - 1, m + cstart - 1, cstart, cend, work0, work1, c, ctemp);
                            }
                            if (System.Math.Abs(e[ll]) <= thresh)
                            {
                                e[ll] = 0;
                            }
                        }
                    }
                    else
                    {
                        if (idir == 1)
                        {
                            f = (System.Math.Abs(d[ll]) - shift) * (extsignbdsqr(1, d[ll]) + shift / d[ll]);
                            g = e[ll];
                            for (i = ll; i <= m - 1; i++)
                            {
                                Rotations.generaterotation(f, g, ref cosr, ref sinr, ref r);
                                if (i > ll)
                                {
                                    e[i - 1] = r;
                                }
                                f = cosr * d[i] + sinr * e[i];
                                e[i] = cosr * e[i] - sinr * d[i];
                                g = sinr * d[i + 1];
                                d[i + 1] = cosr * d[i + 1];
                                Rotations.generaterotation(f, g, ref cosl, ref sinl, ref r);
                                d[i] = r;
                                f = cosl * e[i] + sinl * d[i + 1];
                                d[i + 1] = cosl * d[i + 1] - sinl * e[i];
                                if (i < m - 1)
                                {
                                    g = sinl * e[i + 1];
                                    e[i + 1] = cosl * e[i + 1];
                                }
                                work0[i - ll + 1] = cosr;
                                work1[i - ll + 1] = sinr;
                                work2[i - ll + 1] = cosl;
                                work3[i - ll + 1] = sinl;
                            }
                            e[m - 1] = f;
                            if (ncvt > 0)
                            {
                                Rotations.applyrotationsfromtheleft(fwddir, ll + vstart - 1, m + vstart - 1, vstart, vend, work0, work1, vt, vttemp);
                            }
                            if (ncc > 0)
                            {
                                Rotations.applyrotationsfromtheleft(fwddir, ll + cstart - 1, m + cstart - 1, cstart, cend, work2, work3, c, ctemp);
                            }
                            if (System.Math.Abs(e[m - 1]) <= thresh)
                            {
                                e[m - 1] = 0;
                            }
                        }
                        else
                        {
                            f = (System.Math.Abs(d[m]) - shift) * (extsignbdsqr(1, d[m]) + shift / d[m]);
                            g = e[m - 1];
                            for (i = m; i >= ll + 1; i--)
                            {
                                Rotations.generaterotation(f, g, ref cosr, ref sinr, ref r);
                                if (i < m)
                                {
                                    e[i] = r;
                                }
                                f = cosr * d[i] + sinr * e[i - 1];
                                e[i - 1] = cosr * e[i - 1] - sinr * d[i];
                                g = sinr * d[i - 1];
                                d[i - 1] = cosr * d[i - 1];
                                Rotations.generaterotation(f, g, ref cosl, ref sinl, ref r);
                                d[i] = r;
                                f = cosl * e[i - 1] + sinl * d[i - 1];
                                d[i - 1] = cosl * d[i - 1] - sinl * e[i - 1];
                                if (i > ll + 1)
                                {
                                    g = sinl * e[i - 2];
                                    e[i - 2] = cosl * e[i - 2];
                                }
                                work0[i - ll] = cosr;
                                work1[i - ll] = -sinr;
                                work2[i - ll] = cosl;
                                work3[i - ll] = -sinl;
                            }
                            e[ll] = f;
                            if (System.Math.Abs(e[ll]) <= thresh)
                            {
                                e[ll] = 0;
                            }
                            if (ncvt > 0)
                            {
                                Rotations.applyrotationsfromtheleft(!fwddir, ll + vstart - 1, m + vstart - 1, vstart, vend, work2, work3, vt, vttemp);
                            }
                            if (ncc > 0)
                            {
                                Rotations.applyrotationsfromtheleft(!fwddir, ll + cstart - 1, m + cstart - 1, cstart, cend, work0, work1, c, ctemp);
                            }
                        }
                    }
                    continue;
                }
                for (i = 1; i <= n; i++)
                {
                    if (d[i] < 0)
                    {
                        d[i] = -d[i];
                        if (ncvt > 0)
                        {
                            for (i_ = vstart; i_ <= vend; i_++)
                            {
                                vt[i + vstart - 1, i_] = -1 * vt[i + vstart - 1, i_];
                            }
                        }
                    }
                }
                for (i = 1; i <= n - 1; i++)
                {
                    isub = 1;
                    smin = d[1];
                    for (j = 2; j <= n + 1 - i; j++)
                    {
                        if (d[j] <= smin)
                        {
                            isub = j;
                            smin = d[j];
                        }
                    }
                    if (isub != n + 1 - i)
                    {
                        d[isub] = d[n + 1 - i];
                        d[n + 1 - i] = smin;
                        if (ncvt > 0)
                        {
                            j = n + 1 - i;
                            for (i_ = vstart; i_ <= vend; i_++)
                            {
                                vttemp[i_] = vt[isub + vstart - 1, i_];
                            }
                            for (i_ = vstart; i_ <= vend; i_++)
                            {
                                vt[isub + vstart - 1, i_] = vt[j + vstart - 1, i_];
                            }
                            for (i_ = vstart; i_ <= vend; i_++)
                            {
                                vt[j + vstart - 1, i_] = vttemp[i_];
                            }
                        }
                        if (ncc > 0)
                        {
                            j = n + 1 - i;
                            for (i_ = cstart; i_ <= cend; i_++)
                            {
                                ctemp[i_] = c[isub + cstart - 1, i_];
                            }
                            for (i_ = cstart; i_ <= cend; i_++)
                            {
                                c[isub + cstart - 1, i_] = c[j + cstart - 1, i_];
                            }
                            for (i_ = cstart; i_ <= cend; i_++)
                            {
                                c[j + cstart - 1, i_] = ctemp[i_];
                            }
                        }
                    }
                }
                return result;
            }
            private static void svdv2x2(double f,
               double g,
               double h,
               ref double ssmin,
               ref double ssmax,
               ref double snr,
               ref double csr,
               ref double snl,
               ref double csl)
            {
                bool gasmal = new bool();
                bool swp = new bool();
                int pmax = 0;
                double a = 0;
                double clt = 0;
                double crt = 0;
                double d = 0;
                double fa = 0;
                double ft = 0;
                double ga = 0;
                double gt = 0;
                double ha = 0;
                double ht = 0;
                double l = 0;
                double m = 0;
                double mm = 0;
                double r = 0;
                double s = 0;
                double slt = 0;
                double srt = 0;
                double t = 0;
                double temp = 0;
                double tsign = 0;
                double tt = 0;
                double v = 0;
                ssmin = 0;
                ssmax = 0;
                snr = 0;
                csr = 0;
                snl = 0;
                csl = 0;
                ft = f;
                fa = System.Math.Abs(ft);
                ht = h;
                ha = System.Math.Abs(h);
                clt = 0;
                crt = 0;
                slt = 0;
                srt = 0;
                tsign = 0;
                pmax = 1;
                swp = ha > fa;
                if (swp)
                {
                    pmax = 3;
                    temp = ft;
                    ft = ht;
                    ht = temp;
                    temp = fa;
                    fa = ha;
                    ha = temp;
                }
                gt = g;
                ga = System.Math.Abs(gt);
                if (ga == 0)
                {
                    ssmin = ha;
                    ssmax = fa;
                    clt = 1;
                    crt = 1;
                    slt = 0;
                    srt = 0;
                }
                else
                {
                    gasmal = true;
                    if (ga > fa)
                    {
                        pmax = 2;
                        if (fa / ga < machineepsilon)
                        {
                            gasmal = false;
                            ssmax = ga;
                            if (ha > 1)
                            {
                                v = ga / ha;
                                ssmin = fa / v;
                            }
                            else
                            {
                                v = fa / ga;
                                ssmin = v * ha;
                            }
                            clt = 1;
                            slt = ht / gt;
                            srt = 1;
                            crt = ft / gt;
                        }
                    }
                    if (gasmal)
                    {
                        d = fa - ha;
                        if (d == fa)
                        {
                            l = 1;
                        }
                        else
                        {
                            l = d / fa;
                        }
                        m = gt / ft;
                        t = 2 - l;
                        mm = m * m;
                        tt = t * t;
                        s = System.Math.Sqrt(tt + mm);
                        if (l == 0)
                        {
                            r = System.Math.Abs(m);
                        }
                        else
                        {
                            r = System.Math.Sqrt(l * l + mm);
                        }
                        a = 0.5 * (s + r);
                        ssmin = ha / a;
                        ssmax = fa * a;
                        if (mm == 0)
                        {
                            if (l == 0)
                            {
                                t = extsignbdsqr(2, ft) * extsignbdsqr(1, gt);
                            }
                            else
                            {
                                t = gt / extsignbdsqr(d, ft) + m / t;
                            }
                        }
                        else
                        {
                            t = (m / (s + t) + m / (r + l)) * (1 + a);
                        }
                        l = System.Math.Sqrt(t * t + 4);
                        crt = 2 / l;
                        srt = t / l;
                        clt = (crt + srt * m) / a;
                        v = ht / ft;
                        slt = v * srt / a;
                    }
                }
                if (swp)
                {
                    csl = srt;
                    snl = crt;
                    csr = slt;
                    snr = clt;
                }
                else
                {
                    csl = clt;
                    snl = slt;
                    csr = crt;
                    snr = srt;
                }
                if (pmax == 1)
                {
                    tsign = extsignbdsqr(1, csr) * extsignbdsqr(1, csl) * extsignbdsqr(1, f);
                }
                if (pmax == 2)
                {
                    tsign = extsignbdsqr(1, snr) * extsignbdsqr(1, csl) * extsignbdsqr(1, g);
                }
                if (pmax == 3)
                {
                    tsign = extsignbdsqr(1, snr) * extsignbdsqr(1, snl) * extsignbdsqr(1, h);
                }
                ssmax = extsignbdsqr(ssmax, tsign);
                ssmin = extsignbdsqr(ssmin, tsign * extsignbdsqr(1, f) * extsignbdsqr(1, h));
            }
            private static double extsignbdsqr(double a,
               double b)
            {
                double result = 0;
                if (b >= 0)
                {
                    result = System.Math.Abs(a);
                }
                else
                {
                    result = -System.Math.Abs(a);
                }
                return result;
            }
            private static void svd2x2(double f,
              double g,
              double h,
              ref double ssmin,
              ref double ssmax)
            {
                double aas = 0;
                double at = 0;
                double au = 0;
                double c = 0;
                double fa = 0;
                double fhmn = 0;
                double fhmx = 0;
                double ga = 0;
                double ha = 0;
                ssmin = 0;
                ssmax = 0;
                fa = System.Math.Abs(f);
                ga = System.Math.Abs(g);
                ha = System.Math.Abs(h);
                fhmn = System.Math.Min(fa, ha);
                fhmx = System.Math.Max(fa, ha);
                if (fhmn == 0)
                {
                    ssmin = 0;
                    if (fhmx == 0)
                    {
                        ssmax = ga;
                    }
                    else
                    {
                        ssmax = System.Math.Max(fhmx, ga) * System.Math.Sqrt(1 + System.Math.Pow(System.Math.Min(fhmx, ga) / System.Math.Max(fhmx, ga), 2));
                    }
                }
                else
                {
                    if (ga < fhmx)
                    {
                        aas = 1 + fhmn / fhmx;
                        at = (fhmx - fhmn) / fhmx;
                        au = System.Math.Pow(ga / fhmx, 2);
                        c = 2 / (System.Math.Sqrt(aas * aas + au) + System.Math.Sqrt(at * at + au));
                        ssmin = fhmn * c;
                        ssmax = fhmx / c;
                    }
                    else
                    {
                        au = fhmx / ga;
                        if (au == 0)
                        {
                            ssmin = fhmn * fhmx / ga;
                            ssmax = ga;
                        }
                        else
                        {
                            aas = 1 + fhmn / fhmx;
                            at = (fhmx - fhmn) / fhmx;
                            c = 1 / (System.Math.Sqrt(1 + System.Math.Pow(aas * au, 2)) + System.Math.Sqrt(1 + System.Math.Pow(at * au, 2)));
                            ssmin = fhmn * c * au;
                            ssmin = ssmin + ssmin;
                            ssmax = ga / (c + c);
                        }
                    }
                }
            }
        }
        private class Blas
        {
            public static void inplacetranspose(ref double[,] a,
              int i1,
              int i2,
              int j1,
              int j2,
              ref double[] work)
            {
                int j = 0;
                int ips = 0;
                int jps = 0;
                int l = 0;
                int i1_ = 0;
                if (i1 > i2 || j1 > j2)
                {
                    return;
                }
                if (!(i1 - i2 == j1 - j2))
                    throw new Exception("InplaceTranspose error: incorrect array size!");
                for (int i = i1; i <= i2 - 1; i++)
                {
                    j = j1 + i - i1;
                    ips = i + 1;
                    jps = j1 + ips - i1;
                    l = i2 - i;
                    i1_ = ips - 1;
                    for (int i_ = 1; i_ <= l; i_++)
                    {
                        work[i_] = a[i_ + i1_, j];
                    }
                    i1_ = jps - ips;
                    for (int i_ = ips; i_ <= i2; i_++)
                    {
                        a[i_, j] = a[i, i_ + i1_];
                    }
                    i1_ = 1 - jps;
                    for (int i_ = jps; i_ <= j2; i_++)
                    {
                        a[i, i_] = work[i_ + i1_];
                    }
                }
            }
        }
        private class Ortfac
        {
            public static void rmatrixbd(ref double[,] a,
                int m,
                int n,
                ref double[] tauq,
                ref double[] taup)
            {
                double[] work = new double[0];
                double[] t = new double[0];
                int maxmn = 0;
                int i = 0;
                double ltau = 0;
                int i_ = 0;
                int i1_ = 0;
                tauq = new double[0];
                taup = new double[0];
                if (n <= 0 || m <= 0)
                {
                    return;
                }
                maxmn = System.Math.Max(m, n);
                work = new double[maxmn + 1];
                t = new double[maxmn + 1];
                tauq = new double[n];
                taup = new double[n];
                for (i = 0; i <= n - 1; i++)
                {
                    tauq[i] = 0.0;
                    taup[i] = 0.0;
                }
                for (i = 0; i <= n - 1; i++)
                {
                    i1_ = (i) - (1);
                    for (i_ = 1; i_ <= m - i; i_++)
                    {
                        t[i_] = a[i_ + i1_, i];
                    }
                    Ablas.generatereflection(ref t, m - i, ref ltau);
                    tauq[i] = ltau;
                    i1_ = (1) - (i);
                    for (i_ = i; i_ <= m - 1; i_++)
                    {
                        a[i_, i] = t[i_ + i1_];
                    }
                    t[1] = 1;
                    Ablas.applyreflectionfromtheleft(ref a, ltau, t, i, m - 1, i + 1, n - 1, ref work);
                    if (i < n - 1)
                    {
                        i1_ = (i + 1) - (1);
                        for (i_ = 1; i_ <= n - i - 1; i_++)
                        {
                            t[i_] = a[i, i_ + i1_];
                        }
                        Ablas.generatereflection(ref t, n - 1 - i, ref ltau);
                        taup[i] = ltau;
                        i1_ = (1) - (i + 1);
                        for (i_ = i + 1; i_ <= n - 1; i_++)
                        {
                            a[i, i_] = t[i_ + i1_];
                        }
                        t[1] = 1;
                        Ablas.applyreflectionfromtheright(ref a, ltau, t, i + 1, m - 1, i + 1, n - 1, ref work);
                    }
                    else
                    {
                        taup[i] = 0;
                    }
                }
            }
            public static void rmatrixbdunpackpt(double[,] qp,
                int m,
                int n,
                double[] taup,
                int ptrows,
                ref double[,] pt)
            {
                int i = 0;
                int j = 0;
                pt = new double[0, 0];
                if (!(ptrows <= n))
                    throw new Exception("RMatrixBDUnpackPT: PTRows>N!");
                if (!(ptrows >= 0))
                    throw new Exception("RMatrixBDUnpackPT: PTRows<0!");
                if ((m == 0 || n == 0) || ptrows == 0)
                {
                    return;
                }
                pt = new double[ptrows, n];
                for (i = 0; i <= ptrows - 1; i++)
                {
                    for (j = 0; j <= n - 1; j++)
                    {
                        if (i == j)
                        {
                            pt[i, j] = 1;
                        }
                        else
                        {
                            pt[i, j] = 0;
                        }
                    }
                }
                rmatrixbdmultiplybyp(qp, m, n, taup, ref pt, ptrows, n, true, true);
            }
            public static void rmatrixbdmultiplybyp(double[,] qp,
                int m,
                int n,
                double[] taup,
                ref double[,] z,
                int zrows,
                int zcolumns,
                bool fromtheright,
                bool dotranspose)
            {
                int i = 0;
                double[] v = new double[0];
                double[] work = new double[0];
                double[] dummy = new double[0];
                int mx = 0;
                int i1 = 0;
                int i2 = 0;
                int istep = 0;
                int i_ = 0;
                int i1_ = 0;
                if (((m <= 0 || n <= 0) || zrows <= 0) || zcolumns <= 0)
                {
                    return;
                }
                if (!((fromtheright && zcolumns == n) || (!fromtheright && zrows == n)))
                    throw new Exception("RMatrixBDMultiplyByP: incorrect Z size!");
                mx = System.Math.Max(m, n);
                mx = System.Math.Max(mx, zrows);
                mx = System.Math.Max(mx, zcolumns);
                v = new double[mx + 1];
                work = new double[mx + 1];
                if (fromtheright)
                {
                    i1 = n - 2;
                    i2 = 0;
                    istep = -1;
                }
                else
                {
                    i1 = 0;
                    i2 = n - 2;
                    istep = 1;
                }
                if (!dotranspose)
                {
                    i = i1;
                    i1 = i2;
                    i2 = i;
                    istep = -istep;
                }
                if (n - 1 > 0)
                {
                    i = i1;
                    do
                    {
                        i1_ = (i + 1) - (1);
                        for (i_ = 1; i_ <= n - 1 - i; i_++)
                        {
                            v[i_] = qp[i, i_ + i1_];
                        }
                        v[1] = 1;
                        if (fromtheright)
                        {
                            Ablas.applyreflectionfromtheright(ref z, taup[i], v, 0, zrows - 1, i + 1, n - 1, ref work);
                        }
                        else
                        {
                            Ablas.applyreflectionfromtheleft(ref z, taup[i], v, i + 1, n - 1, 0, zcolumns - 1, ref work);
                        }
                        i = i + istep;
                    }
                    while (i != i2 + istep);
                }
            }
            public static void rmatrixbdunpackdiagonals(double[,] b,
                int m,
                int n,
                ref double[] d,
                ref double[] e)
            {
                int i = 0;
                d = new double[0];
                e = new double[0];
                if (m <= 0 || n <= 0)
                {
                    return;
                }
                d = new double[n];
                e = new double[n];
                for (i = 0; i <= n - 2; i++)
                {
                    d[i] = b[i, i];
                    e[i] = b[i, i + 1];
                }
                d[n - 1] = b[n - 1, n - 1];
            }
            public static void rmatrixbdmultiplybyq(double[,] qp,
                int m,
                int n,
                double[] tauq,
                ref double[,] z,
                int zrows,
                int zcolumns,
                bool fromtheright,
                bool dotranspose)
            {
                int i = 0;
                int i1 = 0;
                int i2 = 0;
                int istep = 0;
                double[] v = new double[0];
                double[] work = new double[0];
                double[] dummy = new double[0];
                int mx = 0;
                int i_ = 0;
                int i1_ = 0;
                if (((m <= 0 || n <= 0) || zrows <= 0) || zcolumns <= 0)
                {
                    return;
                }
                if (!((fromtheright && zcolumns == m) || (!fromtheright && zrows == m)))
                    throw new Exception("RMatrixBDMultiplyByQ: incorrect Z size!");
                mx = System.Math.Max(m, n);
                mx = System.Math.Max(mx, zrows);
                mx = System.Math.Max(mx, zcolumns);
                v = new double[mx + 1];
                work = new double[mx + 1];
                if (fromtheright)
                {
                    i1 = 0;
                    i2 = n - 1;
                    istep = 1;
                }
                else
                {
                    i1 = n - 1;
                    i2 = 0;
                    istep = -1;
                }
                if (dotranspose)
                {
                    i = i1;
                    i1 = i2;
                    i2 = i;
                    istep = -istep;
                }
                i = i1;
                do
                {
                    i1_ = (i) - (1);
                    for (i_ = 1; i_ <= m - i; i_++)
                    {
                        v[i_] = qp[i_ + i1_, i];
                    }
                    v[1] = 1;
                    if (fromtheright)
                    {
                        Ablas.applyreflectionfromtheright(ref z, tauq[i], v, 0, zrows - 1, i, m - 1, ref work);
                    }
                    else
                    {
                        Ablas.applyreflectionfromtheleft(ref z, tauq[i], v, i, m - 1, 0, zcolumns - 1, ref work);
                    }
                    i = i + istep;
                }
                while (i != i2 + istep);
            }
            public static void rmatrixbdunpackq(double[,] qp,
                int m,
                int n,
                double[] tauq,
                int qcolumns,
                ref double[,] q)
            {
                int i = 0;
                int j = 0;
                q = new double[0, 0];
                if (!(qcolumns <= m))
                    throw new Exception("RMatrixBDUnpackQ: QColumns>M!");
                if (!(qcolumns >= 0))
                    throw new Exception("RMatrixBDUnpackQ: QColumns<0!");
                if ((m == 0 || n == 0) || qcolumns == 0)
                {
                    return;
                }
                q = new double[m, qcolumns];
                for (i = 0; i <= m - 1; i++)
                {
                    for (j = 0; j <= qcolumns - 1; j++)
                    {
                        if (i == j)
                        {
                            q[i, j] = 1;
                        }
                        else
                        {
                            q[i, j] = 0;
                        }
                    }
                }
                rmatrixbdmultiplybyq(qp, m, n, tauq, ref q, m, qcolumns, false, false);
            }
        }
        private class Rotations
        {
            public static void generaterotation(double f,
                double g,
                ref double cs,
                ref double sn,
                ref double r)
            {
                double f1 = 0;
                double g1 = 0;
                cs = 0;
                sn = 0;
                r = 0;
                if (g == 0)
                {
                    cs = 1;
                    sn = 0;
                    r = f;
                }
                else
                {
                    if (f == 0)
                    {
                        cs = 0;
                        sn = 1;
                        r = g;
                    }
                    else
                    {
                        f1 = f;
                        g1 = g;
                        if (System.Math.Abs(f1) > System.Math.Abs(g1))
                        {
                            r = System.Math.Abs(f1) * System.Math.Sqrt(1 + System.Math.Pow(g1 / f1, 2));
                        }
                        else
                        {
                            r = System.Math.Abs(g1) * System.Math.Sqrt(1 + System.Math.Pow(f1 / g1, 2));
                        }
                        cs = f1 / r;
                        sn = g1 / r;
                        if (System.Math.Abs(f) > System.Math.Abs(g) && cs < 0)
                        {
                            cs = -cs;
                            sn = -sn;
                            r = -r;
                        }
                    }
                }
            }
            public static void applyrotationsfromtheleft(bool isforward,
                int m1,
                int m2,
                int n1,
                int n2,
                double[] c,
                double[] s,
                double[,] a,
                double[] work)
            {
                int j = 0;
                int jp1 = 0;
                double ctemp = 0;
                double stemp = 0;
                double temp = 0;
                int i_ = 0;
                if (m1 > m2 || n1 > n2)
                {
                    return;
                }
                if (isforward)
                {
                    if (n1 != n2)
                    {
                        for (j = m1; j <= m2 - 1; j++)
                        {
                            ctemp = c[j - m1 + 1];
                            stemp = s[j - m1 + 1];
                            if (ctemp != 1 || stemp != 0)
                            {
                                jp1 = j + 1;
                                for (i_ = n1; i_ <= n2; i_++)
                                {
                                    work[i_] = ctemp * a[jp1, i_];
                                }
                                for (i_ = n1; i_ <= n2; i_++)
                                {
                                    work[i_] = work[i_] - stemp * a[j, i_];
                                }
                                for (i_ = n1; i_ <= n2; i_++)
                                {
                                    a[j, i_] = ctemp * a[j, i_];
                                }
                                for (i_ = n1; i_ <= n2; i_++)
                                {
                                    a[j, i_] = a[j, i_] + stemp * a[jp1, i_];
                                }
                                for (i_ = n1; i_ <= n2; i_++)
                                {
                                    a[jp1, i_] = work[i_];
                                }
                            }
                        }
                    }
                    else
                    {
                        for (j = m1; j <= m2 - 1; j++)
                        {
                            ctemp = c[j - m1 + 1];
                            stemp = s[j - m1 + 1];
                            if (ctemp != 1 || stemp != 0)
                            {
                                temp = a[j + 1, n1];
                                a[j + 1, n1] = ctemp * temp - stemp * a[j, n1];
                                a[j, n1] = stemp * temp + ctemp * a[j, n1];
                            }
                        }
                    }
                }
                else
                {
                    if (n1 != n2)
                    {
                        for (j = m2 - 1; j >= m1; j--)
                        {
                            ctemp = c[j - m1 + 1];
                            stemp = s[j - m1 + 1];
                            if (ctemp != 1 || stemp != 0)
                            {
                                jp1 = j + 1;
                                for (i_ = n1; i_ <= n2; i_++)
                                {
                                    work[i_] = ctemp * a[jp1, i_];
                                }
                                for (i_ = n1; i_ <= n2; i_++)
                                {
                                    work[i_] = work[i_] - stemp * a[j, i_];
                                }
                                for (i_ = n1; i_ <= n2; i_++)
                                {
                                    a[j, i_] = ctemp * a[j, i_];
                                }
                                for (i_ = n1; i_ <= n2; i_++)
                                {
                                    a[j, i_] = a[j, i_] + stemp * a[jp1, i_];
                                }
                                for (i_ = n1; i_ <= n2; i_++)
                                {
                                    a[jp1, i_] = work[i_];
                                }
                            }
                        }
                    }
                    else
                    {
                        for (j = m2 - 1; j >= m1; j--)
                        {
                            ctemp = c[j - m1 + 1];
                            stemp = s[j - m1 + 1];
                            if (ctemp != 1 || stemp != 0)
                            {
                                temp = a[j + 1, n1];
                                a[j + 1, n1] = ctemp * temp - stemp * a[j, n1];
                                a[j, n1] = stemp * temp + ctemp * a[j, n1];
                            }
                        }
                    }
                }
            }
        }
    }
}