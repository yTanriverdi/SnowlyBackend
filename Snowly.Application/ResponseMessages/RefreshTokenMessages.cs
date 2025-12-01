using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Application.ResponseMessages
{
    public static class RefreshTokenMessages
    {
        public const string AddedRefreshToken = "Yenileme tokeni kaydedildi";
        public const string ReturnRefreshToken = "Mevcut yenileme tokeni döndü";
        public const string RecreateRefreshToken = "Yenileme tokeni değiştirildi";
        public const string WrongOrTimeUpRefreshToken = "Yenileme tokeni süresi geçmiş veya token yanlış";
        public const string PleaseNewRefreshToken = "Yenileme tokeni kalmamış";
    }
}
