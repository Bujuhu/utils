using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Utils
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string CertificateFile { get; set; }
        public TimeSpan Duration { get; set; }
        public X509SecurityKey GetCertificate()
        {
            var cert = new X509Certificate2(CertificateFile, string.Empty, X509KeyStorageFlags.MachineKeySet);
            return cert.HasPrivateKey ? new X509SecurityKey(cert) : null;
            
        }
        public SigningCredentials GetCredentials()
        {
            return new SigningCredentials(GetCertificate(), SecurityAlgorithms.RsaSha256);
        }

    }
}
