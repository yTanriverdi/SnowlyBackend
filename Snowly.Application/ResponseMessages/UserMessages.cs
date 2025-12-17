namespace Snowly.Application.ResponseMessages
{
    public static class UserMessages
    {
        public const string UserNotFound = "Kullanıcı bulunamadı";
        public const string UserFound = "Kullanıcı bulundu";
        public const string UserAlreadyExists = "Bu E-Posta kullanılıyor";
        public const string UserCreatedSuccessfully = "Kullanıcı başarıyla oluşturuldu";
        public const string InvalidPassword = "Geçersiz parola";
        public const string Unauthorized = "Yetkisiz erişim";
        public const string UserDeleteFail = "Kullanıcı silinemedi";
        public const string UserDeleteSuccess = "Kullanıcı silindi";
        public const string UserUpdateSuccess = "Kullanıcı güncelleme başarılı";
        public const string WrongEmailOrPassword = "E-Posta veya şifre yanlış";
        public const string LoginAccess = "Giriş başarılı";
        public const string WrongPassword = "Şifre yanlış";
        public const string SuccessPasswordChange = "Şifre güncelleme başarılı";
        public const string FailPasswordChange = "Şifre güncelleme başarısız";
        public const string StillWrongConfirmCode = "Hesabınız henüz onaylanmamış E-Postanızı kontrol edin";
        public const string OnlineStatusChangeFail = "Çevrimiçi durumu güncellenemedi";
        public const string OnlineStatusChangeSuccess = "Çevrimiçi durumu güncellendi";
    }
}
