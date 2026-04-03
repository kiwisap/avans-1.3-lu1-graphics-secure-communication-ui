using System;

namespace Assets.Scripts.Models.Dto
{
    [Serializable]
    public class LoginDto
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
