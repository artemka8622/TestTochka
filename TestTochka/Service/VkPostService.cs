using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using VkNet.Abstractions;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace TestTochka.Service
{
	/// <summary>
	/// Класс работы с внешними сервисами.
	/// </summary>
    public class VkPostService : IExternalApiService
	{
		/// <summary>
		/// Идентификатор приложения.
		/// </summary>
		private const int APPLICATION_ID = 123456;

		/// <summary>
		/// API вконтакте.
		/// </summary>
		private readonly IVkApi _vkApi;

		/// <summary>
		/// Конструктор.
		/// </summary>
		/// <param name="vk_api">API вконтакте.</param>
		public VkPostService(IVkApi vk_api)
		{
			_vkApi = vk_api;
		}

		/// <summary>
		/// Получает последние посты.
		/// </summary>
		/// <param name="user_id">Идентификатор группы или пользователя.</param>
		/// <param name="count">Количество постов.</param>
		/// <returns>Посты.</returns>
		public string GetPosts(string user_id, int count)
		{
			var owner_id = _vkApi.Utils.ResolveScreenName(user_id).Id;
			var posts = _vkApi.Wall.Get(new WallGetParams() { Count = 5, OwnerId = owner_id});
			var sb = new StringBuilder();
			posts.WallPosts.ToList().ForEach(t => sb.Append(t.Text));
			return sb.ToString();
		}

		/// <summary>
		/// Получает последние посты.
		/// </summary>
		/// <param name="user_id">Идентификатор группы или пользователя.</param>
		/// <param name="post">Количество постов.</param>
		public void SendPosts(string user_id, string post)
		{
			var owner_id = _vkApi.Utils.ResolveScreenName(user_id).Id;
			var post_id = _vkApi.Wall.Post(new WallPostParams(){ OwnerId = owner_id, Message = post});
		}

		/// <summary>
		/// Авторизация пользователя.
		/// </summary>
		/// <param name="login">Логин.</param>
		/// <param name="password">Пароль.</param>
		public void Authorize(string login, string password)
		{
			_vkApi.Authorize(new ApiAuthParams
							{
								ApplicationId = APPLICATION_ID,
								Login = login,
								Password = password,
								Settings = Settings.All
							});
		}
    }
}
