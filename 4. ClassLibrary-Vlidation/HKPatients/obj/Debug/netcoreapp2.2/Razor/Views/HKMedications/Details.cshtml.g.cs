#pragma checksum "D:\CPA Level 3\PROG2230-MSWebTech\Assignments\Assignment4\HKPatients\Views\HKMedications\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "05a4b1f3ade8ae16f3c774efa51514d81a440930"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_HKMedications_Details), @"mvc.1.0.view", @"/Views/HKMedications/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/HKMedications/Details.cshtml", typeof(AspNetCore.Views_HKMedications_Details))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\CPA Level 3\PROG2230-MSWebTech\Assignments\Assignment4\HKPatients\Views\_ViewImports.cshtml"
using HKPatients;

#line default
#line hidden
#line 2 "D:\CPA Level 3\PROG2230-MSWebTech\Assignments\Assignment4\HKPatients\Views\_ViewImports.cshtml"
using HKPatients.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"05a4b1f3ade8ae16f3c774efa51514d81a440930", @"/Views/HKMedications/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1494f22bd430077095430698427096aa21b7869f", @"/Views/_ViewImports.cshtml")]
    public class Views_HKMedications_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<HKPatients.Models.Medication>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(37, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
#line 4 "D:\CPA Level 3\PROG2230-MSWebTech\Assignments\Assignment4\HKPatients\Views\HKMedications\Details.cshtml"
  
    ViewData["Title"] = $"{ViewBag.MedicationTypeName}";

#line default
#line hidden
            BeginContext(106, 33, true);
            WriteLiteral("\r\n<h2>Details of Medication for \'");
            EndContext();
            BeginContext(140, 26, false);
#line 8 "D:\CPA Level 3\PROG2230-MSWebTech\Assignments\Assignment4\HKPatients\Views\HKMedications\Details.cshtml"
                          Write(ViewBag.MedicationTypeName);

#line default
#line hidden
            EndContext();
            BeginContext(166, 129, true);
            WriteLiteral("\'</h2>\r\n\r\n<div>\r\n    <h4>Medication</h4>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        \r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(296, 39, false);
#line 16 "D:\CPA Level 3\PROG2230-MSWebTech\Assignments\Assignment4\HKPatients\Views\HKMedications\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Din));

#line default
#line hidden
            EndContext();
            BeginContext(335, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(397, 35, false);
#line 19 "D:\CPA Level 3\PROG2230-MSWebTech\Assignments\Assignment4\HKPatients\Views\HKMedications\Details.cshtml"
       Write(Html.DisplayFor(model => model.Din));

#line default
#line hidden
            EndContext();
            BeginContext(432, 60, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(493, 40, false);
#line 22 "D:\CPA Level 3\PROG2230-MSWebTech\Assignments\Assignment4\HKPatients\Views\HKMedications\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(533, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(595, 36, false);
#line 25 "D:\CPA Level 3\PROG2230-MSWebTech\Assignments\Assignment4\HKPatients\Views\HKMedications\Details.cshtml"
       Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(631, 60, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(692, 41, false);
#line 28 "D:\CPA Level 3\PROG2230-MSWebTech\Assignments\Assignment4\HKPatients\Views\HKMedications\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Image));

#line default
#line hidden
            EndContext();
            BeginContext(733, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(795, 37, false);
#line 31 "D:\CPA Level 3\PROG2230-MSWebTech\Assignments\Assignment4\HKPatients\Views\HKMedications\Details.cshtml"
       Write(Html.DisplayFor(model => model.Image));

#line default
#line hidden
            EndContext();
            BeginContext(832, 60, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(893, 49, false);
#line 34 "D:\CPA Level 3\PROG2230-MSWebTech\Assignments\Assignment4\HKPatients\Views\HKMedications\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Concentration));

#line default
#line hidden
            EndContext();
            BeginContext(942, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(1004, 45, false);
#line 37 "D:\CPA Level 3\PROG2230-MSWebTech\Assignments\Assignment4\HKPatients\Views\HKMedications\Details.cshtml"
       Write(Html.DisplayFor(model => model.Concentration));

#line default
#line hidden
            EndContext();
            BeginContext(1049, 60, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(1110, 53, false);
#line 40 "D:\CPA Level 3\PROG2230-MSWebTech\Assignments\Assignment4\HKPatients\Views\HKMedications\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.ConcentrationCode));

#line default
#line hidden
            EndContext();
            BeginContext(1163, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(1225, 49, false);
#line 43 "D:\CPA Level 3\PROG2230-MSWebTech\Assignments\Assignment4\HKPatients\Views\HKMedications\Details.cshtml"
       Write(Html.DisplayFor(model => model.ConcentrationCode));

#line default
#line hidden
            EndContext();
            BeginContext(1274, 60, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(1335, 50, false);
#line 46 "D:\CPA Level 3\PROG2230-MSWebTech\Assignments\Assignment4\HKPatients\Views\HKMedications\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.DispensingCode));

#line default
#line hidden
            EndContext();
            BeginContext(1385, 61, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(1447, 46, false);
#line 49 "D:\CPA Level 3\PROG2230-MSWebTech\Assignments\Assignment4\HKPatients\Views\HKMedications\Details.cshtml"
       Write(Html.DisplayFor(model => model.DispensingCode));

#line default
#line hidden
            EndContext();
            BeginContext(1493, 47, true);
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n<div>\r\n    ");
            EndContext();
            BeginContext(1540, 55, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "05a4b1f3ade8ae16f3c774efa51514d81a44093010136", async() => {
                BeginContext(1587, 4, true);
                WriteLiteral("Edit");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 54 "D:\CPA Level 3\PROG2230-MSWebTech\Assignments\Assignment4\HKPatients\Views\HKMedications\Details.cshtml"
                           WriteLiteral(Model.Din);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1595, 8, true);
            WriteLiteral(" |\r\n    ");
            EndContext();
            BeginContext(1603, 38, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "05a4b1f3ade8ae16f3c774efa51514d81a44093012483", async() => {
                BeginContext(1625, 12, true);
                WriteLiteral("Back to List");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1641, 10, true);
            WriteLiteral("\r\n</div>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<HKPatients.Models.Medication> Html { get; private set; }
    }
}
#pragma warning restore 1591
