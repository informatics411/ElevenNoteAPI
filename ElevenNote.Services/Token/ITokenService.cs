using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElevenNote.Models.Token;

    public class ITokenService
    {
        public interface ITokenService
        {
            Task<TokenResponse> GetTokenAsync(TokenRequest model);
        }
    }
