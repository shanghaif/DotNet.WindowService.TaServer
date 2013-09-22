using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

using ET.TA.IAA.Models;
using ET.WinService.Core.Data;

namespace ET.TA.IAA.DAL
{
    public class AnalyserDAO
    {

        /// <summary>
        /// 查询前一天的超速报警
        /// </summary>
        /// <returns></returns>
        public List<bi_iaa_vehicle_speed> GetANA_AnalyserSpeedMap()
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
                    sql.Append("SELECT  ");
                    sql.Append(" hvs.VehicleID,hvs.PlateNumber,hvs.PlateColor,");
                    sql.Append(" hvs.DriverICCode,hvs.DriverName,hvs.StartDatetime,hvs.EndDatetime,hvs.MaxSpeed,hvs.MinSpeed,hvs.AverageSpeed,");
                    sql.Append(" hvs.StartLon,hvs.StartLat,hvs.EndLon,hvs.EndLat,hvs.StartMileage,hvs.EndMileage,hvs.StartOil,hvs.EndOil,hvs.AlarmNum,");
                    sql.Append(" hvs.AnalyseConditions,hvs.AnalyseObject,hvs.ConditionsUserId,hvs.CreateDatetime,hvs.UnitName,hvs.SpeedThreshold,hvs.SpeedType ");
                    sql.Append(" from ");
                    sql.Append(" BI_HTA_Vehicle_Speed as hvs INNER join");
                    sql.Append(" etta_basedata.BI_Vehicle_Info as bvi on hvs.VehicleID=bvi.id ");
                    sql.Append(" where hvs.StartDatetime >= @StartDatetime and hvs.EndDatetime <= @EndDatetime");

                    dbCommandWrapper.CommandText = sql.ToString();
                    #region Add parameters
                    db.AddInParameter(dbCommandWrapper, "@StartDatetime", DbType.String, beginTime);
                    db.AddInParameter(dbCommandWrapper, "@EndDatetime", DbType.String, endTime);
                    #endregion
                    ds = db.ExecuteDataSet(dbCommandWrapper);
                    if (ds == null || ds.Tables[0] == null || ds.Tables[0].Rows.Count <= 0)
                    {
                        return null;
                    }
                    return DataUtil.ToList<bi_iaa_vehicle_speed>(ds.Tables[0]);
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

        /// <summary>
        /// 描  述：疲劳驾驶
        /// 作  者：黄冠群 (hgq@e-trans.com.cn)
        /// 时  间：2013年4月17日
        /// 修  改：
        /// 原  因：
        /// </summary>
        /// <returns></returns>
        public List<bi_iaa_vehicle_tired> GetANA_AnalyserTiredMap()
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
                    StringBuilder sql = new StringBuilder("");
                    //sql.Append("SELECT  ");
                    //sql.Append(" hvs.VehicleID,hvs.PlateNumber,hvs.PlateColor,bui.id as MotorcadeID,bui.UnitName as MotorcadeName,");
                    //sql.Append(" hvs.DriverICCode,hvs.DriverName,hvs.StartDatetime,hvs.EndDatetime,");
                    //sql.Append(" hvs.StartLon,hvs.StartLat,hvs.EndLon,hvs.EndLat,hvs.StartMileage,hvs.EndMileage,hvs.StartOil,hvs.EndOil,hvs.AlarmNum,");
                    //sql.Append(" hvs.AnalyseConditions,hvs.AnalyseObject,hvs.ConditionsUserId,hvs.CreateDatetime");
                    //sql.Append(" from ");
                    //sql.Append(" BI_HTA_Vehicle_Tired as hvs INNER join");
                    //sql.Append(" etta_basedata.BI_Vehicle_Info as bvi on hvs.VehicleID=bvi.id INNER join");
                    //sql.Append(" etta_basedata.BI_Unit_info as bui  on bvi.UnitID = bui.id ");
                    //sql.Append(" where hvs.StartDatetime >= @StartDatetime and hvs.EndDatetime <= @EndDatetime");

