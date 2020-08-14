using System;
using System.Collections.Generic;
using System.Text;

namespace NextechNewsWebApp.DAL.Service
{
    public class DataSeviceException : Exception
    {
        public DataSeviceException(string errorMessage) : base(errorMessage)
        {

        }
    }
}
