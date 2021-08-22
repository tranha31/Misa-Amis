using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Enums
{
    /// <summary>
    /// Mã trạng thái service
    /// </summary>
    public enum MisaEnum
    {
        Success = 100,
        Valid = 200,
        NotValid = 300,
        ErrorSever = 500,
        Duplicate = 400,

    }

    /// <summary>
    /// Giới tính
    /// </summary>
    public enum Gender
    {
        Felmale = 1,
        Male = 0,
        Other = 2
    }
}
