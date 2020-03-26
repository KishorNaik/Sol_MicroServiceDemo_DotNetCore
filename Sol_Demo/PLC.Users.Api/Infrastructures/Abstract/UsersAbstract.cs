using Dapper;
using PLC.Dapper.Helpers;
using PLC.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace PLC.Users.Api.Infrastructures.Abstract
{
    public abstract class UsersAbstract : DapperBaseAbstract<UserModel>
    {
        #region Protected Method
        protected override async Task<DynamicParameters> SetParameterAsync(string command, UserModel model = null)
        {
            try
            {
                var dynamicParameterObj = await base.SetParameterAsync(command);

                dynamicParameterObj.Add("@UserIdentity", model?.UserIdentity, DbType.String);
                dynamicParameterObj.Add("@FirstName", model?.FirstName, DbType.String);
                dynamicParameterObj.Add("@LastName", model?.LastName, DbType.String);

                dynamicParameterObj.Add("@MobileNo", model?.UserCommunication?.MobileNo, DbType.String);
                dynamicParameterObj.Add("@EmailId", model?.UserCommunication?.EmailId, DbType.String);

                dynamicParameterObj.Add("@UserName", model?.UserLogin?.UserName, DbType.String);
                dynamicParameterObj.Add("@Password", model?.UserLogin?.Password, DbType.String);

                dynamicParameterObj.Add("@Salt", model?.UserLogin?.Salt, DbType.String);
                dynamicParameterObj.Add("@Hash", model?.UserLogin?.Hash, DbType.String);

                return dynamicParameterObj;
            }
            catch
            {
                throw;
            }
        }

        protected async override Task<DynamicParameters> GetParameterAsync(string command, UserModel model = null)
        {
            try
            {
                var dynamicParameterObj=await  base.GetParameterAsync(command, model);

                dynamicParameterObj.Add("@UserIdentity", model?.UserIdentity, DbType.String);
                dynamicParameterObj.Add("@FirstName", model?.FirstName, DbType.String);
                dynamicParameterObj.Add("@LastName", model?.LastName, DbType.String);

                dynamicParameterObj.Add("@MobileNo", model?.UserCommunication?.MobileNo, DbType.String);
                dynamicParameterObj.Add("@EmailId", model?.UserCommunication?.EmailId, DbType.String);

                dynamicParameterObj.Add("@UserName", model?.UserLogin?.UserName, DbType.String);
                dynamicParameterObj.Add("@Password", model?.UserLogin?.Password, DbType.String);

                return dynamicParameterObj;
            }
            catch
            {
                throw;
            }
            
        }

        #endregion 
    }
}
