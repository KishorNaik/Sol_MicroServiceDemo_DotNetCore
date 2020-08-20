using Dapper;
using PLC.Admin.Api.Models;
using PLC.Dapper.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PLC.Admin.Api.Infrastructures.Abstracts
{
    public abstract class AdminRepositoryAbstract : DapperBaseAbstract<AdminModel>
    {
        protected async override Task<DynamicParameters> SetParameterAsync(string command, AdminModel model = null)
        {
            try
            {
                var dynamicParameterObj = await base.SetParameterAsync(command, model);

                dynamicParameterObj.Add("@AdminIdentity", model?.AdminIdentity, direction: ParameterDirection.Input);
                dynamicParameterObj.Add("@FirstName", model?.FirstName, direction: ParameterDirection.Input);
                dynamicParameterObj.Add("@LastName", model?.LastName, direction: ParameterDirection.Input);

                dynamicParameterObj.Add("@EmailId", model?.EmailId, direction: ParameterDirection.Input);
                dynamicParameterObj.Add("@Role", model?.Role, direction: ParameterDirection.Input);

                dynamicParameterObj.Add("@UserName", model?.AdminLogin?.UserName, direction: ParameterDirection.Input);
                dynamicParameterObj.Add("@Password", model?.AdminLogin?.Password, direction: ParameterDirection.Input);

                dynamicParameterObj.Add("@Salt", model?.AdminLogin?.Salt, direction: ParameterDirection.Input);
                dynamicParameterObj.Add("@Hash", model?.AdminLogin?.Hash, direction: ParameterDirection.Input);

                return dynamicParameterObj;
            }
            catch
            {
                throw;
            }
        }

        protected async override Task<DynamicParameters> GetParameterAsync(string command, AdminModel model = null)
        {
            try
            {
                var dynamicParameterObj = await base.GetParameterAsync(command, model);

                dynamicParameterObj.Add("@UserName", model?.AdminLogin?.UserName, direction: ParameterDirection.Input);

                return dynamicParameterObj;
            }
            catch
            {
                throw;
            }
        }
    }
}