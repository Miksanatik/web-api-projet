using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.Models;

namespace API.Domain.Services.Communication
{
    public class ConcreteResponse<T> : BaseResponse where T : class
    {
        public T Entity { get; private set; }

        private ConcreteResponse(bool success, string message, T entity) : base(success, message)
        {
            Entity = entity;
        }

        public ConcreteResponse(T entity) : this(true, string.Empty, entity)
        { }

        public ConcreteResponse(string message) : this(false, message, null)
        { }
    }
}
