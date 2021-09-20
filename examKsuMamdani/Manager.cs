using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examKsuMamdani
{
    public class Manager
    {
        /// <summary>
        /// значение y в результирующей функции с отсечениями и объединением
        /// </summary>
        public List<double> y = new List<double>();
        public List<double> x = new List<double>();
        List<Rule> _rules = new List<Rule>();

        public double CountMax()
        {
            int maxEndIndex = 0;//index максимальная высота - КОНЕЦ
            for (int yi = 0; yi < y.Count; yi++)
            {
                if (y[yi] >= y[maxEndIndex])
                {
                    maxEndIndex = yi;
                }
            }
            int maxStartIndex = maxEndIndex;//index максимальная высота - СТАРТ
            while (y[maxStartIndex] == y[maxEndIndex])
            {
                if (maxStartIndex == 0)
                    break;
                maxStartIndex--;
            }
            maxStartIndex++;//в цикле while правильно указали номер

            //ищем z по методу максимумов 
            double zMax = (maxStartIndex + maxEndIndex) / 2.0;
            return zMax;
        }

        public double CountCentroid()
        {
            double zCentreChislitel = 0;
            double zCentreZnamenatel = 0;
            for (int i = 0; i < y.Count; i++)
            {
                zCentreChislitel += x[i] * y[i];
                zCentreZnamenatel += y[i];
            }
            double zCentroid = zCentreChislitel / zCentreZnamenatel;
            return zCentroid;
        }

        public List<Rule> GetRules()
        {
            return _rules;
        }

        //public double GetResult(string procent, string time)
        public void GetResult(string procent, string time)
        {
            EnterListOfRules();
            GetResultUsingKnowledge(double.Parse(procent), double.Parse(time));
            //return GetResultUsingKnowledge(double.Parse(procent), double.Parse(time));
        }

        void EnterListOfRules()
        {
            //вводим данные для первого правила
            List<Function> _funcsProcent1 = new List<Function>();
            _funcsProcent1.Add(new Function(new Point(0, 0), new Point(50, 0)));
            _funcsProcent1.Add(new Function(new Point(50, 0), new Point(100, 1)));
            Grafic gr1_proc = new Grafic(_funcsProcent1);

            List<Function> _funcsTime1 = new List<Function>();
            _funcsTime1.Add(new Function(new Point(0, 0), new Point(28, 1)));
            _funcsTime1.Add(new Function(new Point(28, 1), new Point(30, 1)));
            Grafic gr1_time = new Grafic(_funcsTime1);

            List<Function> _funcsResult1 = new List<Function>();
            _funcsResult1.Add(new Function(new Point(70, 0), new Point(90, 1)));
            _funcsResult1.Add(new Function(new Point(90, 1), new Point(100, 1)));
            Grafic gr1_Res = new Grafic(_funcsResult1);

            Rule rule1 = new Rule(gr1_proc, gr1_time, gr1_Res);
            _rules.Add(rule1);

            //вводим данные для второго правила
            List<Function> _funcsProcent2 = new List<Function>();
            _funcsProcent2.Add(new Function(new Point(0, 1), new Point(65, 0)));
            _funcsProcent2.Add(new Function(new Point(65, 0), new Point(100, 0)));
            Grafic gr2_proc = new Grafic(_funcsProcent2);

            List<Function> _funcsTime2 = new List<Function>();
            _funcsTime2.Add(new Function(new Point(0, 1), new Point(28, 0)));
            _funcsTime2.Add(new Function(new Point(28, 0), new Point(30, 0)));
            Grafic gr2_time = new Grafic(_funcsTime2);

            List<Function> _funcsResult2 = new List<Function>();
            _funcsResult2.Add(new Function(new Point(0, 1), new Point(10, 1)));
            _funcsResult2.Add(new Function(new Point(10, 1), new Point(30, 0)));
            Grafic gr2_Res = new Grafic(_funcsResult2);

            Rule rule2 = new Rule(gr2_proc, gr2_time, gr2_Res);
            _rules.Add(rule2);

            //вводим данные для третьего правила
            List<Function> _funcsProcent3 = new List<Function>();
            _funcsProcent3.Add(new Function(new Point(0, 0), new Point(50, 1)));
            _funcsProcent3.Add(new Function(new Point(50, 1), new Point(100, 0)));
            Grafic gr3_proc = new Grafic(_funcsProcent3);

            List<Function> _funcsTime3 = new List<Function>();
            _funcsTime3.Add(new Function(new Point(0, 1), new Point(20, 0)));
            _funcsTime3.Add(new Function(new Point(20, 0), new Point(30, 0)));
            Grafic gr3_time = new Grafic(_funcsTime3);

            List<Function> _funcsResult3 = new List<Function>();
            _funcsResult3.Add(new Function(new Point(20, 0), new Point(40, 1)));
            _funcsResult3.Add(new Function(new Point(40, 1), new Point(60, 0)));
            Grafic gr3_Res = new Grafic(_funcsResult3);

            Rule rule3 = new Rule(gr3_proc, gr3_time, gr3_Res);
            _rules.Add(rule3);

            //вводим данные для четвертого правила
            List<Function> _funcsProcent4 = new List<Function>();
            _funcsProcent4.Add(new Function(new Point(0, 0), new Point(50, 1)));
            _funcsProcent4.Add(new Function(new Point(50, 1), new Point(100, 0)));
            Grafic gr4_proc = new Grafic(_funcsProcent4);

            List<Function> _funcsTime4 = new List<Function>();
            _funcsTime4.Add(new Function(new Point(0, 0), new Point(10, 1)));
            _funcsTime4.Add(new Function(new Point(10, 1), new Point(30, 1)));
            Grafic gr4_time = new Grafic(_funcsTime4);

            List<Function> _funcsResult4 = new List<Function>();
            _funcsResult4.Add(new Function(new Point(40, 0), new Point(60, 1)));
            _funcsResult4.Add(new Function(new Point(60, 1), new Point(80, 0)));
            Grafic gr4_Res = new Grafic(_funcsResult4);

            Rule rule4 = new Rule(gr4_proc, gr4_time, gr4_Res);
            _rules.Add(rule4);
        }


        //public double GetResultUsingKnowledge(double procent, double time)
        public void GetResultUsingKnowledge(double procent, double time)
        {
            //сформировали итоговую фунцкию, показывающую все точки
            List<Grafic> grafsWithOtsech = MakeListGrafsWithOtsech(procent, time);

            //успех 0-100% (100.00)
            double n = 10000;

            //заполняем массив каждой точки финальной функции нулями
            for (int i = 0; i < n; i++)
                y.Add(0);

            //находим значение y 
            for (int xi = 0; xi < n; xi++)
            {
                double curX = xi / 100.0;
                x.Add(curX);

                foreach (Grafic graf in grafsWithOtsech)
                    if (graf.GetValueY(curX) > y[xi])
                        y[xi] = graf.GetValueY(curX);
            }
        }


        public List<Grafic> MakeListGrafsWithOtsech(double procent, double time)
        {
            List<Grafic> grafsWithOtsech = new List<Grafic>();

            //получить результирующий график для правила 1
            //ищем высоту 1 правила
            double high1_1 = _rules[0].grafIf1.GetValueY(procent);
            double high1_2 = _rules[0].grafIf2.GetValueY(time);
            double high1 = Math.Max(high1_1, high1_2); //тут правило с ИЛИ, так что берем 
            //делаем отсечение
            Grafic grafWithOtsech_1 = _rules[0].grafRes.MakeOtsech(high1);
            grafsWithOtsech.Add(grafWithOtsech_1);

            _rules[0].grafResWithOtsech = grafWithOtsech_1;

            //получить резуультирующий график для правила 2
            //ищем высоту 1 правила
            double high2_1 = _rules[1].grafIf1.GetValueY(procent);
            double high2_2 = _rules[1].grafIf2.GetValueY(time);
            double high2 = Math.Min(high2_1, high2_2); //тут правило с И, так что берем 
            //делаем отсечение
            Grafic grafWithOtsech_2 = _rules[1].grafRes.MakeOtsech(high2);
            grafsWithOtsech.Add(grafWithOtsech_2);

            _rules[1].grafResWithOtsech = grafWithOtsech_2;

            //получить результирующий график для правила 3
            //ищем высоту 1 правила
            double high3_1 = _rules[2].grafIf1.GetValueY(procent);
            double high3_2 = _rules[2].grafIf2.GetValueY(time);
            double high3 = Math.Min(high3_1, high3_2); //тут правило с И, так что берем 

            int caseNumber = _rules[2].grafRes.GetCaseNumber(high3);
            //делаем отсечение
            Grafic grafWithOtsech_3 = _rules[2].grafRes.MakeOtsech(high3);
            grafsWithOtsech.Add(grafWithOtsech_3);

            _rules[2].grafResWithOtsech = grafWithOtsech_3;

            //получить резуультирующий график для правила 4
            //ищем высоту 1 правила
            double high4_1 = _rules[3].grafIf1.GetValueY(procent);
            double high4_2 = _rules[3].grafIf2.GetValueY(time);
            double high4 = Math.Min(high4_1, high4_2); //тут правило с И, так что берем 
            //делаем отсечение
            Grafic grafWithOtsech_4 = _rules[3].grafRes.MakeOtsech(high4);
            grafsWithOtsech.Add(grafWithOtsech_4);

            _rules[3].grafResWithOtsech = grafWithOtsech_4;

            return grafsWithOtsech;
        }

        
    }
}
