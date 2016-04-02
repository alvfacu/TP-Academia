using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Data.Database
{
    class PruebaAdapter
    {
        private SqlConnection sqlCon;

        public SqlConnection sqlConn
        {
            get { return sqlCon; }
            set { sqlCon = value; }
        }

        public void Open()
        {
            string conexion = "cadena de conexion";
            sqlConn = new SqlConnection(conexion);
            sqlConn.Open();            
        }

        public void Prueba()
        {
            Open();
            SqlCommand pruebaComando = new SqlCommand(" ",sqlConn);
            SqlDataReader drPrueba = pruebaComando.ExecuteReader();

            

            while(drPrueba.Read)
            {

            }

            sqlConn.Close();



        }
    }
}
