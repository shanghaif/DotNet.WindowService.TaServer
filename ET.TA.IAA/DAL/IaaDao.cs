using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using ET.WinService.Core.Data;

namespace ET.TA.IAA.DAL
{
    public class IaaDao
    {
        #region 超速
        /// <summary>
        /// 查询前一天的超速报警
        /// </summary>
        /// <returns></returns>
        public DataTable GetIaaSpeed()
        {
            String dayTime = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd");
            String beginTime = dayTime + " 00:00:00";
            String endTime = dayTime + " 23:59:59";
            try
            {
                Database db = DatabaseFactory.CreateDatabase(ConnectionControl.DefaultConnectionstring);
                DataSet ds = null;
                DbCommand dbCommandWrapper = null;
                try
                {
                    dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                    dbCommandWrapper.CommandType = CommandType.Text;
                    StringBuilder sql = new StringBuilder();
                    sql.Append("SELECT  ID,StartLon,StartLat,StartPosition,EndLon,EndLat,EndPosition from bi_iaa_vehicle_speed where StartDatetime>=@StartDatetime and StartDatetime<=@EndDatetime ");
                   

                    dbCommandWrapper.CommandText = sql.ToString();
                    #region Add parameters
                    db.AddInParameter(dbCommandWrapper, "@StartDatetime", DbType.String, beginTime);
                    db.AddInParameter(dbCommandWrapper, "@EndDatetime", DbType.String, endTime);
                    #endregion
                    ds = db.ExecuteDataSet(dbCommandWrapper);
                    return ds.Tables[0];
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (dbCommandWrapper != null)
                    {
                        dbCommandWrapper = null;
                    }
                    if (ds != null)
                    {
                        ds = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //批量更新数据
        public void UpdateIaaSpeed(DataTable speedDt)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(ConnectionControl.DefaultConnectionstring);
                DataSet ds = null;
                DbCommand updateCommand = null;
                try
                {
                    
                    updateCommand = db.DbProviderFactory.CreateCommand();
                    updateCommand.CommandType = CommandType.Text;
                    StringBuilder sql = new StringBuilder();
                    sql.Append(" update bi_iaa_vehicle_speed set StartPosition=@StartPosition,EndPosition=@EndPosition where ID=@ID ");
                    updateCommand.CommandText = sql.ToString();
                    #region Add parameters
                    db.AddInParameter(updateCommand, "@ID", DbType.Int32);
                    db.AddInParameter(updateCommand, "@StartPosition", DbType.String, 512);
                    db.AddInParameter(updateCommand, "@EndPosition", DbType.String, 512);

                    updateCommand.Parameters["@ID"].SourceColumn = "ID";
                    updateCommand.Parameters["@StartPosition"].SourceColumn = "StartPosition";
                    updateCommand.Parameters["@EndPosition"].SourceColumn = "EndPosition";
                 

                    #endregion
                     speedDt.TableName = "SpeedTable";
                     db.UpdateDataSet(speedDt.DataSet, "SpeedTable", null, updateCommand, null, UpdateBehavior.Standard);
                   
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (updateCommand != null)
                    {
                        updateCommand = null;
                    }
                    if (ds != null)
                    {
                        ds = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 疲劳
        /// <summary>
        /// 查询前一天的超速报警
        /// </summary>
        /// <returns></returns>
        public DataTable GetIaaTired()
        {
            String dayTime = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd");
            String beginTime = dayTime + " 00:00:00";
            String endTime = dayTime + " 23:59:59";
            try
            {
                Database db = DatabaseFactory.CreateDatabase(ConnectionControl.DefaultConnectionstring);
                DataSet ds = null;
                DbCommand dbCommandWrapper = null;
                try
                {
                    dbCommandWrapper = db.DbProviderFactory.CreateCommand();
                    dbCommandWrapper.CommandType = CommandType.Text;
                    StringBuilder sql = new StringBuilder();
                    sql.Append("SELECT  ID,StartLon,StartLat,StartPosition,EndLon,EndLat,EndPosition from bi_iaa_vehicle_tired where StartDatetime>=@StartDatetime and StartDatetime<=@EndDatetime ");


                    dbCommandWrapper.CommandText = sql.ToString();
                    #region Add parameters
                    db.AddInParameter(dbCommandWrapper, "@StartDatetime", DbType.String, beginTime);
                    db.AddInParameter(dbCommandWrapper, "@EndDatetime", DbType.String, endTime);
                    #endregion
                    ds = db.ExecuteDataSet(dbCommandWrapper);
                    return ds.Tables[0];
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (dbCommandWrapper != null)
                    {
                        dbCommandWrapper = null;
                    }
                    if (ds != null)
                    {
                        ds = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //批量更新数据
        public void UpdateIaaTired(DataTable speedDt)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(ConnectionControl.DefaultConnectionstring);
                DataSet ds = null;
                DbCommand updateCommand = null;
                try
                {

                    updateCommand = db.DbProviderFactory.CreateCommand();
                    updateCommand.CommandType = CommandType.Text;
                    StringBuilder sql = new StringBuilder();
                    sql.Append(" update bi_iaa_vehicle_tired set StartPosition=@StartPosition,EndPosition=@EndPosition where ID=@ID ");
                    updateCommand.CommandText = sql.ToString();
                    #region Add parameters
                    db.AddInParameter(updateCommand, "@ID", DbType.Int32);
                    db.AddInParameter(updateCommand, "@StartPosition", DbType.String, 512);
                    db.AddInParameter(updateCommand, "@EndPosition", DbType.String, 512);

                    updateCommand.Parameters["@ID"].SourceColumn = "ID";
                    updateCommand.Parameters["@StartPosition"].SourceColumn = "StartPosition";
                    updateCommand.Parameters["@EndPosition"].SourceColumn = "EndPosition";


                    #endregion
                    speedDt.TableName = "TiredTable";
                    db.UpdateDataSet(speedDt.DataSet, "TiredTable", null, updateCommand, null, UpdateBehavior.Standard);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (updateCommand != null)
                    {
                        updateCommand = null;
                    }
                    if (ds != null)
                    {
                        ds = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
