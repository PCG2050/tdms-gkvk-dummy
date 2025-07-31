using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Enums
{
    public enum BlobOperationType
    {
        Read,
        Write,
        Delete,
        List
    }
    public enum ContainerType
    {
        Public,
        Private
    }
}
