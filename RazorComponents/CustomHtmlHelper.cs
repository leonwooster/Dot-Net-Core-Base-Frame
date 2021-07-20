using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Dksh.ePOD.RazorComponents
{
    public static class CustomHtmlHelper
    {
        public static IHtmlContent CountryBasedLabelFor<TModelItem, TResult>(
               this IHtmlHelper<IEnumerable<TModelItem>> htmlHelper,
               Expression<Func<TModelItem, TResult>> expression,
               string country,
               string resource,
               string tooltip,
               string extraAttributes,
               string htmlFieldName)
        {
            string name = $"{resource}_{country}";
            string v = Resources.GeneralResource.ResourceManager.GetString(name);
            string star = extraAttributes.Contains("required") ? " * " : "";

            return new HtmlString($"<label title='{tooltip}' for='{htmlFieldName}'>{v}<span class='required-field'>{star}</span></label>");
        }

        public static IHtmlContent CountryBasedLabel(
               this IHtmlHelper htmlHelper,               
               string country,
               string resource,
               string tooltip,
               string extraAttributes,
               string htmlFieldName)
        {
            string name = $"{resource}_{country}";
            string v = Resources.GeneralResource.ResourceManager.GetString(name);
            string star = extraAttributes.Contains("required") ? " * " : "";

            return new HtmlString($"<label title='{tooltip}' for='{htmlFieldName}'>{v}<span class='required-field'>{star}</span></label>");
        }
    }
}
