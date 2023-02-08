
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestsNet
{
    public class Prueba
    {
        [Fact]
        public void pruebaTest()
        {
          
            Assert.Equal(5, Add(2, 2));

        }
        public int Add(int x, int y)
        {
            return x + y;
        }
    }
}
