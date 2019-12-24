using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using URF.Core.Abstractions;
using vms.entity.Enums;
using vms.entity.models;
using vms.service.dbo;
using vms.Utility;

namespace vms.ActionFilter
{
    public class Logged : IActionFilter
    {

        private readonly Operations _operations;
        private readonly ObjectTypeEnum _objectType;
        private IAuditLogService _service { get; }
        public virtual IDictionary<string, object> ActionArguments { get; set; }

        public string previousstate { get; set; }


        private readonly IUnitOfWork _unityOfWork;
        readonly AuditLog audit = new AuditLog();
        public Logged(IAuditLogService service, IUnitOfWork unitOfWork, Operations operations, ObjectTypeEnum objectType)
        {
            _service = service;
            this._operations = operations;
            this._objectType = objectType;
            _unityOfWork = unitOfWork;

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            try
            {
                if (context.Exception == null)
                {
                    //var changedobj = JsonConvert.SerializeObject(ActionArguments.Values.FirstOrDefault());
                    var session=context.HttpContext.Session.GetComplexData<entity.viewModels.vmSession>("session");
                    var previousdata = JObject.Parse(session.PreviousData);
                    var postMethod = (int)((Operations)Enum.Parse(typeof(Operations), _operations.ToString()));
                     var primaryKey = _objectType.ToString() + "Id";
                    var Id = "";
                    var deletedPrimeryKey = 0;
                    if (postMethod != 7)
                    {
                        //JObject obj = JObject.Parse(changedobj);
                        Id = (string)previousdata[primaryKey];
                    }
                    else
                    {
                        deletedPrimeryKey = Convert.ToInt32(previousdata.ToString());
                    }

                    
                    AuditLog audit = new AuditLog
                    {
                        ObjectTypeId = (int)((ObjectTypeEnum)Enum.Parse(typeof(ObjectTypeEnum), _objectType.ToString())),
                        AuditOperationId = (int)((Operations)Enum.Parse(typeof(Operations), _operations.ToString())),
                        CreatedBy = session.UserId,
                        PrimaryKey =postMethod==7?Convert.ToInt32(deletedPrimeryKey) :Convert.ToInt32(Id),
                        CreatedTime = DateTime.Now,
                        Descriptions = session.PreviousData, // description size 
                        IsActive = true,
                        Datetime = DateTime.Now,
                        UserId = session.UserId,
                        OrganizationId = session.OrganizationId
                    };
                    _service.Insert(audit);
                    _unityOfWork.SaveChangesAsync();

                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }



        public void OnActionExecuting(ActionExecutingContext context)
        {
            previousstate = JsonConvert.SerializeObject(context.ActionArguments.Values.FirstOrDefault());
            ActionArguments = context.ActionArguments;
            //Activator.CreateInstance
        }

        private string change(string sourceJsonString, string targetJsonString)
        {
            //string sourceJsonString = "{'name':'John Doe','age':'25','hitcount':34}";
            //string targetJsonString = "{'name':'John Doe','age':'26','hitcount':30}";

            var sourceJObject = JsonConvert.DeserializeObject<JObject>(sourceJsonString);
            var targetJObject = JsonConvert.DeserializeObject<JObject>(targetJsonString);
            var item = new List<string>();

            if (!JToken.DeepEquals(sourceJObject, targetJObject))
            {
                foreach (KeyValuePair<string, JToken> sourceProperty in sourceJObject)
                {
                    JProperty targetProp = targetJObject.Property(sourceProperty.Key);

                    if (!JToken.DeepEquals(sourceProperty.Value, targetProp.Value))
                    {
                        item.Add(sourceProperty.Key + ":" + sourceProperty.Value + "->" + sourceProperty.Key + targetProp.Value);
                        Console.WriteLine(string.Format("{0} property value is changed", sourceProperty.Key));
                    }

                }
            }
            return string.Join(",", item);
        }
    }
}
