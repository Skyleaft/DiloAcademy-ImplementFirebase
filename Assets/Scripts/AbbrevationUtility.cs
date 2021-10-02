using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public static class AbbrevationUtility
{
    private static readonly SortedDictionary<long, string> abbrevations = new SortedDictionary<long, string>
     {
         {1000,"K"},
         {1000000, "M" },
         {1000000000, "B" },
         {1000000000000,"T"}
     };

    public static string AbbreviateNumber(double number)
    {
        //loop setiap karakter yang terdapat pada variabel abrevations
        for (int i = abbrevations.Count - 1; i >= 0; i--)
        {
            //bandingkan apakah nomor >= abbrevations
            KeyValuePair<long, string> pair = abbrevations.ElementAt(i);
            if (Mathf.Abs(((float)number)) >= pair.Key)
            {
                //bagi nomor dengan variable pembanding lalu dibulatkan 3 digit setelah koma
                double roundedNumber = System.Math.Round(number / pair.Key, 3);
                return roundedNumber.ToString() + pair.Value;
            }
        }
        //jika nomor masih kurang dari 1000 tampilkan
        return number.ToString("0");
    }
}