using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Entities;
using System.Data;
using System.Data.SqlClient;
using Util;

namespace Data.Database
{
    public class CursoAdapter: Adapter
    {
        public List<Curso> GetAll()
        {
            List<Curso> cursos = new List<Curso>();

            try
            {
                this.OpenConnection();

                SqlCommand cmdCursos = new SqlCommand("SELECT c.id_curso as idcurso, c.id_materia as idmat, c.id_comision as idcom, c.anio_calendario as anio, c.cupo as cupo, c.desc_curso as curso, com.desc_comision as desc_comi, mat.desc_materia as desc_mati"+
                    " FROM tp2_net.dbo.cursos c INNER JOIN tp2_net.dbo.comisiones com ON com.id_comision = c.id_comision INNER JOIN tp2_net.dbo.materias mat ON mat.id_materia = c.id_materia", sqlConn);

                SqlDataReader drCursos = cmdCursos.ExecuteReader();

                while (drCursos.Read())
                {
                    Curso cur = new Curso();

                    cur.ID = (int)drCursos["idcurso"];
                    cur.IDMateria = (int)drCursos["idmat"];
                    cur.IDComision = (int)drCursos["idcom"];
                    cur.AnioCalendario = (int)drCursos["anio"];
                    cur.Cupo = (int)drCursos["cupo"];
                    cur.Descripcion = (string)drCursos["curso"];
                    cur.Desc_Comision = (string)drCursos["desc_comi"];
                    cur.Desc_Materia = (string)drCursos["desc_mati"];
                    cursos.Add(cur);
                }

                drCursos.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar lista de cursos", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

            return cursos;
        }

        public Curso GetOne(int ID)
        {
            Curso cur = new Curso();

            try
            {
                this.OpenConnection();

                SqlCommand cmdCurso = new SqlCommand("SELECT * FROM cursos WHERE id_curso=@id", sqlConn);
                cmdCurso.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                SqlDataReader drCursos = cmdCurso.ExecuteReader();

                if (drCursos.Read())
                {
                    cur.ID = (int)drCursos["id_curso"];
                    cur.IDMateria = (int)drCursos["id_materia"];
                    cur.IDComision = (int)drCursos["id_comision"];
                    cur.AnioCalendario = (int)drCursos["anio_calendario"];
                    cur.Descripcion = (string)drCursos["desc_curso"];
                    cur.Cupo = (int)drCursos["cupo"];
                }

                drCursos.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar datos del curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

            return cur;
        }

        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdDelete = new SqlCommand("DELETE cursos WHERE id_curso=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteReader();
            }
            catch (Exception Ex)
            {
                ErrorEliminar miExp = new ErrorEliminar("No se puede eliminar el curso.", Ex);
                throw miExp;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        protected void Insert(Curso curso)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("INSERT INTO cursos(id_materia,id_comision,anio_calendario,cupo,desc_curso) " +
                    "VALUES(@id_materia,@id_comision,@anio_calendario,@cupo,@desc_curso) " +
                    "SELECT @@identity", //esta linea es para recuperar el ID que asignó el SQL automaticamente
                    sqlConn);

                cmdSave.Parameters.Add("@id_materia", SqlDbType.Int).Value = curso.IDMateria;
                cmdSave.Parameters.Add("@id_comision", SqlDbType.Int).Value = curso.IDComision;
                cmdSave.Parameters.Add("@anio_calendario", SqlDbType.Int).Value = curso.AnioCalendario;
                cmdSave.Parameters.Add("@cupo", SqlDbType.Int).Value = curso.Cupo;
                cmdSave.Parameters.Add("@desc_curso", SqlDbType.VarChar, 50).Value = curso.Descripcion;
                curso.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar()); // asi se obtiene el ID que asigno al BD automaticamente
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                Exception ExcepcionManejada = new Exception("Error al crear curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        protected void Update(Curso curso)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("UPDATE cursos SET id_materia=@id_materia, id_comision=@id_comision, " +
                    "anio_calendario=@anio_calendario, cupo=@cupo, desc_curso=@desc_curso " + "WHERE id_curso=@id_curso", sqlConn);

                cmdSave.Parameters.Add("@id_curso", SqlDbType.Int).Value = curso.ID;
                cmdSave.Parameters.Add("@id_materia", SqlDbType.Int).Value = curso.IDMateria;
                cmdSave.Parameters.Add("@id_comision", SqlDbType.Int).Value = curso.IDComision;
                cmdSave.Parameters.Add("@anio_calendario", SqlDbType.Int).Value = curso.AnioCalendario;
                cmdSave.Parameters.Add("@cupo", SqlDbType.Int).Value = curso.Cupo;
                cmdSave.Parameters.Add("@desc_curso", SqlDbType.VarChar, 50).Value = curso.Descripcion;
                cmdSave.ExecuteReader();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar datos del curso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }
        
        public void Save(Curso curso)
        {
            if (curso.State == BusinessEntity.States.Deleted)
            {
                this.Delete(curso.ID);
            }
            else if (curso.State == BusinessEntity.States.New)
            {
                this.Insert(curso);
            }
            else if (curso.State == BusinessEntity.States.Modified)
            {
                this.Update(curso);
            }
            curso.State = BusinessEntity.States.Unmodified;
        }
             
        public List<Curso> GetAllXPlan(int id)
        {
            List<Curso> cursos = new List<Curso>();

            try
            {
                this.OpenConnection();

                SqlCommand cmdCursos = new SqlCommand("SELECT c.id_curso as idcurso, c.id_materia as idmat, c.id_comision as idcom, c.anio_calendario as anio, c.cupo as cupo, c.desc_curso as curso, com.desc_comision as desc_comi, mat.desc_materia as desc_mati" +
                    " FROM tp2_net.dbo.cursos c INNER JOIN tp2_net.dbo.comisiones com ON com.id_comision = c.id_comision INNER JOIN tp2_net.dbo.materias mat ON mat.id_materia = c.id_materia WHERE mat.id_plan=@id", sqlConn);
                cmdCursos.Parameters.Add("@id", SqlDbType.Int).Value = id;

                SqlDataReader drCursos = cmdCursos.ExecuteReader();

                while (drCursos.Read())
                {
                    Curso cur = new Curso();

                    cur.ID = (int)drCursos["idcurso"];
                    cur.IDMateria = (int)drCursos["idmat"];
                    cur.IDComision = (int)drCursos["idcom"];
                    cur.AnioCalendario = (int)drCursos["anio"];
                    cur.Cupo = (int)drCursos["cupo"];
                    cur.Descripcion = (string)drCursos["curso"];
                    cur.Desc_Comision = (string)drCursos["desc_comi"];
                    cur.Desc_Materia = (string)drCursos["desc_mati"];
                    cursos.Add(cur);
                }

                drCursos.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar lista de cursos", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

            return cursos;
        }

        public List<ReporteCursos> reporteCurso()
        {
            List<ReporteCursos> reportesCurso = new List<ReporteCursos>();
            try
            {
                this.OpenConnection();
                SqlCommand cmdCurso = new SqlCommand("SELECT c.cupo,c.anio_calendario,m.desc_materia,m.hs_semanales,m.hs_totales,com.desc_comision,p.desc_plan,e.desc_especialidad FROM cursos c INNER JOIN comisiones com ON com.id_comision=c.id_comision INNER JOIN materias m ON m.id_materia=c.id_materia " +
                "INNER JOIN planes p ON p.id_plan=m.id_plan INNER JOIN especialidades e ON e.id_especialidad=p.id_especialidad ", sqlConn);

                SqlDataReader drCurso = cmdCurso.ExecuteReader();
                while (drCurso.Read())
                {
                    ReporteCursos cso = new ReporteCursos();
                    cso.año = (int)drCurso["anio_calendario"];
                    cso.comision = (String)drCurso["desc_comision"];
                    cso.especialidad = (String)drCurso["desc_especialidad"];
                    cso.hsSemanales = (int)drCurso["hs_semanales"];
                    cso.hsTotales = (int)drCurso["hs_totales"];
                    cso.materia = (String)drCurso["desc_materia"];
                    cso.plan = (String)drCurso["desc_plan"];
                    reportesCurso.Add(cso);
                }
                drCurso.Close();
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de cursos", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return reportesCurso;
        }
    }
}
