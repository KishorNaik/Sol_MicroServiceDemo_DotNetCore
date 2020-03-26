using Dapper;
using PLC.Dapper.Helpers;
using PLC.Models.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace PLC.Course.Api.Infrastructures.Abstract
{
    public abstract class CourseAbstract : DapperBaseAbstract<CourseModel>
    {
        #region Protected Method
        protected async override Task<DynamicParameters> GetParameterAsync(string command, CourseModel model = null)
        {
            try
            {
                var dynamicParameter = await base.GetParameterAsync(command);

                dynamicParameter.Add("@CourseIdentity", model?.CourseIdentity, DbType.String, direction: ParameterDirection.Input);
                dynamicParameter.Add("@CourseName", model?.CourseName, DbType.String, ParameterDirection.Input);

                return dynamicParameter;
            }
            catch
            {
                throw;
            }
        }
        #endregion 
    }
}
