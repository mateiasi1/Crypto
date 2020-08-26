using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace DataLayer.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        //de adaugat config reader -- facut in Registrations, e ok?
        // AppSetting - research

        private string _cachedInputBodyData = null;
        protected string InputBodyData
        {
            get
            {
                if (_cachedInputBodyData == null)
                    _cachedInputBodyData = ReadPostDataInternal(Request).Result;

                return _cachedInputBodyData;
            }
        }
        private async Task<string> ReadPostDataInternal(HttpRequest httpReq)
        {
            using (var sr = new StreamReader(httpReq.Body))
            {
                return await sr.ReadToEndAsync();
            }
        }
    }
}
