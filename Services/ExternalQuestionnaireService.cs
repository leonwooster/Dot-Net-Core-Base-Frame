using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Security;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using Dksh.ePOD.Data;
using Dksh.ePOD.Entities;
using Dksh.ePOD.Configuration;

namespace Dksh.ePOD.Services
{
    /// <summary>
    /// A contract defined to manage the data for external questionnaires. The standard operations
    /// CRUD (create, read, update, delete) definition is defined in the implemenation class that inherits from this 
    /// interface.
    /// The underneath data access facility will be provided separately via dependency injection and it 
    /// should be a sperate concern from this interface.
    /// </summary>
    public interface IExternalQuestionnaireService
    {  
        /// <summary>
        /// A function that retrieves a specific questionnaire record from the database using the passed in primiary key "id".
        /// </summary>
        /// <param name="id">Primary key for a questionnaire record.</param>
        /// <returns></returns>
        ExternalQuestionnaireBO GetExternalQuestionnaire(long id);
        /// <summary>
        /// A function that retrieves a specific questionnaire record from the database using the passed in request number.
        /// </summary>
        /// <param name="requestNo">The request number or reference number.</param>
        /// <returns></returns>
        ExternalQuestionnaireBO GetExternalQuestionnaire(string requestNo);
        /// <summary>
        /// Not implemented for now.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ExternalQuestionnaireBO> GetAll();
        /// <summary>
        /// A function that saves the data posted from the UI into the database.
        /// </summary>
        /// <param name="bo">The related questionnaire data.</param>
        /// <param name="files">The file attachments. Each file in the collection will be saved as base 64 encoded data.</param>
        void Save(ExternalQuestionnaireBO bo, List<ExternalQuestionaireFileBO> files);
        /// <summary>
        /// A function that retrieves the related attachment file for a questionnaire.
        /// </summary>
        /// <param name="qid">Questionnaire id in database table.</param>
        /// <param name="field">The related attachment field name to check in the database to retrieve a matching attachment.</param>
        /// <returns></returns>
        ExternalQuestionaireFileBO GetFile(long qid, string field);
        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="id"></param>
        void Delete(long id);
        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="requestNo"></param>
        void Delete(string requestNo);
    }

    /// <summary>
    /// A class that contains the implementation of the IExternalQuestionnaireService contract.
    /// However this class has the related security concern mixed in and should be moved out to the web api
    /// gateway if possible.
    /// </summary>
    public class ExternalQuestionnaireService : ServiceBase, IExternalQuestionnaireService
    {
        private readonly ICommonDataService _commonService;        

        public ExternalQuestionnaireService(DataContext ctx, IAccessControlService accessControl, 
            IHttpContextAccessor httpContextAccessor, ICommonDataService commonService, IUsualConfig config) : 
            base(ctx, accessControl, httpContextAccessor, config)
        {
            _commonService = commonService;            
        }

        public void Delete(long id)
        {            
            throw new NotImplementedException();
        }

