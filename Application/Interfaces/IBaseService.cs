using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IBaseService
    {
        Task DeleteAsync(Guid id);
    }
}
