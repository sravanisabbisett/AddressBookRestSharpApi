using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBookRestSharpAPI
{
    public class Person
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int zip { get; set; }
        public int mobileNumber { get; set; }
        public int id { get; set; }
    }
}