                    sql.Append("SELECT  ");
                    sql.Append(" hvs.VehicleID,hvs.PlateNumber,hvs.PlateColor,");
                    sql.Append(" hvs.DriverICCode,hvs.DriverName,hvs.StartDatetime,hvs.EndDatetime,");
                    sql.Append(" hvs.StartLon,hvs.StartLat,hvs.EndLon,hvs.EndLat,hvs.StartMileage,hvs.EndMileage,hvs.StartOil,hvs.EndOil,hvs.AlarmNum,");
                    sql.Append(" hvs.AnalyseConditions,hvs.AnalyseObject,hvs.ConditionsUserId,hvs.CreateDatetime,hvs.MileageThreshold,hvs.TimeThreshold,hvs.UnitName ");
                    sql.Append(" from ");
                    sql.Append(" BI_HTA_Vehicle_Tired as hvs INNER join");
                    sql.Append(" etta_basedata.BI_Vehicle_Info as bvi on hvs.VehicleID=bvi.id");
                    sql.Append(" where hvs.StartDatetime >= @StartDatetime and hvs.EndDatetime <= @EndDatetime");

                    dbCommandWrapper.CommandText = sql.ToString();
                    #region Add parameters
                    db.AddInParameter(dbCommandWrapper, "@StartDatetime", DbType.String, beginTime);
                    db.AddInParameter(dbCommandWrapper, "@EndDatetime", DbType.String, endTime);
                    #endregion
                    ds = db.ExecuteDataSet(dbCommandWrapper);
                    if (ds == null || ds.Tables[0] == null || ds.Tables[0].Rows.Count <= 0)
                    {
                        return null;
                    }
                    return DataUtil.ToList<bi_iaa_vehicle_tired>(ds.Tables[0]);
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

        /// <summary>
        /// 描  述：进出区域
        /// 作  者：黄冠群 (hgq@e-trans.com.cn)
        /// 时  间：2013年4月17日
        /// 修  改：
        /// 原  因：
        /// </summary>
        /// <returns></returns>
        public List<bi_hta_area_inout> GetAreaInout()
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
                    StringBuilder sql = new StringBuilder("");
                    sql.Append("SELECT  ");
                    sql.Append(" t1.VehicleID,t1.PlateNumber,t1.PlateColor,t3.id as MotorcadeID,t3.UnitName as MotorcadeName,");
                    sql.Append(" t1.DriverICCode,t1.DriverName,t1.ActionState,t1.ActionDatetime,t1.CoverageID,");
                    sql.Append(" t1.ActionLon,t1.ActionLat,t1.ActionOil,t1.ActionMileage,t1.AnalyseConditions,t1.ConditionsUserId");
                    sql.Append(" from ");
                    sql.Append(" bi_hta_area_inout as t1 INNER join");
                    sql.Append(" etta_basedata.BI_Vehicle_Info as t2 on t1.VehicleID=t2.id INNER join");
                    sql.Append(" etta_basedata.BI_Unit_info as t3  on t2.UnitID = t3.id ");
                    sql.Append(" where t1.ActionDatetime >= @StartDatetime and t1.ActionDatetime <= @EndDatetime");

                    dbCommandWrapper.CommandText = sql.ToString();
                    #region Add parameters
                    db.AddInParameter(dbCommandWrapper, "@StartDatetime", DbType.String, beginTime);
                    db.AddInParameter(dbCommandWrapper, "@EndDatetime", DbType.String, endTime);
                    #endregion
                    ds = db.ExecuteDataSet(dbCommandWrapper);
                    if (ds == null || ds.Tables[0] == null || ds.Tables[0].Rows.Count <= 0)
                    {
                        return null;
                    }
                    return DataUtil.ToList<bi_hta_area_inout>(ds.Tables[0]);
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

