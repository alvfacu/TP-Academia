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
    public class ModuloUsuarioAdapter: Adapter
    {
        public List<ModuloUsuario> GetAll()
        {
            List<ModuloUsuario> modulosUsuarios = new List<ModuloUsuario>();

            try
            {
                this.OpenConnection();

                SqlCommand cmdModUsuarios = new SqlCommand("SELECT * FROM modulos_usuarios INNER JOIN usuarios ON modulos_usuarios.id_usuario = usuarios.id_usuario"+
                    " INNER JOIN modulos ON modulos.id_modulo = modulos_usuarios.id_modulo", sqlConn);

                SqlDataReader drModUsuarios = cmdModUsuarios.ExecuteReader();

                while (drModUsuarios.Read())
                {
                    ModuloUsuario modu = new ModuloUsuario();

                    modu.ID = (int)drModUsuarios["id_modulo_usuario"];
                    modu.IdModulo = (int)drModUsuarios["id_modulo"];
                    modu.IdUsuario = (int)drModUsuarios["id_usuario"];
                    modu.Modulo = (string)drModUsuarios["desc_modulo"];
                    modu.Usuario = (string)drModUsuarios["nombre_usuario"];
                    modu.PermiteAlta = (bool)drModUsuarios["alta"];
                    modu.PermiteBaja = (bool)drModUsuarios["baja"];
                    modu.PermiteModificacion = (bool)drModUsuarios["modificacion"];
                    modu.PermiteConsulta = (bool)drModUsuarios["consulta"];

                    modulosUsuarios.Add(modu);
                }

                drModUsuarios.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar lista de modulos usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

            return modulosUsuarios;
        }

        public List<ModuloUsuario> GetAll(int idUsuario)
        {
            List<ModuloUsuario> ModuloUsuarios = new List<ModuloUsuario>();
            try
            {
                this.OpenConnection();
                SqlCommand cmdModuloUsuario = new SqlCommand("SELECT * FROM modulos_usuarios WHERE id_usuario = @id_usuario", sqlConn);
                cmdModuloUsuario.Parameters.Add("@id_usuario", SqlDbType.Int).Value = idUsuario;
                SqlDataReader drModuloUsuario = cmdModuloUsuario.ExecuteReader();
                while (drModuloUsuario.Read())
                {
                    ModuloUsuario mu = new ModuloUsuario();
                    mu.ID = (int)drModuloUsuario["id_modulo_usuario"];
                    mu.IdModulo = (int)drModuloUsuario["id_modulo"];
                    mu.IdUsuario = (int)drModuloUsuario["id_usuario"];
                    mu.PermiteAlta = (bool)drModuloUsuario["alta"];
                    mu.PermiteBaja = (bool)drModuloUsuario["baja"];
                    mu.PermiteConsulta = (bool)drModuloUsuario["modificacion"];
                    mu.PermiteModificacion = (bool)drModuloUsuario["consulta"];
                    ModuloUsuarios.Add(mu);
                }
                drModuloUsuario.Close();
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar lista de ModuloUsuarios", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return ModuloUsuarios;
        }
        
        public ModuloUsuario GetOne(int ID)
        {
            ModuloUsuario modu = new ModuloUsuario();

            try
            {
                this.OpenConnection();

                SqlCommand cmdModUsuario = new SqlCommand("SELECT * FROM modulos_usuarios WHERE id_modulo_usuario=@id", sqlConn);
                cmdModUsuario.Parameters.Add("@id", SqlDbType.Int).Value = ID;

                SqlDataReader drModUsuarios = cmdModUsuario.ExecuteReader();

                if (drModUsuarios.Read())
                {
                    modu.ID = (int)drModUsuarios["id_modulo_usuario"];
                    modu.IdModulo = (int)drModUsuarios["id_modulo"];
                    modu.IdUsuario = (int)drModUsuarios["id_usuario"];
                    modu.PermiteAlta = (bool)drModUsuarios["alta"];
                    modu.PermiteBaja = (bool)drModUsuarios["baja"];
                    modu.PermiteModificacion = (bool)drModUsuarios["modificacion"];
                    modu.PermiteConsulta = (bool)drModUsuarios["consulta"];
                }

                drModUsuarios.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada =
                new Exception("Error al recuperar datos del modulo usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }

            return modu;
        }

        public ModuloUsuario GetOne(int idModulo, int idUsuario)
        {
            ModuloUsuario mu = new ModuloUsuario();
            try
            {
                this.OpenConnection();
                SqlCommand cmdModuloUsuarios = new SqlCommand("SELECT * FROM modulos_usuarios WHERE id_modulo= @id_modulo and id_usuario = @id_usuario", sqlConn);
                cmdModuloUsuarios.Parameters.Add("@id_modulo", SqlDbType.Int).Value = idModulo;
                cmdModuloUsuarios.Parameters.Add("@id_usuario", SqlDbType.Int).Value = idUsuario;
                SqlDataReader drModuloUsuario = cmdModuloUsuarios.ExecuteReader();
                if (drModuloUsuario.Read())
                {
                    mu.ID = (int)drModuloUsuario["id_modulo_usuario"];
                    mu.IdUsuario = (int)drModuloUsuario["id_modulo"];
                    mu.IdModulo = (int)drModuloUsuario["id_usuario"];
                    mu.PermiteAlta = (bool)drModuloUsuario["alta"];
                    mu.PermiteBaja = (bool)drModuloUsuario["baja"];
                    mu.PermiteConsulta = (bool)drModuloUsuario["consulta"];
                    mu.PermiteModificacion = (bool)drModuloUsuario["modificacion"];
                }
                drModuloUsuario.Close();
            }
            catch (Exception ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar datos de ModuloUsuarios", ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return mu;
        }
        
        public void Delete(int ID)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdDelete = new SqlCommand("DELETE modulos_usuarios WHERE id_modulo_usuario=@id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteReader();
            }
            catch (Exception Ex)
            {
                ErrorEliminar miExp = new ErrorEliminar("No se puede eliminar el módulo.", Ex);
                throw miExp;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        protected void Update(ModuloUsuario modusr)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("UPDATE modulos_usuarios SET id_modulo=@id_modulo, id_usuario=@id_usuario, alta=@alta, baja=@baja, modificacion=@modificacion, consulta=@consulta WHERE id_modulo_usuario=@id_modulo_usuario", sqlConn);

                cmdSave.Parameters.Add("@id_modulo_usuario", SqlDbType.Int).Value = modusr.ID;
                cmdSave.Parameters.Add("@id_modulo",SqlDbType.Int).Value = modusr.IdModulo;
                cmdSave.Parameters.Add("@id_usuario", SqlDbType.Int).Value = modusr.IdUsuario;
                cmdSave.Parameters.Add("@alta",SqlDbType.Bit).Value = modusr.PermiteAlta;
                cmdSave.Parameters.Add("@baja", SqlDbType.Bit).Value = modusr.PermiteBaja;
                cmdSave.Parameters.Add("@modificacion", SqlDbType.Bit).Value = modusr.PermiteModificacion;
                cmdSave.Parameters.Add("@consulta", SqlDbType.Bit).Value = modusr.PermiteConsulta;
                cmdSave.ExecuteReader();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al modificar datos del modulo usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        protected void Insert(ModuloUsuario modusr)
        {
            try
            {
                this.OpenConnection();

                SqlCommand cmdSave = new SqlCommand("INSERT INTO modulos_usuarios(id_modulo,id_usuario,alta,baja,modificacion,consulta) VALUES(@id_modulo,@id_usuario,@alta,@baja,@modificacion,@consulta) " +
                    "SELECT @@identity", sqlConn);

                cmdSave.Parameters.Add("@id_modulo", SqlDbType.Int).Value = modusr.IdModulo;
                cmdSave.Parameters.Add("@id_usuario", SqlDbType.Int).Value = modusr.IdUsuario;
                cmdSave.Parameters.Add("@alta", SqlDbType.Bit).Value = modusr.PermiteAlta;
                cmdSave.Parameters.Add("@baja", SqlDbType.Bit).Value = modusr.PermiteBaja;
                cmdSave.Parameters.Add("@modificacion", SqlDbType.Bit).Value = modusr.PermiteModificacion;
                cmdSave.Parameters.Add("@consulta", SqlDbType.Bit).Value = modusr.PermiteConsulta;
                modusr.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar()); // asi se obtiene el ID que asigno al BD automaticamente
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                Exception ExcepcionManejada = new Exception("Error al crear modulo usuario", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
        }

        public void Save(ModuloUsuario modusr)
        {
            if (modusr.State == BusinessEntity.States.Deleted)
            {
                this.Delete(modusr.ID);
            }
            else if (modusr.State == BusinessEntity.States.New)
            {
                this.Insert(modusr);
            }
            else if (modusr.State == BusinessEntity.States.Modified)
            {
                this.Update(modusr);
            }
            modusr.State = BusinessEntity.States.Unmodified;
        }
    }
}
