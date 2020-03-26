using PLC.Course.Api.Cores.Repository;
using PLC.Course.Api.Infrastructures.Abstract;
using PLC.Dapper.Core.DbProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using PLC.Models.Course;

namespace PLC.Course.Api.Infrastructures.Repository
{
    public sealed class GetCourseListRepository : CourseAbstract, IGetCourseListRepository
    {
        #region Declaration
        private readonly ISqlClientDbProviders sqlClientDbProviders = null;
        #endregion

        #region Constructor
        public GetCourseListRepository(ISqlClientDbProviders sqlClientDbProviders)
        {
            this.sqlClientDbProviders = sqlClientDbProviders;
        }
        #endregion 

        #region Public Methods
        async Task<dynamic> IGetCourseListRepository.GetCourseListAsync()
        {
            try
            {
                return
                    await
                        base
                            .DapperFluent
                            ?.Value
                            ?.SqlOpenConnectionAsync(await this.sqlClientDbProviders.GetConnectionAsync())
                            ?.SqlParameterAsync(async () => await base.GetParameterAsync("Get-CourseList"))
                            ?.SqlCommandAsync(async (leDbConnection, leDynamicParameter) =>
                            {
                                var queryData =
                                    await
                                    leDbConnection
                                    ?.QueryAsync<CourseModel>("Course.uspGetCourse", param: leDynamicParameter, commandType: CommandType.StoredProcedure);

                                return queryData;
                            })
                            ?.ResultAsync<dynamic>();
            }
            catch
            {
                throw;
            }
        }
        #endregion 
    }
}
