#pragma checksum "D:\CPA Level 3\PROG2230-MSWebTech\Exam\final\Consulting\Consulting\Consulting\Views\HKWorkSessions\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "36b4a7013d107690ccefb4826cbca403bff38933"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_HKWorkSessions_Details), @"mvc.1.0.view", @"/Views/HKWorkSessions/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/HKWorkSessions/Details.cshtml", typeof(AspNetCore.Views_HKWorkSessions_Details))]
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
#line 1 "D:\CPA Level 3\PROG2230-MSWebTech\Exam\final\Consulting\Consulting\Consulting\Views\_ViewImports.cshtml"
using Consulting;

#line default
#line hidden
#line 2 "D:\CPA Level 3\PROG2230-MSWebTech\Exam\final\Consulting\Consulting\Consulting\Views\_ViewImports.cshtml"
using Consulting.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"36b4a7013d107690ccefb4826cbca403bff38933", @"/Views/HKWorkSessions/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4a3c487bc16d3646c319e259c5b539202ded5e4b", @"/Views/_ViewImports.cshtml")]
    public class Views_HKWorkSessions_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Consulting.Models.WorkSession>
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
            BeginContext(38, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "D:\CPA Level 3\PROG2230-MSWebTech\Exam\final\Consulting\Consulting\Consulting\Views\HKWorkSessions\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
            BeginContext(83, 134, true);
            WriteLiteral("\r\n<h1>Details</h1>\r\n\r\n<div>\r\n    <h4>WorkSession</h4>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class = \"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(218, 46, false);
#line 14 "D:\CPA Level 3\PROG2230-MSWebTech\Exam\final\Consulting\Consulting\Consulting\Views\HKWorkSessions\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.DateWorked));

#line default
#line hidden
            EndContext();
            BeginContext(264, 63, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(328, 42, false);
#line 17 "D:\CPA Level 3\PROG2230-MSWebTech\Exam\final\Consulting\Consulting\Consulting\Views\HKWorkSessions\Details.cshtml"
       Write(Html.DisplayFor(model => model.DateWorked));

#line default
#line hidden
            EndContext();
            BeginContext(370, 62, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(433, 47, false);
#line 20 "D:\CPA Level 3\PROG2230-MSWebTech\Exam\final\Consulting\Consulting\Consulting\Views\HKWorkSessions\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.HoursWorked));

#line default
#line hidden
            EndContext();
            BeginContext(480, 63, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(544, 43, false);
#line 23 "D:\CPA Level 3\PROG2230-MSWebTech\Exam\final\Consulting\Consulting\Consulting\Views\HKWorkSessions\Details.cshtml"
       Write(Html.DisplayFor(model => model.HoursWorked));

#line default
#line hidden
            EndContext();
            BeginContext(587, 62, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(650, 51, false);
#line 26 "D:\CPA Level 3\PROG2230-MSWebTech\Exam\final\Consulting\Consulting\Consulting\Views\HKWorkSessions\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.WorkDescription));

#line default
#line hidden
            EndContext();
            BeginContext(701, 63, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(765, 47, false);
#line 29 "D:\CPA Level 3\PROG2230-MSWebTech\Exam\final\Consulting\Consulting\Consulting\Views\HKWorkSessions\Details.cshtml"
       Write(Html.DisplayFor(model => model.WorkDescription));

#line default
#line hidden
            EndContext();
            BeginContext(812, 62, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(875, 46, false);
#line 32 "D:\CPA Level 3\PROG2230-MSWebTech\Exam\final\Consulting\Consulting\Consulting\Views\HKWorkSessions\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.HourlyRate));

#line default
#line hidden
            EndContext();
            BeginContext(921, 63, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(985, 42, false);
#line 35 "D:\CPA Level 3\PROG2230-MSWebTech\Exam\final\Consulting\Consulting\Consulting\Views\HKWorkSessions\Details.cshtml"
       Write(Html.DisplayFor(model => model.HourlyRate));

#line default
#line hidden
            EndContext();
            BeginContext(1027, 62, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(1090, 49, false);
#line 38 "D:\CPA Level 3\PROG2230-MSWebTech\Exam\final\Consulting\Consulting\Consulting\Views\HKWorkSessions\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.ProvincialTax));

#line default
#line hidden
            EndContext();
            BeginContext(1139, 63, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(1203, 45, false);
#line 41 "D:\CPA Level 3\PROG2230-MSWebTech\Exam\final\Consulting\Consulting\Consulting\Views\HKWorkSessions\Details.cshtml"
       Write(Html.DisplayFor(model => model.ProvincialTax));

#line default
#line hidden
            EndContext();
            BeginContext(1248, 62, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(1311, 56, false);
#line 44 "D:\CPA Level 3\PROG2230-MSWebTech\Exam\final\Consulting\Consulting\Consulting\Views\HKWorkSessions\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.TotalChargeBeforeTax));

#line default
#line hidden
            EndContext();
            BeginContext(1367, 63, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(1431, 52, false);
#line 47 "D:\CPA Level 3\PROG2230-MSWebTech\Exam\final\Consulting\Consulting\Consulting\Views\HKWorkSessions\Details.cshtml"
       Write(Html.DisplayFor(model => model.TotalChargeBeforeTax));

#line default
#line hidden
            EndContext();
            BeginContext(1483, 62, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(1546, 46, false);
#line 50 "D:\CPA Level 3\PROG2230-MSWebTech\Exam\final\Consulting\Consulting\Consulting\Views\HKWorkSessions\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Consultant));

#line default
#line hidden
            EndContext();
            BeginContext(1592, 63, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(1656, 52, false);
#line 53 "D:\CPA Level 3\PROG2230-MSWebTech\Exam\final\Consulting\Consulting\Consulting\Views\HKWorkSessions\Details.cshtml"
       Write(Html.DisplayFor(model => model.Consultant.FirstName));

#line default
#line hidden
            EndContext();
            BeginContext(1708, 62, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(1771, 44, false);
#line 56 "D:\CPA Level 3\PROG2230-MSWebTech\Exam\final\Consulting\Consulting\Consulting\Views\HKWorkSessions\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Contract));

#line default
#line hidden
            EndContext();
            BeginContext(1815, 63, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(1879, 45, false);
#line 59 "D:\CPA Level 3\PROG2230-MSWebTech\Exam\final\Consulting\Consulting\Consulting\Views\HKWorkSessions\Details.cshtml"
       Write(Html.DisplayFor(model => model.Contract.Name));

#line default
#line hidden
            EndContext();
            BeginContext(1924, 47, true);
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n<div>\r\n    ");
            EndContext();
            BeginContext(1971, 65, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "36b4a7013d107690ccefb4826cbca403bff3893311784", async() => {
                BeginContext(2028, 4, true);
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
#line 64 "D:\CPA Level 3\PROG2230-MSWebTech\Exam\final\Consulting\Consulting\Consulting\Views\HKWorkSessions\Details.cshtml"
                           WriteLiteral(Model.WorkSessionId);

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
            BeginContext(2036, 8, true);
            WriteLiteral(" |\r\n    ");
            EndContext();
            BeginContext(2044, 38, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "36b4a7013d107690ccefb4826cbca403bff3893314151", async() => {
                BeginContext(2066, 12, true);
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
            BeginContext(2082, 10, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Consulting.Models.WorkSession> Html { get; private set; }
    }
}
#pragma warning restore 1591
