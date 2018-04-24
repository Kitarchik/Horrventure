using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace HorrventuresEconomy
{
    public class EconomyLogic : Java.Lang.Object
    {
        private double Currency;
        private Timer timer;
        public List<MyDeviceView> deviceList;
        private BluetoothDataLayer deviceData;
        private const double BEACONCOSTPERTIME = 1;


        public EconomyLogic()
        {
            deviceList = new List<MyDeviceView>();
            deviceData = new BluetoothDataLayer();

            timer = new Timer
            {
                Interval = 10000
            };
            timer.Elapsed += OnTimerTick;
            timer.Start();
        }

        private void OnTimerTick(object sender, ElapsedEventArgs e)
        {
            UpdateCurrency();
        }

        private void UpdateCurrency()
        {
            deviceList = deviceData.GetDeviceList();
            Currency += deviceList.Count*BEACONCOSTPERTIME;
        }

        public double GetCurrency()
        {
            return Currency;
        }

        public void RefreshCurrency()
        {
            Currency = 0;
        }

        public bool RetrieveMoney(int amount)
        {
            if (amount < Currency)
            {
                Currency -= amount;
                return true;
            }
            return false;
        }
    }
}