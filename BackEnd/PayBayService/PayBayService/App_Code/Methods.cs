using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PayBayService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;

namespace PayBayService.App_Code
{
    public class Methods
    {
        static SqlCommand cmd;
        static SqlConnection cnn = new SqlConnection(PayBayDatabaseEntities.connectionString);
        static SqlDataAdapter da;
        public static string err = ""; 

        /// <summary>
        /// Return a json object
        /// </summary>
        /// <param name="code">ErrCode</param>
        /// <param name="message">ErrMsg</param>
        /// <returns></returns>
        public static JObject CustomResponseMessage(int code, string message)
        {
            JObject response = new JObject();
            response.Add("ErrCode", code);
            response.Add("ErrMsg", message);

            return response;
        }

        /// <summary>
        /// Execute Non Query with no data
        /// </summary>
        /// <param name="sql">String query or procedure name</param>
        /// <param name="ct">Text or Store Procedure</param>
        /// <param name="err">Error</param>
        /// <param name="param">Parameters</param>
        /// <returns></returns>
        public static bool ExecNonQuery(string sql, CommandType ct, ref string err, params SqlParameter[] param)
        {
            bool result = false;
            try
            {
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                cmd.CommandType = ct;
                cmd.CommandTimeout = 6000;
                cmd.Parameters.Clear();
                if (param != null)
                {
                    foreach (SqlParameter p in param)
                    {
                        cmd.Parameters.Add(p);
                    }
                }

                cmd.ExecuteNonQuery();

                result = true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                result = false;
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }

        /// <summary>
        /// Excute query with data from database
        /// </summary>
        /// <param name="sql">String query or procedure name</param>
        /// <param name="ct">Text or Store Procedure</param>
        /// <param name="err">Error</param>
        /// <param name="param">Parameters</param>
        /// <returns></returns>
        public static JArray ExecQueryWithResult(string sql, CommandType ct, ref string err, params SqlParameter[] param)
        {
            DataTable dt = new DataTable();
            JArray result = new JArray();
            string json = "";
            try
            {
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                cmd.CommandType = ct;
                cmd.CommandTimeout = 6000;
                cmd.Parameters.Clear();
                if (param != null)
                {
                    foreach (SqlParameter p in param)
                        cmd.Parameters.Add(p);
                }

                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                json = JsonConvert.SerializeObject(dt);                
                result = JArray.Parse(json);
            }
            catch (SqlException ex)
            {
                JObject message = CustomResponseMessage(0, ex.Message.ToString());
                result.Add(message);
                err = ex.Message;
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }
               
    }
}