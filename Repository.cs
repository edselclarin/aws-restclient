using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace restclient
{
    [DataContract(Name = "repo")]
    public class Repository
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        /*  // The line of code above is equivalent to block of code below.
            public string Name 
            { 
                get { return this._name; }
                set { this._name = value; }
            }
            private string _name;         
        */

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "html_url")]
        public Uri GitHubHomeUrl { get; set; }

        [DataMember(Name = "homepage")]
        public Uri Homepage { get; set; }

        [DataMember(Name = "watchers")]
        public int Watchers { get; set; }

        [DataMember(Name = "pushed_at")]
        private string JsonDate { get; set; }

        // IgnoreDataMember instructs the serializer that this type should not
        // be read to or written from any JSON object.
        [IgnoreDataMember]
        public DateTime LastPush
        {
            get
            {
                return DateTime.ParseExact(
                    JsonDate, "yyyy-MM-ddTHH:mm:ssZ",
                    CultureInfo.InvariantCulture);
            }

            // No set accessor, means read-only property.
        }

        public override string ToString()
        {
            return String.Format($"{Name}{Environment.NewLine}{Description}{Environment.NewLine}{GitHubHomeUrl}{Environment.NewLine}{Homepage}{Environment.NewLine}{Watchers}{Environment.NewLine}{LastPush}{Environment.NewLine}");
        }
    }
}
