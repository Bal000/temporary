using System;
using System.Web;
using System.Web.Mvc;
using BusinessEntities.Enums;
using System.IO;
using System.Web.Script.Serialization;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace MyWebApp.Extensions.Controllers
{
    public class JsonViewResult<TModel> : ViewResult
    {
        private ViewState _viewState;
        private TModel _viewModel;
        private string _modelErrors = string.Empty;

        public JsonViewResult(string viewName, TModel viewModel, ViewState viewState)
        {
            ViewName = viewName;
            this._viewModel = viewModel;
            this._viewState = viewState;
        }

        public override void ExecuteResult(ControllerContext controllerContext)
        {
            if (controllerContext == null)
                throw new ArgumentException("Controllercontext is null");

            HttpResponseBase response = controllerContext.HttpContext.Response;
            ValidateModelState(controllerContext, ref response);

            var json = new
            {
                view = string.IsNullOrEmpty(ViewName) ? string.Empty : RenderView(controllerContext),
                viewState = this._viewState,
                success = this._viewState == ViewState.Valid ? true : false,
                statusCode = response.StatusCode,
                errors = this._modelErrors
            };
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = int.MaxValue;
            serializer.RecursionLimit = 150;
            string jsonString = serializer.Serialize(json);

            response.ContentEncoding = Encoding.UTF8;
            response.ContentType = "Application/json";
            response.Write(jsonString);

        }

        private void ValidateModelState(ControllerContext controllerContext, ref HttpResponseBase response)
        {
            var modelState = controllerContext.Controller.ViewData.ModelState;
                
            if (this._viewState == ViewState.NotSet)
            {
                this._viewState = ViewState.Invalid;
                response.StatusCode = 500;
                modelState.AddModelError("ViewStateNotSet", "ViewState not set");
            }

            if (this._viewState == ViewState.Invalid || !modelState.IsValid)
            {
                response.StatusCode = 500;
                CollectModelErrors(modelState);
            }            
        }

        private void CollectModelErrors(ModelStateDictionary modelState)
        {
            var modelErrors = modelState.Values.SelectMany(v => v.Errors)
                                                                .Select(e => e.ErrorMessage);
            foreach (var error in modelErrors)
            {
                this._modelErrors += "* " + error + "\r\n";
            }
        }

        private string RenderView(ControllerContext controllerContext)
        {
            controllerContext.Controller.ViewData.Model = this._viewModel;
            using (var sw = new StringWriter())
            {
                var ViewResult = ViewEngines.Engines.FindPartialView(controllerContext, base.ViewName);
                var ViewContext = new ViewContext(controllerContext, ViewResult.View, controllerContext.Controller.ViewData, controllerContext.Controller.TempData, sw);
                ViewResult.View.Render(ViewContext, sw);
                ViewResult.ViewEngine.ReleaseView(controllerContext, ViewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}