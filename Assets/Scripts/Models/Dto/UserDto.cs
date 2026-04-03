using System;

namespace Assets.Scripts.Models.Dto
{
    [Serializable]
    public class UserDto
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string DoctorName { get; set; }

        public string TreatmentDetails { get; set; }

        public string TreatmentDate { get; set; }

        public int CurrentLevel { get; set; }
    }
}
