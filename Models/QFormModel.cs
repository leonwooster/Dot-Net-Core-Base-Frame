using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;

using Dksh.ePOD.Helpers.MVC;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Dksh.ePOD.Models
{
    public class QFormModelMY : QFormModel
    {
        public string entity_details_registered_name_other_language { get; set; }

        [RequiredIf(nameof(compliance_and_ethics_3), "Yes", ErrorMessage = "How often training field is required")]
        public string often_training { get; set; }

        [Required(ErrorMessage = "Company Anti-Bribery/Anti-Corruption policies is required.")]
        public string anti_bribe_policy { get; set; }

        [Required(ErrorMessage ="Anti Bribe Policy is required.")]
        public string compliance_and_ethics_6 { get; set; }
        [Required(ErrorMessage = "Business advantage question is required.")]
        public string compliance_and_ethics_7 { get; set; }
        [Required(ErrorMessage = "Company Anti-Bribery/Anti-Corruption policies adherence is required.")]
        public string compliance_and_ethics_8 { get; set; }
    }
    public class QFormModelVN: QFormModel
    {
        [RequiredIf(nameof(basic_info_entity_type), "Company", ErrorMessage = "Registered Name is required")]
        public string entity_details_registered_name_other_language { get; set; }

        [RequiredIf(nameof(compliance_and_ethics_5), "Yes", ErrorMessage = "Conflict of interest description is required.")]
        public string compliance_and_ethics_6 { get; set; }
    }
    public abstract class QFormModel
    {
        public long id { get; set; }

        public string ref_no { get; set; }

        [DataType(DataType.Date)]
        public DateTime? request_date { get; set; }

        [Required()]
        [MaxLength(250)]
        public string requestor_name { get; set; }

        [EmailAddress]
        public string requestor_email { get; set; }

        public string first_approver { get; set; }

        public string second_approver { get; set; }

        public bool create_on_behalf { get; set; }

        [Required(ErrorMessage ="Entity full name is required.")]
        public string basic_info_full_name { get; set; }

        [Required(ErrorMessage ="Please enter a value for Entity Type")]
        public string basic_info_entity_type { get; set; }
      
        [MaxLength(250)]
        [RequiredIf(nameof(basic_info_entity_type), "Company", ErrorMessage = "Company Registration No is required")]
        public string basic_info_company_registration_no { get; set; }

        [RequiredIf(nameof(basic_info_entity_type), "Individual", ErrorMessage = "Personal Identification No is required")]
        [MaxLength(250)]
        public string basic_info_identification_no { get; set; }

        public string basic_info_description_services { get; set; }

        [Required(ErrorMessage ="Type of third party category is required.")]
        public string basic_info_third_party_type_TPI_Category { get; set; }

        [Required(ErrorMessage ="Business unit is required.")]
        public string basic_info_bu { get; set; }

        [RequiredIf(nameof(basic_info_bu), "Others", ErrorMessage ="Business Unit/Function (Others) is required.")]
        [MaxLength(250)]
        public string basic_info_bu_others { get; set; }

        [Required(ErrorMessage ="Function is required")]
        public string basic_info_function { get; set; }

        [RequiredIf(nameof(basic_info_function), "Others", ErrorMessage = "Function (Others) is required.")]
        [MaxLength(250)]
        public string basic_info_function_others { get; set; }

        public string basic_info_entity_code { get; set; }

        [Required(ErrorMessage ="Reason for creation is required.")]
        public string basic_info_reason_creation { get; set; }

        [RequiredIf(nameof(basic_info_reason_creation), "Re-contract;Periodic re-check", ErrorMessage = "SAP Code is required.")]
        public string basic_info_sap_code { get; set; }

        public string basic_info_acknowledge_creation { get; set; }

        public string basic_info_acknowledge_reason { get; set; }

        [Required(ErrorMessage = "Contact person phone no is required.")]
        [RegularExpression(@"^\d+$", ErrorMessage ="Contact phone number accept numbers only.")]
        [MaxLength(20)]
        public string contact_person_telephone_no { get; set; }

        [Required(ErrorMessage = "Contact person email is required.")]
        [EmailAddress]
        public string contact_person_email { get; set; }

        [Required(ErrorMessage = "Contact person name is required.")]
        public string contact_person_name { get; set; }

        [Required(ErrorMessage ="Type of organization is required.")]
        public string third_party_organization_type { get; set; }

        [RequiredIf(nameof(third_party_organization_type), "Others (Please describe below)", ErrorMessage = "Organization Description is required")]
        public string third_party_organization_description { get; set; }

        [RequiredIf(nameof(basic_info_entity_type), "Company", ErrorMessage = "Registered Name is required")]
        public string entity_details_registered_name { get; set; }

        [RequiredIf(nameof(basic_info_entity_type), "Company", ErrorMessage = "Registered Address is required")]
        public string entity_details_registered_address { get; set; }

        [RequiredIf(nameof(basic_info_entity_type), "Company", ErrorMessage = "Business Address is required")]
        public string entity_details_business_address { get; set; }

        [DisplayFormat(DataFormatString ="{dd/MM/yyyy}")]        
        [RequiredIf(nameof(basic_info_entity_type), "Company", ErrorMessage = "Date of Incoporation is required")]
        public DateTime? entity_details_date_incorporation { get; set; }

        [RequiredIf(nameof(basic_info_entity_type), "Company", ErrorMessage = "Publicly Traded is required")]
        public string entity_details_publicly_traded { get; set; }

        [RequiredIf(nameof(entity_details_publicly_traded), "Yes", ErrorMessage = "Stock Exchange Name is required")]
        public string entity_details_stock_exchange_name { get; set; }

        [RequiredIf(nameof(basic_info_entity_type), "Company", ErrorMessage = "Subsidiary is required")]
        public string entity_details_subsidiary { get; set; }

        [RequiredIf(nameof(entity_details_subsidiary), "Yes", ErrorMessage = "Parent Company Stock Exchange Name is required")]
        public string entity_details_stock_exchange_name_parent { get; set; }

        [RequiredIf(nameof(basic_info_entity_type), "Individual", ErrorMessage = "Individual Name is required")]
        public string individual_details_name { get; set; }
        
        public string individual_details_name_other_language { get; set; }

        [RequiredIf(nameof(basic_info_entity_type), "Individual", ErrorMessage = "Permanent Address is required")]
        public string individual_details_permanent_address { get; set; }

        [RequiredIf(nameof(basic_info_entity_type), "Individual", ErrorMessage = "Temporary Address is required")]
        public string individual_details_temporary_address { get; set; }

        [DataType(DataType.Date)]
        [RequiredIf(nameof(basic_info_entity_type), "Individual", ErrorMessage = "Individual DOB is required")]
        public DateTime? individual_details_date_birth { get; set; }

        [RequiredIf(nameof(basic_info_entity_type), "Individual", ErrorMessage = "Individual Gender is required")]
        public string individual_details_gender { get; set; }

        [RequiredIf(nameof(basic_info_entity_type), "Individual", ErrorMessage = "Individual Nationality is required")]
        public string individual_details_nationality { get; set; }
        
        [Required(ErrorMessage ="Business license attachment is required.")]
        public string supporting_doc_business_license { get; set; }
        [RequiredIf(nameof(basic_info_entity_type), "Company", ErrorMessage = "Certified copy of business license/registration is required.")]
        public IFormFile businessLicenseFile { get; set; }


        public string supporting_doc_articles_association { get; set; }
        public IFormFile articleAssFile { get; set; }


        public string supporting_doc_identification_records { get; set; }

        [RequiredIf(nameof(basic_info_entity_type), "Individual", ErrorMessage = "Certified copy of identity is required.")]
        public IFormFile IdRecordFile { get; set; }

        [Required(ErrorMessage = "Owners/shareholders of the company percentage is required.")]
        public string ownership_1 { get; set; }

        [Required(ErrorMessage = "Name of parent company (Immediate) and ownership percentage is required.")]
        public string ownership_2 { get; set; }

        [Required(ErrorMessage = "Name of parent company (Ultimate) and ownership percentage is required.")]
        public string ownership_3 { get; set; }

        [Required(ErrorMessage = "Subsidiaries, Affiliates is required.")]
        public string ownership_4 { get; set; }

        [Required(ErrorMessage = "List of company directors is required.")]
        public string directors_and_management_1 { get; set; }

        [Required(ErrorMessage = "List of key management and personnel is required.")]
        public string directors_and_management_2 { get; set; }

        [Required(ErrorMessage = "Existing or former business partner is required")]
        public string business_info_1 { get; set; }

        [RequiredIf(nameof(business_info_1), "Yes", ErrorMessage = "Explanation of existing or former business relationship is required.")]
        public string business_info_2 { get; set; }

        [Required(ErrorMessage ="Annual Turn Over is required")]
        public string business_info_3 { get; set; }

        [RegularExpression(@"^[1-9]\d*(\.\d+)?$", ErrorMessage = "Invalid number Approx. annual turnover.")]
        [DefaultValue(0)]
        [Required(ErrorMessage ="Number of approximate annual turnover is required.")]
        public decimal? business_info_4 { get; set; }

        [RegularExpression(@"^[1-9]\d*(\.\d+)?$", ErrorMessage = "Invalid number of employees.")]
        [DefaultValue(0)]
        [Required(ErrorMessage = "Number of employee is required.")]
        public decimal? business_info_5 { get; set; }

        [Required(ErrorMessage = "Professional experience, expertise, qualification of the service provider is required.")]
        public string business_info_6 { get; set; }

        public string business_info_7 { get; set; }

        [Required(ErrorMessage ="Business conflict of interest is required")]
        public string conflict_of_interest_1 { get; set; }

        [RequiredIf(nameof(conflict_of_interest_1), "Yes", ErrorMessage = "Conflict of interest description is required.")]
        public string conflict_of_interest_2 { get; set; }

        [Required(ErrorMessage ="Personal conlict of interest is required")]
        public string conflict_of_interest_3 { get; set; }

        [RequiredIf(nameof(conflict_of_interest_3), "Yes", ErrorMessage = "Personal conflict of interest description is required.")]
        public string conflict_of_interest_4 { get; set; }

        [Required(ErrorMessage ="Government entities is required")]
        public string relationships_public_officials_1 { get; set; }

        [RequiredIf(nameof(relationships_public_officials_1), "Yes", ErrorMessage = "Official relationship description is required.")]
        public string relationships_public_officials_2 { get; set; }

        [Required]
        [MaxLength(50)]
        public string relationships_public_officials_3 { get; set; }

        [RequiredIf(nameof(relationships_public_officials_3), "Yes", ErrorMessage = "Director relationship description is required.")]
        public string relationships_public_officials_4 { get; set; }

        [Required(ErrorMessage ="Third party service is required.")]
        public string third_party_1 { get; set; }

        [RequiredIf(nameof(third_party_1), "Yes", ErrorMessage = "Use of third party service description is required.")]
        public string third_party_2 { get; set; }

        [RequiredIf(nameof(third_party_1), "Yes", ErrorMessage = "Percentage field is required.")]        
        [Range(0, 100)]
        public decimal? third_party_3 { get; set; }

        [RequiredIf(nameof(third_party_1), "Yes", ErrorMessage = "PercentageThird party direct interactionwith public official is required.")]        
        public string third_party_4 { get; set; }

        [RequiredIf(nameof(third_party_4), "Yes", ErrorMessage = "Third party interaction with officials description is required.")]
        public string third_party_5 { get; set; }

        [Required(ErrorMessage ="Code of conduct is required.")]
        public string compliance_and_ethics_1 { get; set; }

        [Required(ErrorMessage = "Code of ethnic is required.")]
        public string compliance_and_ethics_2 { get; set; }

        [Required(ErrorMessage = "Conflict of interest with DKSH is required.")]
        public string compliance_and_ethics_3 { get; set; }


        [Required(ErrorMessage ="Compliance committment attachment is required.")]
        public string compliance_and_ethics_4 { get; set; }
        public IFormFile ethics4File { get; set; }


        [Required(ErrorMessage = "Conflict of interest with DKSH is required.")]
        public string compliance_and_ethics_5 { get; set; }

        [Required(ErrorMessage ="Criminal investigation is required.")]
        public string actions_1 { get; set; }

        [RequiredIf(nameof(actions_1), "Yes", ErrorMessage = "Criminal investigation description is required.")]

        public string actions_2 { get; set; }

        [Required(ErrorMessage ="Professional suspension is required.")]

        public string actions_3 { get; set; }

        [RequiredIf(nameof(actions_3), "Yes", ErrorMessage = "Professional suspension description is required.")]
        public string actions_4 { get; set; }

        [Required(ErrorMessage ="Sanctions is required.")]
        public string actions_5 { get; set; }

        [RequiredIf(nameof(actions_5), "Yes", ErrorMessage = "Sanctions and penalties description is required.")]
        public string actions_6 { get; set; }

        [Required(ErrorMessage ="Regulatory supervision.")]
        public string actions_7 { get; set; }

        [RequiredIf(nameof(actions_7), "Yes", ErrorMessage = "Regulatory supervision description is required.")]
        public string actions_8 { get; set; }


        //[Required(ErrorMessage ="Completed external questionnaire attachment is required.")]
        public string self_certification_1 { get; set; }
        public IFormFile cert1File { get; set; }


        [Required(ErrorMessage ="Legal representative is required.")]
        public string self_certification_2 { get; set; }

        [Required(ErrorMessage ="Name of TPI signing the questionnaire is required.")]
        public string self_certification_3 { get; set; }

        [Required(ErrorMessage ="Title Position is required.")]
        public string self_certification_4 { get; set; }

        [RequiredIf(nameof(self_certification_2), "No", ErrorMessage = "Different person signing description is required.")]
        public string self_certification_5 { get; set; }


        [Required(ErrorMessage ="Supporting document attachment is required.")]
        public string self_certification_6 { get; set; }
        [RequiredIf(nameof(self_certification_2), "No", ErrorMessage = "Support document for different signing is required.")]
        public IFormFile cert6File { get; set; }


        
        [DataType(DataType.Date)]
        [Required(ErrorMessage ="Date of signing self-certification is required.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? self_certification_7 { get; set; }

        public string requester_confirmation { get; set; }

        public string tpi_manager_acknowledgement { get; set; }


        [DataType(DataType.Date)]
        public DateTime? year { get; set; }

        public string risk_level { get; set; }

        public string in_scope { get; set; }

        public string reason_for_not_in_scope { get; set; }

        public string TPI_manager { get; set; }

        public string compliance_manager { get; set; }

        public string flow_status { get; set; }

        public string flowauthor { get; set; }

        public string flowprocessID { get; set; }

        public string flowcancel { get; set; }

        public string flowcondition { get; set; }

        public string flowcurrent { get; set; }

        public string flowcurrentauthor { get; set; }

        public string flowinitiator { get; set; }

        public string flowpreviousauthor { get; set; }

        public string flowreader { get; set; }

        public DateTime? flowsubmitdate { get; set; }

        public string flowcountry { get; set; }

        public string flowcomment { get; set; }

        public int? RequestID { get; set; }

        public string application_no { get; set; }

        public string others_field { get; set; }

        public string websiteadress_field { get; set; }

        public string Govt_entity { get; set; }

        public string Description_Govt { get; set; }

        public string LegalTPI { get; set; }

        public string TpiManagerDropDown { get; set; }

        public string TpiManagerFile { get; set; }

        public string TpiManagerTextArea { get; set; }

        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string updated_by { get; set; }
        public DateTime updated_date { get; set; }
        public string status { get; set; }
        public string ui_state { get; set; }
        
        //during submission, user might remove non-compulsory file.
        public string fileToRemove { get; set; } = "[]";

        public SelectList entity_type_ls { get; set; }
        public SelectList entity_bu_ls { get; set; }
        public SelectList entity_function_ls { get; set; }
        public SelectList entity_tpiCategory_ls { get; set; }
        public SelectList entity_reason_for_creation_ls { get; set; }
        public SelectList tpi_manager_ls { get; set; }
        public SelectList organization_type_ls { get; set; }
        public SelectList gender_ls { get; set; }
        public SelectList currency_ls { get; set; }
        public SelectList type_of_third_party_ls { get; set; }
    }
    
    public class QFormEditParam
    {
        public string IdName { get; set; }
        public string ErrorMsg { get; set; }
        public string ExtraAttributes { get; set; }
        public object Value { get; set; }
        public string Label { get; set; }
        public string PlaceHolder { get; set; }
        public string Tooltip { get; set; }
        public string Type { get; set; } = "text";        
    }
    public class FileUploadParam
    {
        private string _country;
        private bool _isVisible = true;

        public string IdName { get; set; }
        public string ErrorMsg { get; set; }
        public string ExtraAttributes { get; set; }
        public object Value { get; set; }
        public string Label { get; set; }
        public string PlaceHolder { get; set; }
        public string Tooltip { get; set; }
        public string Type { get; set; } = "text";
        public string Url { get; set; } = "#";
        public string Field { get; set; }
        public bool IsVisible 
        {
             get
            {
                return _isVisible;
            }                
        }
        public string Country { 
            get
            {
                return _country;
            }
            set
            {
                _country = value;

                if (_country == Constants.Malaysia)
                    _isVisible = false;
            } 
        }
    }

    public class FileUploadParamCountry
    {
        public string IdName { get; set; }
        public string ErrorMsg { get; set; }
        public string ExtraAttributes { get; set; }
        public object Value { get; set; }
        public string Label { get; set; }
        public string PlaceHolder { get; set; }
        public string Tooltip { get; set; }
        public string Type { get; set; } = "text";
        public string Url { get; set; } = "#";
        public string Field { get; set; }
        public string Country { get; set; }
        public string Resource { get; set; }
    }
}
