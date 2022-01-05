using System;
using System.Collections.Generic;

namespace src.Enums
{
    public class Classification
    {
        public readonly Dictionary<string, Dictionary<string, string>> EClassification;

        public Classification()
        {
            EClassification = new Dictionary<string, Dictionary<string, string>>();
            // Define ELandSubclassification
            EClassification.Add("Land", new Dictionary<string, string>{
                { "Classification", "Land" },
                { "Radar", "Radar" },
                { "Tank", "Tank" },
                { "Anti-aircraft", "Anti-aircraft" }
            });

            // Define ESeaSubclassification
            EClassification.Add("Sea", new Dictionary<string, string>{
                { "Classification", "Sea" },
                { "Aircraft Carrier", "Aircraft Carrier" },
                { "Fast Attack Craft", "Fast Attack Craft" },
                { "Support", "Support" }
            });

            // Define ESubSurfaceSubclassification
            EClassification.Add("Subsurface", new Dictionary<string, string>{
                { "Classification", "Subsurface" },
                { "Submarine", "Submarine" },
                { "Sea-Mine", "Sea-Mine" },
                { "Trident", "Trident" }
            });

            // Define EAirSubclassification
            EClassification.Add("Air", new Dictionary<string, string>{
                { "Classification", "Air" },
                { "Recon", "Recon" },
                { "Fighter", "Fighter" },
                { "Bomber", "Bomber" }
            });
        }
    }
}
