using System;
using System.Collections.Generic;

namespace src.Enums
{
    public class Hostility
    {
        public readonly Dictionary<string, string> EHostility;

        public Hostility()
        {
            EHostility = new Dictionary<string, string>();
            EHostility.Add("Hostile", "Hostile");
            EHostility.Add("Neutral", "Neutral");
            EHostility.Add("Friendly", "Friendly");
            EHostility.Add("Unknown", "Unknown");
        }
    }
}
