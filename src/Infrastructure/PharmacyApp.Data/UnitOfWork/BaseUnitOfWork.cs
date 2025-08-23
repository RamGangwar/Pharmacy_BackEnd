using PharmacyApp.Application.UnitOfWork;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace PharmacyApp.Data.UnitOfWork
{
    public class BaseUnitOfWork : IBaseUnitOfWork
    {

        private IDbConnection dbConnection;
        private IDbTransaction dbTransaction;
        private bool Disposed = false;
        public BaseUnitOfWork(IConfiguration config)
        {
            dbConnection = new SqlConnection(config["ConnectionStrings:SqlConnection"]);
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
        }
        ~BaseUnitOfWork() => Dispose(false);
        public IDbTransaction BeginDbTransaction()
        {
            if (dbTransaction == null && dbConnection.State == ConnectionState.Open)
            {
                dbTransaction = this.DbConnection.BeginTransaction();
            }
            return dbTransaction;
        }
        public IDbConnection DbConnection
        {
            get
            {

                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }
                return dbConnection;
            }
        }

        public IDbTransaction DbTransaction => dbTransaction;

        public void Commit()
        {
            try
            {
                dbTransaction?.Commit();
            }
            catch 
            {
                if (dbTransaction?.Connection != null)
                    dbTransaction?.Rollback();
                throw;
            }
            finally
            {
                dbTransaction?.Dispose();
                dbTransaction = null;

            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Rollback()
        {
            this.dbTransaction?.Rollback();
            dbTransaction?.Dispose();
            dbTransaction = null;
        }
        protected virtual void Dispose(bool disposing)
        {

            if (Disposed)
            {
                return;
            }
            if (disposing)
            {
                // disponsed managed object
                CloseConnection();
            }
            // Disponse unmanaged objects 

            dbTransaction = null;
            dbConnection = null;
            Disposed = true;
        }
        private void CloseConnection()
        {
            dbTransaction?.Dispose();
            if (this.DbConnection != null)
            {
                if (this.DbConnection.State == ConnectionState.Open)
                {
                    this.DbConnection.Close();
                }
            }
            DbConnection?.Dispose();

        }
    }
}
