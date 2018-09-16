using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using VkNet.Abstractions;
using VkNet.Enums;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;
using Group = VkNet.Model.Group;

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
		private const int APPLICATION_ID = 6694205;

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
			var owner_id = GetOwnerId(user_id);
			var posts = _vkApi.Wall.Get(new WallGetParams() { Count = 5, OwnerId = owner_id});
			var sb = new StringBuilder();
			posts.WallPosts.ToList().ForEach(t => sb.Append(t.Text));
			return sb.ToString();
		}

	    public void Authorize(string token)
	    {
	        _vkApi.Authorize(new ApiAuthParams
	        {
	           AccessToken = token

	        });
        }

	    /// <summary>
		/// Получает последние посты.
		/// </summary>
		/// <param name="user_id">Идентификатор группы или пользователя.</param>
		/// <param name="post">Количество постов.</param>
		public void SendPosts(string user_id, string post)
	    {
	        var owner_id = GetOwnerId(user_id);
	        var wall = _vkApi.Wall.Get(new WallGetParams(){OwnerId  = owner_id});
	        var api_user = _vkApi.Users.Get(new List<long>() {_vkApi.UserId.Value}).FirstOrDefault();
		    var message = $"{api_user.FirstName} {api_user.LastName}, статистика для последних 5 постов: {post}";
            var post_id = _vkApi.Wall.Post(new WallPostParams(){
			    OwnerId = owner_id,
			    Message = message,
			    FromGroup = true,
			});
		}

        /// <summary>
        /// Получает идентификатор группы или пользователя.
        /// </summary>
        /// <param name="user_id">Идентификатор пользователя в виде строке.</param>
        /// <returns>Идентификатор пользователя.</returns>
	    private long GetOwnerId(string user_id)
	    {
	        var gr = _vkApi.Utils.ResolveScreenName(user_id);
	        var id = gr.Id.Value;

	        var owner_id = (user_id.ToLower().StartsWith("id") ? 1 : -1 ) * id;
	        return owner_id;
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
								Settings = Settings.All,
                                TwoFactorAuthorization = default(Func<string>)

            });
		}
    }
}
