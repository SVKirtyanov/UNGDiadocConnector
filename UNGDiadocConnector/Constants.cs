namespace UNGDiadocConnector
{
	internal static class Constants
	{
		// URL веб-сервиса Диадок
		internal const string DefaultApiUrl = "https://diadoc-api.kontur.ru";

		// Идентификатор клиента, он же ключ разработчика
		internal const string DefaultClientId = "API-ee30b0b7-5e35-460e-bca2-c4a0b06d823b";

		// Логин для авторизации на сервере Диадок
		internal const string DefaultLogin = "kirtyanovsv@sibintek.ru";

		// Пароль для авторизации на сервере Диадок
		internal const string DefaultPassword = "BuddaBra15$";
		// Подставьте сюда идентификатор вашего ящика (отправителя), из которого будете отправлять документ.
		// Допустимы форматы как в GUID (12345675-1234-1234-1234-123456789012),
		// так и в формате вида 12345675123412341234123456789012@diadoc.ru
		internal const string DefaultFromBoxId = "dc6123f4-32cf-4fcd-ba1a-adf3f43a12d0";//"2BM-9630945424-963001000-202205091229093276660";

		// Подставьте сюда идентификатор ящика-получателя, в который будете отправлять документ.
		internal const string DefaultToBoxId = "2568c934-976f-47b8-8413-47bc178495a3";//"2BM-9646353494-964601000-202206211126027368125";
		  
		// Подставьте сюда путь до публичной части сертификата, которым будет подписан документ (пример: C:\public.cer)
		// Важно, чтобы приватная часть ключа была установлена в машину, на которой будет выполняться этот код
		internal const string CertificatePath = @"C:\svkirtyanov\TestCryptoProCertStrong.cer";
	}
}