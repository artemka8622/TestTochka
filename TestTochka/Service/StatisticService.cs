using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace TestTochka.Service
{
	/// <summary>
	/// Сервис получения статиски.
	/// </summary>
    public class StatisticService : IStatisticService
	{
        /// <summary>
        /// Кол-во цифр округления.
        /// </summary>
	    private const int DIGIT_ROUND = 3;

	    /// <summary>
		/// Получает статистику букв в строке.
		/// </summary>
		/// <param name="posts">Строка с постами.</param>
		/// <returns>Статистика.</returns>
		public string GetStatistic(string posts)
		{
		    string pattern = "\\W+";
            Regex rgx = new Regex(pattern,RegexOptions.Multiline);
            var result = rgx.Replace(posts, "");

		    var length = result.Length;

            var frequency = new Dictionary<char, double>();
		    for (int i = 0; i < result.Length; i++)
		    {
		        if (frequency.ContainsKey(result[i]))
		        {
		            frequency[result[i]]++;
		        }
		        else
		        {
		            frequency.Add(result[i], 1);

                }
		    }
		    var res = frequency.ToDictionary(t => t.Key, v => Math.Round(v.Value / length, DIGIT_ROUND));
            var freq = JsonConvert.SerializeObject(res, Formatting.Indented);  

            return freq;
		}
    }
}
