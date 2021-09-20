using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace examKsuMamdani
{
    public class Grafic
    {        
        List<Function> _functions = new List<Function>();

        public Grafic(List<Function> functions)
        {
            _functions = functions;
        }

        /// <summary>
        /// Вычислить значение y(х) для текущего ГРАФИКА. Функцию опередляет сам
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double GetValueY(double x)
        {
            //если значение х пересекает график (какую-то функцию)
            if (_functions[0].P1.X <= x && x <= _functions[_functions.Count - 1].P2.X)
            {
                //находим, какой по счету график пересекает функция
                int funcNum = 0;
                for (int i = 0; i < _functions.Count; i++)
                    if (_functions[i].P1.X <= x && x <= _functions[i].P2.X)
                    {
                        funcNum = i;
                        break;
                    }
                return _functions[funcNum].K * x + _functions[funcNum].B;
            }
            else
                //MessageBox.Show("х" + x + " не попадает в допустимый диапазон значений"); //ксю
                return 0;              
        }


        public int GetCaseNumber(double y)
        {
            //ПЕРВЫЙ ВАРИАНТ
            //если 1-ая функция не пересекает (выше отсечения)
            if (_functions[0].P1.Y > y && _functions[0].P2.Y > y)
            {
                //если 2-ая функция пересекает
                if (_functions[1].P1.Y >= y && _functions[1].P2.Y <= y)
                {
                    return 1;
                }
            }

            //ВТОРОЙ ВАРИАНТ
            //если 1-ая функция пересекает 
            if (_functions[0].P1.Y <= y && _functions[0].P2.Y >= y)
            {
                //если 2-ая функция не пересекает (выше отсечения)
                if (_functions[1].P1.Y > y && _functions[1].P2.Y > y)
                {
                    return 2;
                }
            }

            //ТРЕТИЙ ВАРИАНТ
            //если 1-ая функция пересекает 
            if (_functions[0].P1.Y <= y && _functions[0].P2.Y >= y)
            {
                //если 2-ая функция пересекает
                if (_functions[1].P1.Y >= y && _functions[1].P2.Y <= y)
                {
                    return 3;
                }
            }

            return 0;
        }

        /// <summary>
        /// Генерирует результирующий график результата для выбранного Правила
        /// </summary>
        public Grafic MakeOtsech(double y)
        {
            //1. СОЗАТЬ лист для хранения точек нового ГРАФИКА.
            //2. ПРОВЕРИТЬ какой тип отсечения мы рассматриваем (сколько функций пересекаются и какие конкретно)
            //   Правило: если функция не пересекает, значит она ГОРИЗОНТАЛЬНАЯ.
            //            Проконтролировать при создании правил!

            //3. ДОБАВИТЬ в лист точек значения точек для функций с учетом отсечений.
            //4. СФОРМИРОВАТЬ новые функции по Листу точек.
            //5. СФОРМИРОВАТЬ новый график по новым функциям.
            //6. ВЕРНУТЬ график.


            List<Point> points = new List<Point>();
            List<Function> funcsWithOtsech = new List<Function>();
            double xCrossY;

            //ТРЕТИЙ ВАРИАНТ
            //если 1-ая функция пересекает 
            if (_functions[0].P1.Y < y && _functions[0].P2.Y >= y ||
                _functions[0].P1.Y <= y && _functions[0].P2.Y > y)
            {
                //если 2-ая функция пересекает
                if (_functions[1].P1.Y > y && _functions[1].P2.Y <= y ||
                    _functions[1].P1.Y >= y && _functions[1].P2.Y < y)
                {
                    xCrossY = _functions[0].GetValueX(y);//получили значение х для y 1-ой функции
                    points.Add(new Point(_functions[0].P1.X, _functions[0].P1.Y));
                    points.Add(new Point(xCrossY, y));
                    xCrossY = _functions[1].GetValueX(y);//получили значение х для y 2-ой функции
                    points.Add(new Point(xCrossY, y));
                    points.Add(new Point(_functions[1].P2.X, _functions[1].P2.Y));
                }
            }

            //ПЕРВЫЙ ВАРИАНТ
            //если 1-ая функция пересекает 
            if (_functions[0].P1.Y <= y && _functions[0].P2.Y >= y)
            {
                //если 2-ая функция не пересекает (выше отсечения)
                if (_functions[1].P1.Y >= y && _functions[1].P2.Y >= y)
                {
                    xCrossY = _functions[0].GetValueX(y);//получили значение х для y 
                    points.Add(new Point(_functions[0].P1.X, _functions[0].P1.Y));
                    points.Add(new Point(xCrossY, y));
                    points.Add(new Point(_functions[1].P2.X, y));
                }
            }

            //ВТОРОЙ ВАРИАНТ
            //если 1-ая функция не пересекает (выше отсечения)
            if (_functions[0].P1.Y > y && _functions[0].P2.Y > y)
            {
                //если 2-ая функция пересекает
                if (_functions[1].P1.Y >= y && _functions[1].P2.Y <= y)
                {
                    xCrossY = _functions[1].GetValueX(y);//получили значение х для y функции
                    points.Add(new Point(0, y));
                    points.Add(new Point(xCrossY, y));
                    points.Add(new Point(_functions[1].P2.X, _functions[1].P2.Y));

                }
            }

            

            //составляем все функции на графике
            for (int i = 0; i < points.Count - 1; i++)
            {
                funcsWithOtsech.Add(new Function(points[i], points[i + 1]));
            }

            //тут делаем новый график, учитывая, что функции отсеклись
            return new Grafic(funcsWithOtsech);
        }

    }
}
