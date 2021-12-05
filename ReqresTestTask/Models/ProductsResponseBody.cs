﻿namespace ReqresTestTask
{
    public class ProductsResponseBody
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public Data[] data { get; set; }
        public Support support { get; set; }

        public class Support
        {
            public string url { get; set; }
            public string text { get; set; }
        }

        public class Data
        {
            public int id { get; set; }
            public string name { get; set; }
            public int year { get; set; }
            public string color { get; set; }
            public string pantone_value { get; set; }
        }
    }    
}
