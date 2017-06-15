using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using SALLab12;

namespace Lab12
{
    public class ValidacionComplex : Fragment
    {
        public ResultInfo Result { get; set; }

        public override string ToString()
        {
            return $"{Result.Status}\n{Result.FullName}\n{Result.Token}";
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RetainInstance = true;
        }
    }
}