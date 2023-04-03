
using Domain;
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
            

            for (int i = 1; i < 1000000;i++)
            {

            }
            var a = Metrics.GetMemoryUsageMB();
            var b = Metrics.GetCPUUsagePercent();
            var c = Metrics.GetDiskUsage();

            Assert.Equal(5, Add(2, 2));

        }
        public int Add(int x, int y)
        {
            return x + y;
        }
    }
}
