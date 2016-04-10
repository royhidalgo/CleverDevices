using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;

namespace WcfService3.Sql
{
    /// <summary>
    /// Provides a connection that wrapper that monitors opened transactions on the connection
    /// and aotumatically uses that opened transactino on all Commands created on this connection.
    /// </summary>
    public class ConnectionWithTransactionManagement : IDbConnection
    {
        private readonly IDbConnection conn;
        private int activeTranactionCount;
        private IDbTransaction activeTransaction;

        public ConnectionWithTransactionManagement(IDbConnection conn)
        {
            this.conn = conn;
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            var transCount = Interlocked.Increment(ref activeTranactionCount);

            if (transCount > 1)
            {
                throw new Exception("Nested transactions are not supported on this connection!");
            }

            var trans = this.conn.BeginTransaction(il);
            Interlocked.Exchange<IDbTransaction>(ref activeTransaction, trans);

            return new ObserableTransaction(trans, TransactionCompleted);
        }

        private void TransactionCompleted(IDbTransaction trans)
        {
            var originalVal = Interlocked.CompareExchange<IDbTransaction>(ref activeTransaction, null, trans);
            if (originalVal == trans)
            {
                Interlocked.Decrement(ref activeTranactionCount);
            }
        }

        public IDbTransaction BeginTransaction()
        {
            return this.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void ChangeDatabase(string databaseName)
        {
            conn.ChangeDatabase(databaseName);
        }

        public void Close()
        {
            conn.Close();
        }

        public string ConnectionString
        {
            get
            {
                return conn.ConnectionString;
            }
            set
            {
                conn.ConnectionString = value;
            }
        }

        public int ConnectionTimeout
        {
            get { return conn.ConnectionTimeout; }
        }

        public IDbCommand CreateCommand()
        {
            var cmd = conn.CreateCommand();

            if (activeTransaction != null)
            {
                cmd.Transaction = activeTransaction;
            }
            return cmd;
        }

        public string Database
        {
            get { return conn.Database; }
        }

        public void Open()
        {
            conn.Open();
        }

        public ConnectionState State
        {
            get { return conn.State; }
        }

        public void Dispose()
        {
            conn.Dispose();
        }

        private class ObserableTransaction : IDbTransaction
        {
            private readonly IDbTransaction trans;
            private readonly Action<IDbTransaction> onComplete;

            public ObserableTransaction(IDbTransaction trans, Action<IDbTransaction> onComplete)
            {
                this.trans = trans;
                this.onComplete = onComplete;
            }

            public void Commit()
            {
                trans.Commit();
                onComplete(trans);
            }

            public IDbConnection Connection
            {
                get { return this.trans.Connection; }
            }

            public IsolationLevel IsolationLevel
            {
                get { return trans.IsolationLevel; }
            }

            public void Rollback()
            {
                trans.Rollback();
                onComplete(trans);
            }

            public void Dispose()
            {
                trans.Dispose();
                onComplete(trans);
            }
        }
    }
}