        public void Delete(string requestNo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExternalQuestionnaireBO> GetAll()
        {
            throw new NotImplementedException();
        }

        public ExternalQuestionnaireBO GetExternalQuestionnaire(long id)
        {            
            var r = DbContext.tbl_DKSH_TPI_EXTQNS_REQUEST.Where(e => e.id == id).AsNoTracking().FirstOrDefault();

            if (CanDo(r.external_party_id))
            {
                r.OtherCountry = GetOtherCountry(r.flowcountry, r.id);
                return r;
            }
            else throw new SecurityException("Invalid Operation.");
        }

        public ExternalQuestionnaireBO GetExternalQuestionnaire(string requestNo)
        {
            if (string.IsNullOrEmpty(requestNo))
                throw new AppException("Request Number does not contain a value.");

            throw new NotImplementedException();
        }

        public ExternalQuestionaireFileBO GetFile(long qid, string field)
        {
            if (string.IsNullOrEmpty(field))
                throw new AppException("Field value cannot be empty.");

            var r = DbContext.tbl_DKSH_TPI_EXTQNS_REQUEST.Where(e => e.id == qid).AsNoTracking().FirstOrDefault();

            if (CanDo(r.external_party_id))
            {
                var result = DbContext.tbl_DKSH_TPI_EXTQNS_REQUEST_FILES.Where(e => e.QId == qid && e.Field == field).AsNoTracking().FirstOrDefault();
                return result;
            }    
            else throw new SecurityException("Invalid Operation.");
        }

        public void Save(ExternalQuestionnaireBO bo, List<ExternalQuestionaireFileBO> files)
        {
            if (bo == null)
                throw new AppException("Unable to save");

            if (bo.id <= 0)
                throw new AppException("Inavlid record to save, please check.");

            DbContext.tbl_DKSH_TPI_EXTQNS_REQUEST.Update(bo);

            //save the country specific data
            if(bo.OtherCountry != null)
            {
                if(bo.OtherCountry.GetType() == typeof(ExternalQuestionnaireMYBO))
                {
                    ExternalQuestionnaireMYBO b = (ExternalQuestionnaireMYBO)bo.OtherCountry;
                    //check to see if it exist.
                    var exist = DbContext.tbl_DKSH_TPI_EXTQNS_REQUEST_EXT_MY.Where(e => e.Id == bo.id).AsNoTracking().FirstOrDefault();

                    if (exist == null)
                        DbContext.tbl_DKSH_TPI_EXTQNS_REQUEST_EXT_MY.Add(b);
                    else DbContext.tbl_DKSH_TPI_EXTQNS_REQUEST_EXT_MY.Update(b);
                }
            }

            var f = DbContext.tbl_DKSH_TPI_EXTQNS_REQUEST_FILES.Where(e => e.QId == bo.id).AsNoTracking().ToList();

            //remove those unneeded files.
            if (bo.basic_info_entity_type == Constants.Individual)
            {
                bo.supporting_doc_business_license = "";
                
                var found = f.Where(e => e.Field == "supporting_doc_business_license").FirstOrDefault();
                if (found != null)
                    DbContext.Remove(found);
            }
            else if(bo.basic_info_entity_type == Constants.Company)
            {
                bo.supporting_doc_identification_records = "";

                var found = f.Where(e => e.Field == "supporting_doc_identification_records").FirstOrDefault();
                if (found != null)
                    DbContext.Remove(found);
            }

            foreach (var file in files)
            {
                var exist = f.Where(e => e.Field == file.Field).FirstOrDefault();

                if(exist != null)
                {
                    if (file.RecordStatus == Constants.RecordDelete)
                    {
                        DbContext.Remove(exist);
                    }
                    else
                    {
                        exist.Data = file.Data;
                        exist.FileName = file.FileName;
                        exist.ModifyDate = DateTime.Now;
                        exist.ModyfBy = file.ModyfBy;
                        exist.QId = file.QId;
                        exist.RecordStatus = file.RecordStatus;

                        DbContext.tbl_DKSH_TPI_EXTQNS_REQUEST_FILES.Update(exist);
                    }
                }
                else
                {                    
                    DbContext.tbl_DKSH_TPI_EXTQNS_REQUEST_FILES.Update(file);
                }
            }

            DbContext.SaveChanges();

            //create audit log
            _commonService.CreateAuditTrail(new AuditTrailBO()
            {
                Action = bo.status == Constants.RecordExtSubmit ? "External Questionnaire Submitted." : "External Questionnaire Saved.",
                RequestID = bo.id,
                Comments = bo.status == Constants.RecordExtSubmit? "External Questionnaire Submitted." : "External Questionnaire Saved.",
                ActionedBy = base.WebContext.HttpContext.User.Identity.Name,
                ActionedByName = base.WebContext.HttpContext.User.Identity.Name,
                ActionedDate = DateTime.Now,
                Attachment = ""
            });
        }

        private IExtensionData GetOtherCountry(string country, long id)
        {
            if(country == Constants.Malaysia)
            {
                //assuming it's one to one relationship.
                var r = DbContext.tbl_DKSH_TPI_EXTQNS_REQUEST_EXT_MY.Where(e => e.Id == id).AsNoTracking().FirstOrDefault();
                return r;
            }
            
            return null;
        }
    }
}
