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
    public class MonthDao
    {
        #region 超速
        /// <summary>
        /// 统计当月的车辆超速月度报表
        /// </summary>
        /// <returns></returns>
        public DataTable GetMonthSpeed(int year, int month)
        {
            DateTime FirstDay = DateTime.Parse(string.Format("{0}-{1}-01 00:00:00", year, month));
            DateTime LastDay = DateTime.Parse(FirstDay.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
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
                    sql.AppendFormat(" SELECT {0}  as Year,{1} as Month,", year, month);
                    sql.Append("PlateNumber,UnitName,count(*) as OverSpeedCount,0 as SevereOverSpeedCount,sum(TotalTime) as OverTime,sum(AverageSpeed)/count(*) as AverageSpeed from bi_iaa_vehicle_speed  where StartDatetime>=@StartDatetime and StartDatetime<=@EndDatetime group by UnitName,PlateNumber ");


                    dbCommandWrapper.CommandText = sql.ToString();
                    #region Add parameters
                    db.AddInParameter(dbCommandWrapper, "@StartDatetime", DbType.String, FirstDay);
                    db.AddInParameter(dbCommandWrapper, "@EndDatetime", DbType.String, LastDay);
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

        /// <summary>
        /// 删除月度超速数据
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        public void DeleteMonthSpeed(int year, int month)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(ConnectionControl.DefaultConnectionstring);
                DbCommand deleteCommand = null;
                try
                {

                    deleteCommand = db.DbProviderFactory.CreateCommand();
                    deleteCommand.CommandType = CommandType.Text;
                    StringBuilder sql = new StringBuilder();
                    sql.Append(" delete from bi_iaa_month_speed where Year=@Year and Month=@Month ");
                    deleteCommand.CommandText = sql.ToString();
                    #region Add parameters
                    db.AddInParameter(deleteCommand, "@Year", DbType.Int32);
                    db.AddInParameter(deleteCommand, "@Month", DbType.Int32);

                    deleteCommand.Parameters["@Year"].Value = year;
                    deleteCommand.Parameters["@Month"].Value = month;


                    #endregion
                    db.ExecuteNonQuery(deleteCommand);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (deleteCommand != null)
                    {
                        deleteCommand = null;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="speedDt"></param>
        public void UpdateMonthSpeed(DataTable speedDt)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(ConnectionControl.DefaultConnectionstring);
                DataSet ds = null;
                DbCommand insertCommand = null;
                try
                {

                    insertCommand = db.DbProviderFactory.CreateCommand();
                    insertCommand.CommandType = CommandType.Text;
                    StringBuilder sql = new StringBuilder();
                    sql.Append(" insert into bi_iaa_month_speed(Year,Month,PlateNumber,UnitName,OverSpeedCount,SevereOverSpeedCount,OverTime,AverageSpeed) values (@Year,@Month,@PlateNumber,@UnitName,@OverSpeedCount,@SevereOverSpeedCount,@OverTime,@AverageSpeed) ");
                    //sql.Append("select Year,Month,PlateNumber,UnitName,OverSpeedCount,SevereOverSpeedCount,OverTime,AverageSpeed from bi_iaa_month_speed where 1=0 ");
                    insertCommand.CommandText = sql.ToString();
                    db.AddInParameter(insertCommand, "@Year", DbType.Int32);
                    db.AddInParameter(insertCommand, "@Month", DbType.Int32);
                    db.AddInParameter(insertCommand, "@PlateNumber", DbType.String, 512);
                    db.AddInParameter(insertCommand, "@UnitName", DbType.String, 50);
                    db.AddInParameter(insertCommand, "@OverSpeedCount", DbType.Int32);
                    db.AddInParameter(insertCommand, "@SevereOverSpeedCount", DbType.Int32);
                    db.AddInParameter(insertCommand, "@OverTime", DbType.Int32);
                    db.AddInParameter(insertCommand, "@AverageSpeed", DbType.Int32);
                    foreach (DataRow speedDr in speedDt.Rows)
                    {
                        insertCommand.Parameters["@Year"].Value = speedDr["Year"];
                        insertCommand.Parameters["@Month"].Value = speedDr["Month"];
                        insertCommand.Parameters["@PlateNumber"].Value = speedDr["PlateNumber"];
                        insertCommand.Parameters["@UnitName"].Value = speedDr["UnitName"];
                        insertCommand.Parameters["@OverSpeedCount"].Value = speedDr["OverSpeedCount"];
                        insertCommand.Parameters["@SevereOverSpeedCount"].Value = speedDr["SevereOverSpeedCount"];
                        insertCommand.Parameters["@OverTime"].Value = speedDr["OverTime"];
                        insertCommand.Parameters["@AverageSpeed"].Value = speedDr["AverageSpeed"];
                        db.ExecuteNonQuery(insertCommand);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    insertCommand.Dispose();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  疲劳
        /// <summary>
        /// 统计当月的车辆疲劳月度报表
        /// </summary>
        /// <returns></returns>
        public DataTable GetMonthTird(int year, int month)
        {
            DateTime FirstDay = DateTime.Parse(string.Format("{0}-{1}-01 00:00:00", year, month));
            DateTime LastDay = DateTime.Parse(FirstDay.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
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
                    sql.AppendFormat(" SELECT {0}  as Year,{1} as Month,", year, month);
                    sql.Append("PlateNumber,UnitName,count(*) as TiredCount,0 as SevereTiredCount,sum(TotalMileage) as TotalMileage,sum(TotalTime) as TotalTime from bi_iaa_vehicle_tired  where StartDatetime>=@StartDatetime and StartDatetime<=@EndDatetime group by UnitName,PlateNumber ");


                    dbCommandWrapper.CommandText = sql.ToString();
                    #region Add parameters
                    db.AddInParameter(dbCommandWrapper, "@StartDatetime", DbType.String, FirstDay);
                    db.AddInParameter(dbCommandWrapper, "@EndDatetime", DbType.String, LastDay);
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

        /// <summary>
        /// 删除月度疲劳数据
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        public void DeleteMonthTird(int year, int month)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(ConnectionControl.DefaultConnectionstring);
                DbCommand deleteCommand = null;
                try
                {

                    deleteCommand = db.DbProviderFactory.CreateCommand();
                    deleteCommand.CommandType = CommandType.Text;
                    StringBuilder sql = new StringBuilder();
                    sql.Append(" delete from bi_iaa_month_tired where Year=@Year and Month=@Month ");
                    deleteCommand.CommandText = sql.ToString();
                    #region Add parameters
                    db.AddInParameter(deleteCommand, "@Year", DbType.Int32);
                    db.AddInParameter(deleteCommand, "@Month", DbType.Int32);

                    deleteCommand.Parameters["@Year"].Value = year;
                    deleteCommand.Parameters["@Month"].Value = month;


                    #endregion
                    db.ExecuteNonQuery(deleteCommand);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (deleteCommand != null)
                    {
                        deleteCommand = null;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="speedDt"></param>
        public void UpdateMonthTird(DataTable speedDt)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(ConnectionControl.DefaultConnectionstring);
                DataSet ds = null;
                DbCommand insertCommand = null;
                try
                {

                    insertCommand = db.DbProviderFactory.CreateCommand();
                    insertCommand.CommandType = CommandType.Text;
                    StringBuilder sql = new StringBuilder();
                    sql.Append(" insert into bi_iaa_month_tired(Year,Month,PlateNumber,UnitName,TiredCount,TotalMileage,TotalTime,SevereTiredCount) values (@Year,@Month,@PlateNumber,@UnitName,@TiredCount,@TotalMileage,@TotalTime,@SevereTiredCount) ");
                    insertCommand.CommandText = sql.ToString();
                    db.AddInParameter(insertCommand, "@Year", DbType.Int32);
                    db.AddInParameter(insertCommand, "@Month", DbType.Int32);
                    db.AddInParameter(insertCommand, "@PlateNumber", DbType.String, 512);
                    db.AddInParameter(insertCommand, "@UnitName", DbType.String, 50);
                    db.AddInParameter(insertCommand, "@TiredCount", DbType.Int32);
                    db.AddInParameter(insertCommand, "@TotalMileage", DbType.Int32);
                    db.AddInParameter(insertCommand, "@TotalTime", DbType.Int32);
                    db.AddInParameter(insertCommand, "@SevereTiredCount", DbType.Int32);
                    foreach (DataRow speedDr in speedDt.Rows)
                    {
                        insertCommand.Parameters["@Year"].Value = speedDr["Year"];
                        insertCommand.Parameters["@Month"].Value = speedDr["Month"];
                        insertCommand.Parameters["@PlateNumber"].Value = speedDr["PlateNumber"];
                        insertCommand.Parameters["@UnitName"].Value = speedDr["UnitName"];
                        insertCommand.Parameters["@TiredCount"].Value = speedDr["TiredCount"];
                        insertCommand.Parameters["@TotalMileage"].Value = speedDr["TotalMileage"];
                        insertCommand.Parameters["@TotalTime"].Value = speedDr["TotalTime"];
                        insertCommand.Parameters["@SevereTiredCount"].Value = speedDr["SevereTiredCount"];
                        db.ExecuteNonQuery(insertCommand);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    insertCommand.Dispose();

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
