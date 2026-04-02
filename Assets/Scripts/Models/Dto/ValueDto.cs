using System;

namespace Assets.Scripts.Models.Dto
{
    [Serializable]
    public class ValueDto<T>
    {
        public T Value { get; set; }
    }
}
