﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CGIFrontEndTest.Helpers
{
    public class AbsolutePath
    {

        public static String ApiRoot
        {
            get
            {
                return HttpContext.Current.Request.Url.AbsoluteUri + "api/";
            }
        }



    }
}