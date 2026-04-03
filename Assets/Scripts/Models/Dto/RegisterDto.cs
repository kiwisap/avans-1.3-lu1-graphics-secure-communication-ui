using System;

namespace Assets.Scripts.Models.Dto
{
    [Serializable]
    public class RegisterDto
    {
        public string Email { get; set; } = default!;

        public string Password { get; set; } = default!;

        public string Name { get; set; } = default!;

        public int Age { get; set; }

        public string DoctorName { get; set; }

        public string TreatmentDetails { get; set; }

        public string TreatmentDate { get; set; }
    }
}
