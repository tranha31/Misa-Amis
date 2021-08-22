using Microsoft.AspNetCore.Mvc;
using MISA.Amis.Core.Entities;
using MISA.Amis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Amis.Api.Controllers
{
    /// <summary>
    /// Khai báo đường dẫn cho controller của Phòng ban
    /// Create by: TQHa (22/7/2021)
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class DepartmentController : BaseController<Department>
    {
        IBaseService<Department> _baseService;

        public DepartmentController(IBaseService<Department> baseService) : base(baseService)
        {
            _baseService = baseService;
        }

    }
}
