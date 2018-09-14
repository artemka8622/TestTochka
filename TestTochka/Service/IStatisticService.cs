namespace TestTochka.Service
{
	public interface IStatisticService
	{
		/// <summary>
		/// Получает статистику букв в строке.
		/// </summary>
		/// <param name="posts">Строка с постами.</param>
		/// <returns>Статистика.</returns>
		string GetStatistic(string posts);
	}
}