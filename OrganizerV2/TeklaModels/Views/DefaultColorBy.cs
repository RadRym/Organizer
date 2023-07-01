using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerV2.TeklaModels.Views
{
    public class DefaultColorBy
    {
        public static string ColorByPhase()
        {
            string content = @"REPRESENTATIONS
                            {
                                Version= 1.04
                                Count= 1
                                SECTION_UTILITY_LIMITS
                                {
                                    0.5
                                    0.9
                                    1
                                    1.2
                                }
                                SECTION_OBJECT_REP
                                {
                                    All
                                    -4
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
                            }";
            return content;
        }
        public static string ColorByClass()
        {
            string content = @"REPRESENTATIONS 
                            {
                                Version= 1.04 
                                Count= 1 
                                SECTION_UTILITY_LIMITS 
                                {
                                    0.5 
                                    0.9 
                                    1 
                                    1.2 
                                    }
                                SECTION_OBJECT_REP 
                                {
                                    All 
                                    -2 
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
            return content;
        }
    }
}
