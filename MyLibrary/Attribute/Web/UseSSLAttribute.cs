﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace MyLibrary.Attribute.Web
{
    public class UseSSLAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                HttpResponseMessage msg = new HttpResponseMessage();
                msg.StatusCode = HttpStatusCode.Forbidden;
                msg.ReasonPhrase = "SSL Needed!";
                actionContext.Response = msg;
            }
            else
            {
                base.OnAuthorization(actionContext);
            }
        }
    }
}
