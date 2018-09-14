namespace TestTochka.Service
{
	/// <summary>
	/// Интерфейс для работы с внешними сервисами.
	/// </summary>
	public interface IExternalApiService
	{
		/// <summary>
		/// Получает последние посты.
		/// </summary>
		/// <param name="user_id">Идентификатор группы или пользователя.</param>
		/// <param name="count">Количество постов.</param>
		/// <returns>Посты.</returns>
		string GetPosts(string user_id, int count);

		/// <summary>
		/// Авторизация пользователя.
		/// </summary>
		/// <param name="login">Логин.</param>
		/// <param name="password">Пароль.</param>
		void Authorize(string login, string password);

		/// <summary>
		/// Получает последние посты.
		/// </summary>
		/// <param name="user_id">Идентификатор группы или пользователя.</param>
		/// <param name="post">Количество постов.</param>
		void SendPosts(string user_id, string post);
	}
}