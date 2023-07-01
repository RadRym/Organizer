using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerV2.TeklaModels.Views
{
    public class CustomColorBy
    {
        public static string CreateObjectGroupFile(string proWHAT, string alblWhat, string value)
        {
            string result = $"TITLE_OBJECT_GROUP\n" +
                            "{\n" +
                            "    Version= 1.05\n" +
                            "    Count= 1\n" +
                            "    SECTION_OBJECT_GROUP\n" +
                            "    {\n" +
                            "        0\n" +
                            "        1\n" +
                            "        co_part\n" +
                            $"        {proWHAT}\n" +
                            $"        {alblWhat}\n" +
                            "        ==\n" +
                            "        albl_Equals\n" +
                            $"        {value}\n" +
                            "        0\n" +
                            "        &&\n" +
                            "    }\n" +
                            "}";

            return result;
        }
        public static string ColorByProfile(string modelLocation)
        {
            List<string> profiles = ListsOfObjectGroups.profiles();
            foreach (string profile in profiles)
            {
                string objectGroupFilePath = Path.Combine(modelLocation, "attributes", "OG_" + profile.Replace('/', '_') + ".PObjGrp");
                string objectGroupContent = CustomColorBy.CreateObjectGroupFile("proPROFILE", "alblProfile", profile);
                File.WriteAllText(objectGroupFilePath, objectGroupContent);
            }

            int[][] colors = GenerateColors.GenerateRainbowColorsWithoutRed(profiles.Count);
            string middleString = string.Empty;

            string startString =
                    $"REPRESENTATIONS\n" +
                        "{\n" +
                        "Version= 1.04\n" +
                        $"Count= {profiles.Count + 1}\n";
            for (int i = 0; i < profiles.Count; i++)
            {
                middleString +=
                $"SECTION_UTILITY_LIMITS\n" +
                "{\n" +
                    "0.5\n" +
                    "0.9\n" +
                    "1\n" +
                    "1.2\n" +
                "}\n" +
                "SECTION_OBJECT_REP\n" +
                "{\n" +
                    $"OG_{profiles[i].Replace('/', '_')}\n" +
                    $"{i + 13969144}\n" +
                    "10\n" +
                "}\n" +
                "SECTION_OBJECT_REP_BY_ATTRIBUTE\n" +
                "{\n" +
                    "(SETTING\u0002NOT\u0002DEFINED)\n" +
                "}\n" +
                $"SECTION_OBJECT_REP_RGB_VALUE\n" +
                "{\n" +
                    $"{colors[i][0]}\n" +
                    $"{colors[i][1]}\n" +
                    $"{colors[i][2]}\n" +
                "}\n";
            }
            string endString =
                    @"SECTION_UTILITY_LIMITS 
                    {
                        0 
                        0 
                        0 
                        0 
                        }
                    SECTION_OBJECT_REP 
                    {
                        All 
                        1 
                        10 
                        }
                    SECTION_OBJECT_REP_BY_ATTRIBUTE 
                    {
                        (SETTINGNOTDEFINED) 
                        }
                    SECTION_OBJECT_REP_RGB_VALUE 
                    {
                        -1 
                        -1 
                        -1 
                        }
                    }
                    ";
            string contentOfRepresentationFile = startString + middleString + endString;
            return contentOfRepresentationFile;
        }
        public static string ColorByMaterial(string modelLocation)
        {
            List<string> materials = ListsOfObjectGroups.materials;
            foreach (string material in materials)
            {
                string objectGroupFilePath = Path.Combine(modelLocation, "attributes", "OG_" + material.Replace('/', '_') + ".PObjGrp");
                string objectGroupContent = CustomColorBy.CreateObjectGroupFile("proMATERIAL", "alblMaterial", material);
                File.WriteAllText(objectGroupFilePath, objectGroupContent);
            }

            int[][] colors = GenerateColors.GenerateRainbowColorsWithoutRed(materials.Count);
            string middleString = string.Empty;

            string startString =
                    $"REPRESENTATIONS\n" +
                        "{\n" +
                        "Version= 1.04\n" +
                        $"Count= {materials.Count + 1}\n";
            for (int i = 0; i < materials.Count; i++)
            {
                middleString +=
                $"SECTION_UTILITY_LIMITS\n" +
                "{\n" +
                    "0.5\n" +
                    "0.9\n" +
                    "1\n" +
                    "1.2\n" +
                "}\n" +
                "SECTION_OBJECT_REP\n" +
                "{\n" +
                    $"OG_{materials[i].Replace('/', '_')}\n" +
                    $"{i + 13969144}\n" +
                    "10\n" +
                "}\n" +
                "SECTION_OBJECT_REP_BY_ATTRIBUTE\n" +
                "{\n" +
                    "(SETTING\u0002NOT\u0002DEFINED)\n" +
                "}\n" +
                $"SECTION_OBJECT_REP_RGB_VALUE\n" +
                "{\n" +
                    $"{colors[i][0].ToString()}\n" +
                    $"{colors[i][1].ToString()}\n" +
                    $"{colors[i][2].ToString()}\n" +
                "}\n";
            }
            string endString =
                    @"SECTION_UTILITY_LIMITS 
                    {
                        0 
                        0 
                        0 
                        0 
                        }
                    SECTION_OBJECT_REP 
                    {
                        All 
                        1 
                        10 
                        }
                    SECTION_OBJECT_REP_BY_ATTRIBUTE 
                    {
                        (SETTINGNOTDEFINED) 
                        }
                    SECTION_OBJECT_REP_RGB_VALUE 
                    {
                        -1 
                        -1 
                        -1 
                        }
                    }
                    ";
            string contentOfRepresentationFile = startString + middleString + endString;
            return contentOfRepresentationFile;
        }
    }
}
