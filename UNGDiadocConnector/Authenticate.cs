using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Diadoc.Api;
using Diadoc.Api.Cryptography;

namespace UNGDiadocConnector
{
	internal static class Authenticate
	{
		private const string CertificateThumbprint = "<Подставьте сюда отпечаток публичной части сертификата>";

		public static string? AuthToken { get; set; }
		
		public static string GetToken()
		{
			Console.WriteLine($"Подключение к Диадок");
			Console.WriteLine("=====================================");

			// Для использования API Диадока требуются:
			// 1. Крипто-API, предоставляемое операционной системой. Для систем на ОС Windows используйте класс WinApiCrypt.
			// 2. Экземпляр класса DiadocApi, проксирующий работу с Диадоком.

			var crypt = new WinApiCrypt();
			var diadocApi = new DiadocApi(
				Constants.DefaultClientId,
				Constants.DefaultApiUrl,
				crypt);

			// Большинству команд интеграторского интерфейса требуется авторизация.
			// Для этого команды требуют в качестве обязательного параметра так называемый авторизационный токен — массив байтов, однозначно идентифицирующий пользователя.
			// Один из способов авторизации — через логин и пароль пользователя:
			var authTokenByLogin = diadocApi.Authenticate(Constants.DefaultLogin, Constants.DefaultPassword);
			AuthToken = authTokenByLogin;
			Console.WriteLine("Успешная аутентификация по логину и паролю. Токен: " + authTokenByLogin);

			//// Другой способ: через сертификат пользователя:
			//var certificate = new X509Certificate2(File.ReadAllBytes(Constants.CertificatePath));
			//var authTokenByCertificate = diadocApi.Authenticate(certificate.RawData);
			//Console.WriteLine("Успешная аутентификация по сертификату. Токен: " + authTokenByCertificate);

			//// Можно использовать перегрузку, которая сама найдёт сертификат по отпечатку
			//var authTokenByCertificateThumbprint = diadocApi.Authenticate(CertificateThumbprint);
			//Console.WriteLine("Успешная аутентификация по отпечатку сертификата. Токен: " + authTokenByCertificateThumbprint);

			// В дальнейшем полученный токен следует подставлять в те методы API, где он требуется. (PostMessage и т.п.)
			// Токен длится 24 часа, после его протухания методы начнут возвращать 401, и потребуется вновь получить токен через методы выше.
			return authTokenByLogin;
		}
	}
}