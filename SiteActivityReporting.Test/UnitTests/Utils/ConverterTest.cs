using SiteActivityReporting.API.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteActivityReporting.Test.UnitTests.Utils
{
    public class ConverterTest
    {
        
        [Fact]
        public void ConvertString_Invalid()
        {
            int result = Converter.StringToNearestInt("test");

            Assert.Equal(result, -1);
        }

        [Fact]
        public void ConvertString_Valid1()
        {
            int result = Converter.StringToNearestInt("3.1");

            Assert.Equal(3, result);
        }

        [Fact]
        public void ConvertString_Valid2()
        {
            int result = Converter.StringToNearestInt("3.9");

            Assert.Equal(4, result);
        }

    }
}
