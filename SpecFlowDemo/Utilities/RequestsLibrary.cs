using System.Collections.Generic;

namespace SpecFlowDemo.Utilities
{
    public static class RequestsLibrary
    {
        public static Dictionary<string, Dictionary<string, object>> Requests = new Dictionary<string, Dictionary<string, object>>()
        {
            {
                "Create-Customer",
                new Dictionary<string, object>()
                {
                    {
                        "Valid Request",
                        new
                        {
                            Firstname = "Nemanja",
                            Lastname = "Djordjevic",
                            Email = "ne.djordjevic@levi9.com"
                        }
                    },
                    {
                        "Invalid Request",
                        new
                        {                                                  
                            Firstname = "Nemanja",
                            Lastname = "Djordjevic",
                            Email = (string)null
                        }
                    }
                }
            }
        };
    }
}
