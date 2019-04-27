using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Foundation;
using UIKit;

using StepMotorTest.Interfaces;
using StepMotorTest.iOS;
using StepMotorTest.Models;

[assembly: Dependency(typeof(BluetoothHelper))]
namespace StepMotorTest.iOS
{
    public class BluetoothHelper : IBluetoothHelper
    {
        public bool Connect(string name)
        {
            throw new NotImplementedException();
        }

        public List<MyBluetoothDevice> GetPairedDevices()
        {
            throw new NotImplementedException();
        }
    }
}