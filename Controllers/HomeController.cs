using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Localization;

using Dksh.ePOD.Models;
using Dksh.ePOD.Services;
using Dksh.ePOD.Entities;
using Dksh.ePOD.Helpers;
using Dksh.ePOD.Email;

using AutoMapper;

namespace Dksh.ePOD.Controllers
{
    /// <summary>
    /// A class that handles the external questionnaire events such as save, submit and retrieve.
    /// </summary>
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IExternalQuestionnaireService _service;
        private readonly ICommonDataService _commonService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IEmailConfiguration _emailConfig;
        private readonly IStringLocalizer<HomeController> _localizer;

        private readonly long _maxFileSize = 29360128;
        private readonly string[] _fileExtension = new string[] { ".jpg", ".jpeg", ".bmp", ".gif", ".png", ".txt", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pdf", ".pptx", ".zip", ".7z" };

        /// <summary>
        /// A constructor that receives multiple DI components.
        /// </summary>
        /// <param name="logger">File logger component.</param>
        /// <param name="service">Backend service component.</param>
        /// <param name="commonService">Common data service component.</param>
        /// <param name="mapper">AutoMapper component.</param>
        /// <param name="email">Email component.</param>
        /// <param name="emailConfiguration">Email configuration element.</param>
        public HomeController(ILogger<HomeController> logger,
                                IExternalQuestionnaireService service,
                                ICommonDataService commonService,
                                IMapper mapper,
                                IEmailService email,
                                IEmailConfiguration emailConfiguration,
                                IStringLocalizer<HomeController> localizer)
        {
            _logger = logger;
            _service = service;            
            _commonService = commonService;
            _mapper = mapper;
            _emailService = email;
            _emailConfig = emailConfiguration;
            _localizer = localizer;
        }

        /// <summary>
        /// The function that loads the related questionnaire based on the passed in Id.
        /// If there's no id passed in, it will redirect the user to the error page.
        /// </summary>
        /// <param name="id">Questionnaire id in database table.</param>
        /// <returns>The editable/view only form.</returns>
        public IActionResult Index(long? id)
        {
            try
            {
                SetBaseUrl();

                ViewData["Title"] = _localizer["PageTitle"];

                QFormModel m = null;

                if (id.HasValue)
                {
                    var d = _service.GetExternalQuestionnaire(id.Value);
                    m = GenerateModel(d);

                    if (m.status == Constants.RecordSentToExt)
                        m.ui_state = Constants.EditOnly;
                    else m.ui_state = Constants.ViewOnly;
                }
                else
                {
                    //treat this as invalid for now.
                    //m.ui_state = Constants.EditOnly;
                    return View("InvalidOpr");
                }

                PopulateSelectLists(m.flowcountry, ref m);

                if (m.ui_state == Constants.ViewOnly)
                    return View("~/Views/Home/View.cshtml", m);
                else                
                    return View("Index", m);                
            }
            catch (Exception ex)
            {
                TempData["Error"] = _localizer["SaveError"];
                _logger.LogError(ex, "Application Error");
                return View("Error");
            }
        }
    }
}