        /// <summary>
        /// 描  述：地点进出
        /// 作  者：黄冠群 (hgq@e-trans.com.cn)
        /// 时  间：2013年4月17日
        /// 修  改：
        /// 原  因：
        /// </summary>
        /// <returns></returns>
        public List<bi_hta_locale_inout> GetLocaleInout()
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
                    StringBuilder sql = new StringBuilder("");
                    sql.Append("SELECT  ");
                    sql.Append(" t1.VehicleID,t1.PlateNumber,t1.PlateColor,t3.id as MotorcadeID,t3.UnitName as MotorcadeName,");
                    sql.Append(" t1.DriverICCode,t1.DriverName,t1.ActionState,t1.ActionDatetime,t1.CoverageID,");
                    sql.Append(" t1.ActionLon,t1.ActionLat,t1.ActionOil,t1.ActionMileage,t1.AnalyseConditions,t1.ConditionsUserId");
                    sql.Append(" from ");
                    sql.Append(" bi_hta_locale_inout as t1 INNER join");
                    sql.Append(" etta_basedata.BI_Vehicle_Info as t2 on t1.VehicleID=t2.id INNER join");
                    sql.Append(" etta_basedata.BI_Unit_info as t3  on t2.UnitID = t3.id ");
                    sql.Append(" where t1.ActionDatetime >= @StartDatetime and t1.ActionDatetime <= @EndDatetime");

                    dbCommandWrapper.CommandText = sql.ToString();
                    #region Add parameters
                    db.AddInParameter(dbCommandWrapper, "@StartDatetime", DbType.String, beginTime);
                    db.AddInParameter(dbCommandWrapper, "@EndDatetime", DbType.String, endTime);
                    #endregion
                    ds = db.ExecuteDataSet(dbCommandWrapper);
                    if (ds == null || ds.Tables[0] == null || ds.Tables[0].Rows.Count <= 0)
                    {
                        return null;
                    }
                    return DataUtil.ToList<bi_hta_locale_inout>(ds.Tables[0]);
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
        /// <summary>
        /// 描  述：地点超时
        /// 作  者：黄冠群 (hgq@e-trans.com.cn)
        /// 时  间：2013年4月17日
        /// 修  改：
        /// 原  因：
        /// </summary>
        /// <returns></returns>
        public List<bi_iaa_vehicle_overtime> GetVehicleOvertime()
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
                    string sql = @"SELECT   t1.VehicleID,t1.PlateNumber,t1.PlateColor,t3.id as MotorcadeID,t3.UnitName as MotorcadeName,
                                          t1.DriverICCode,t1.DriverName,t1.CoverageID,t1.StartDatetime,t1.EndDatetime,
					                      t1.StopLat,t1.StopLon,t1.AnalyseConditions,t1.AnalyseObject,t1.ConditionsUserId
                                  From 
                                          bi_hta_vehicle_overtime as t1 INNER join
                                          etta_basedata.BI_Vehicle_Info as t2 on t1.VehicleID=t2.id INNER join
                                          etta_basedata.BI_Unit_info as t3  on t2.UnitID = t3.id 
                                  where t1.StartDatetime >= @StartDatetime and t1.EndDatetime <= @EndDatetime";

                    dbCommandWrapper.CommandText = sql;
                    #region Add parameters
                    db.AddInParameter(dbCommandWrapper, "@StartDatetime", DbType.String, beginTime);
                    db.AddInParameter(dbCommandWrapper, "@EndDatetime", DbType.String, endTime);
                    #endregion
                    ds = db.ExecuteDataSet(dbCommandWrapper);
                    if (ds == null || ds.Tables[0] == null || ds.Tables[0].Rows.Count <= 0)
                    {
                        return null;
                    }
                    return DataUtil.ToList<bi_iaa_vehicle_overtime>(ds.Tables[0]);
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
    }
}
