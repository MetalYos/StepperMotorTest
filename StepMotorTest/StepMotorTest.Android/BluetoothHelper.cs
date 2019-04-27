using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Bluetooth;

using StepMotorTest.Interfaces;
using StepMotorTest.Droid;
using StepMotorTest.Models;
using Java.Util;

[assembly: Dependency(typeof(BluetoothHelper))]
namespace StepMotorTest.Droid
{
    public class BluetoothHelper : IBluetoothHelper
    {
        private BluetoothSocket _socket = null;
        private void checkDefaultAdapter(BluetoothAdapter adapter)
        {
            if (adapter == null)
                throw new Exception("No bluetooth adapter found.");

            if (!adapter.IsEnabled)
                throw new Exception("Bluetooth adapter is not enabled.");
        }
        public async Task<bool> Connect(string name)
        {
            BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
            checkDefaultAdapter(adapter);

            BluetoothDevice device = (from bd in adapter.BondedDevices
                                      where bd.Name == name
                                      select bd).FirstOrDefault();

            if (device == null)
                throw new Exception(name + " device was not found.");

            _socket = device.CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));
            await _socket.ConnectAsync();

            return _socket.IsConnected;
        }

        public List<MyBluetoothDevice> GetPairedDevices()
        {
            BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
            checkDefaultAdapter(adapter);

            List<MyBluetoothDevice> devices = new List<MyBluetoothDevice>();
            foreach (var item in adapter.BondedDevices)
            {
                MyBluetoothDevice device = new MyBluetoothDevice();
                device.Name = item.Name;
                device.Address = item.Address;
            }

            return devices;
        }

        Task<bool> IBluetoothHelper.Connect(string name)
        {
            throw new NotImplementedException();
        }
    }
}