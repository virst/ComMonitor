using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComMonitor
{
    class ComDataSet : DataSet
    {
        public ComDataSet()
        {
            Tables.Add("Ports");
            Tables["Ports"].Columns.Add("PortName",typeof(string));
            UpdatePorts();

            Tables.Add("Encoding");
            Tables["Encoding"].Columns.Add("Encoding", typeof(string));
            Tables["Encoding"].Rows.Add()[0] = "ASCII";
            Tables["Encoding"].Rows.Add()[0] = "BigEndianUnicode";
            Tables["Encoding"].Rows.Add()[0] = "Unicode";
            Tables["Encoding"].Rows.Add()[0] = "UTF32";
            Tables["Encoding"].Rows.Add()[0] = "UTF7";
            Tables["Encoding"].Rows.Add()[0] = "UTF8";

        }

        public void UpdatePorts()
        {
            Tables["Ports"].Clear();
            foreach (var p in SerialPort.GetPortNames())
            {
                Tables["Ports"].Rows.Add()[0] = p;
            }
        }
    }
}
