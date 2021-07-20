using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Dksh.ePOD.Entities;

namespace Dksh.ePOD.Data
{
    public interface IAuditTrailRepository: IDisposable
    {
        IEnumerable<AuditTrailBO> GetAuditTrails();
        AuditTrailBO GetAuditTrailById(long id);
        void InsertAuditTrail(AuditTrailBO bo);
        void DeleteAuditTrail(long id);
        void UpdateAuditTrail(AuditTrailBO bo);
        void Save();
    }

    public class AuditTrailRepository : IAuditTrailRepository, IDisposable
    {
        private DataContext _context;
        private bool _disposed = false;

        public void DeleteAuditTrail(DataContext ctx)
        {
            _context = ctx;
        }

        public void DeleteAuditTrail(long id)
        {
            AuditTrailBO bo = _context.AuditTrail.Find(id);
            _context.AuditTrail.Remove(bo);
        }

        public AuditTrailBO GetAuditTrailById(long id)
        {
            return _context.AuditTrail.Find(id);
        }

        public IEnumerable<AuditTrailBO> GetAuditTrails()
        {
            return _context.AuditTrail.ToList();
        }

        public void InsertAuditTrail(AuditTrailBO bo)
        {
            _context.AuditTrail.Add(bo);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateAuditTrail(AuditTrailBO bo)
        {
            _context.Entry(bo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
