using PLC.Admin.Api.Cores.Business.Events;
using PLC.Admin.Api.Cores.Business.Query;
using PLC.Admin.Api.Cores.Infrastructures.Repository;
using PLC.CQ;
using PLC.UtilityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Api.Business.Query
{
    public sealed class GetAllAdminQuery : IGetAllAdminQuery
    {
        private readonly IGetAllAdminRepository getAllAdminRepository = null;
        private readonly IAdminEncrypteListEventHandler adminEncrypteListEventHandler = null;

        public GetAllAdminQuery(IGetAllAdminRepository getAllAdminRepository, IAdminEncrypteListEventHandler adminEncrypteListEventHandler)
        {
            this.getAllAdminRepository = getAllAdminRepository;
            this.adminEncrypteListEventHandler = adminEncrypteListEventHandler;
        }

        async Task<object> IQueryHandler<object>.HandleAsync()
        {
            try
            {
                var getData = await this.getAllAdminRepository?.GetAllAsync();

                if (getData == null || getData?.Count == 0) return new ErrorModel() { StatusCode = 401, Message = "No data found" };

                var encryptedList = await adminEncrypteListEventHandler?.EventHandleAsync(getData.ToList());

                return encryptedList;
            }
            catch
            {
                throw;
            }
        }
    }
}