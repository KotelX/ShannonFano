using System;
using System.Collections.Generic;
using System.Linq;

namespace MethodShannonFano
{
    class Program
    {
        static void Main(string[] args)
        {
            var Chars = new List<IsChar>();         
            Console.WriteLine("Write world");
            String InputWorld = Console.ReadLine();
            for (int i = 0; i < InputWorld.Length; i++)     
            {
                double frequency = Probability(InputWorld, InputWorld[i]);
                Chars.Add(new IsChar{ Name = InputWorld[i], Frequency = frequency});
            }   //деление строки на символы и запись в List                                                                      
            var res = GetCode(Chars);       //Построение кода
            Console.WriteLine("Resul:");
            foreach (var item in Chars)     
            {
                Console.Write(res.First(x => x.Name == item.Name).Code + " ");
            }  //Результат
            Console.ReadLine();
        }
        /// <summary>
        /// Расчитывает частоту аовторения букв
        /// </summary>
        /// <param name="inputString">Строка, в которой необходимо произвести расчеты</param>
        /// <param name="name">Знак, частоту когорого необходимо расчитать</param>
        /// <returns>Частота повторения</returns>
        private static double Probability(string inputString, char name)
        {
            double result = 0;
            for (int i = 0; i < inputString.Length; i++)
            {
                if (inputString[i] == name)
                    result++;
            }
            return result / inputString.Length;
        }
        /// <summary>
        /// Основной метод для генерации кода символа
        /// </summary>
        /// <param name="chars">Список IsChar</param>
        /// <returns>List<IsChar></IsChar></returns>
        private static List<IsChar> GetCode(List<IsChar> chars) 
        {
            chars = chars.GroupBy(x => x.Name).Select(x => x.First()).ToList();     //Сортировка: удаление двойников символа
            chars = chars.OrderByDescending(x => x.Frequency).ToList();             //Сеортировка по частоте(по убыванию)
            Divider(chars);
            return chars;
        }
        /// <summary>
        /// Непосредственная генерация кода для знаков
        /// </summary>
        /// <param name="chars">Список знаков с частотой</param>
        /// <returns>List<IsChar></IsChar></returns>
        public static List<IsChar> Divider(List<IsChar> chars)
        {
            if (chars.Count < 1)        //Проверка длины списка
                return null;
            double difference = 1;
            double summ = chars[0].Frequency;
            for (int i = 1; i < chars.Count; i++)
            {
                double newDifference = Math.Abs((chars.Sum(x => x.Frequency) - summ) - summ);   //Деление на две группы разность суммы эдементов которых минимальная
                if (newDifference >= difference | newDifference == 0)
                {
                    i -= newDifference == 0 ? 0 : 1;
                    var chars2 = new List<IsChar>();
                    for (int j = 0; j < i; j++)         //Заполнение кода группы 1
                    {
                        chars[j].Code += '0';
                        chars2.Add(chars[j]);           
                    }
                    Divider(chars2);                     //деление группы 1 на 2 группы(если это нужно)
                    chars2.Clear();
                    for (int k = i; k < chars.Count; k++)   //Заполнение кода груааы 2 и создание ее копии для дальнейшей генерации кода(если необходимо)(Эта проверка находится в начале метода)
                    {
                        chars2.Add(chars[k]);
                        chars[k].Code += '1';
                    }
                    Divider(chars2);            //деление группы 2 на 2 группы(если это нужно)
                    return null;
                }
                difference = newDifference;
            }
            return null;
        }
    }
}
