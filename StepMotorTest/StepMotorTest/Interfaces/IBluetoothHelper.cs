using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using StepMotorTest.Models;

namespace StepMotorTest.Interfaces
{
    public interface IBluetoothHelper
    {
        public List<MyBluetoothDevice> GetPairedDevices();
        public Task<bool> Connect(string name);
    }
}
