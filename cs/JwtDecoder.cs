using System;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace Utils
{
    public static class JwtDecoder
    {
        public static JwtToken Decode(string token)
        { 
            var tokenValue = token.Split('.');

            while (tokenValue[1].Count() % 4 != 0)
            {
                tokenValue[1] += "=";
            }
            var bytes = Convert.FromBase64String(tokenValue[1]);
            var decodedString = Encoding.Default.GetString(bytes);

            return JsonSerializer.Deserialize<JwtToken>(
                decodedString, 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
        }
    }

    public class JwtToken
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public long Exp { get; set; }
        [Required]
        public long Ttl { get; set; }
        [Required]
        public string Type { get; set; } = null!;
        [Required]
        public string Key { get; set; } = null!;
        [Required]
        public string Project { get; set; } = null!;
        [Required]
        public bool Needs2FA { get; set; }
    }
}