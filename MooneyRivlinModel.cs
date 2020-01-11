using System;
using System.Collections.Generic;
using System.Linq;
using ShrinkageExplorer.Core.ExtensionMethods;
using ShrinkageExplorer.Core.Interfaces;

namespace ShrinkageExplorer.Core.Models
{
    public struct lengthsSector
    {
        double length;
        bool isOnRoll;
    };

    public class MooneyRivlinModel : ShrinkageModel
    {
        //TempRes temp = new TempRes();
        public override ShrinkageResult[] Calculate(IRollLine line, IFilm film)
        {
            List<lengthsSector> lengthsSpectre = new List<lengthsSector>();
            Line = line;
            IFilm = film;
            Rolls = line.WorkingRolls.ToList();
            var results = new List<ShrinkageResult>();
            Length[] ls = new Length[Rolls.Count];
            var L = Ls().Select(x => x.l).ToArray();
            var films = Films();
            //CalcCoveringAngles(ls, Rolls);
            var n = IFilm.Material.GetScalarValue("n");
            var b = IFilm.Material.GetScalarValue("b");
            var mu0 = IFilm.Material.GetScalarValue("mu0");
            var Tr = IFilm.Material.GetScalarValue("Tr");

            var A = IFilm.Material.GetScalarValue("A_mr");
            var B = IFilm.Material.GetScalarValue("B_mr");
            var C = IFilm.Material.GetScalarValue("C_mr");

            var x1 = Math.Pow(2, n + 1);

            //////////
            /*
           temp.AddStr("Расчет усадки полимерной пленки на базе модели Муни-Ривлина\r\n");
            temp.AddStr("Индекс степенного закона = " + n.ToString() + "\r\n");
            temp.AddStr("Температурный индекс = " + b.ToString() + ",1/С\r\n");
            temp.AddStr("Коэффициент вязкости = " + mu0.ToString() + ",Па*с\r\n");
            temp.AddStr("Температура приведения = " + Tr.ToString() + ",С\r\n");
            temp.AddStr("");
            temp.AddStr("Параметры тела Муни-Ривлина, эмпирические коэффициенты");
            temp.AddStr("A = " + A.ToString() + "\r\n");
            temp.AddStr("B = " + B.ToString() + "\r\n");
            temp.AddStr("C = " + C.ToString() + "\r\n");*/
            //////////

            var gSum = 0.0;
            var result = new List<double>();


            ///////
            //temp.AddStr("Валок\tТемпература\tВязкость\tНапряжение вытяжки\tПродольная усадка реологического тела Муни-Ривлина\r\n");
            double sumShrinkage = 0, goalShrinkage = 60;
            double sMin = 0, sMax = goalShrinkage * 1.5 / (Rolls.Count - 1);
            for (var i = 0; i < Rolls.Count - 1; ++i)
            {
                var currentRoll = Rolls[i];
                var nextRoll = Rolls[i + 1];

                var currentFilm = films[i];
                var nextFilm = films[i + 1];
                var x2 = Math.Pow((currentRoll.Velocity * n) / (L[i] * (1 - n)), n) * Math.Pow(2, n + 1);
                var x3 = Math.Pow(1 - currentRoll.Velocity / nextRoll.Velocity, 1 - n);
                var x4 = (currentFilm.Thickness * currentFilm.Width) / (nextFilm.Thickness * nextFilm.Width);
                var temperature = nextRoll.Temperature - Tr;
                var mu = mu0 * Math.Exp(-b * temperature);
                var G = x1 * x2 * x3 * x4 * mu;
                if (!double.IsNaN(G) && !double.IsInfinity(G))
                    gSum = G;
                const double step = 0.1;
                var sum = gSum;
                //AddStr("");
                //temp.AddStr(string.Format("\nПоиск корня уравнения: {0}*x^6 + {1}*x^4 + {2}*x^3 - {1}*x^2 - {3}", C, A, B - C - sum, B));
                //temp.AddStr(string.Format("\nна отрезке: [{0}; {1}]", sMin, sMax));
                //var root = MathMethods.ScanRoot(Sl => (C * Math.Pow(Sl, 6) + A * Math.Pow(Sl, 4) + (B - C - sum) * Math.Pow(Sl, 3) - A * Sl * Sl - B), sMin, sMax, 0.0001, 1000);
                var root = MathMethods.BisectRoot(Sl => (C * Math.Pow(Sl, 6) + A * Math.Pow(Sl, 4) + (B - C - sum) * Math.Pow(Sl, 3) - A * Sl * Sl - B), sMin, sMax, 0.0001, 100000);
                //AddStr("");
                //temp.AddStr(string.Format("\nF({0}) = {1}", root, (C * Math.Pow(root, 6) + A * Math.Pow(root, 4) + (B - C - sum) * Math.Pow(root, 3) - A * root * root - B)));

                //sMin += root;
                //sMax += root;
                int ind = 0;
                if (double.IsNaN(root))
                {
                    ind++;
                    break;
                }
                if (!double.IsNaN(root) && Math.Abs(Math.Round(root, 5) - 1) > 0.001)
                    result.Add(root);

                ///////
                //temp.AddStr((i + 1).ToString() + "\t\t" + temperature.ToString() + "С\t" + Math.Round(mu, 4).ToString() + "Па*с\t\t " + Math.Round(root, 4).ToString() + "\t\t" + Math.Round(G, 4) + "\t");
                ///////
                //results.Add(new ShrinkageResult(-result.Sum(x => (x)), ind, 0));
            }

            //////
            //AddStr("");
            //temp.AddStr("Усадка полимерной пленки");
            //temp.AddStr("Ширина\t\tДлина\t\tТолщина");
            /*foreach (var item in results)
            {
                temp.AddStr(Math.Round(item.Sw, 5).ToString() + "% \t" + Math.Round(item.Sl, 5).ToString() + "% \t" + item.Sh.ToString() + "%");
            }*/

            //temp.SaveToFile("D:\\" + ModelName + ".txt");
            ////////

            return results.ToArray();
        }
        public override string ModelName
        {
            get
            {
                return "Mooney-Rivlin model";
            }
        }
        
    }
}
