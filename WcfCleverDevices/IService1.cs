using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfCleverDevices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        Reverse ReverseWordsBySql(string sentence);

        [OperationContract]
        int CountPrimeBySql(long topRange);

        [OperationContract]
        CommonCharacters GetCommonCharacterBySql(string a, string b);

        [OperationContract]
        CommonCharacters GetCommonCharacterByCSharp(string a, string b);

        [OperationContract]
        Reverse ReverseWordsByCSharp(string sentence);

        [OperationContract]
        int CountPrimeByCSharp(long ceilingNumber);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Reverse
    {
        [DataMember]
        public string Sentence { get; set; }

        [DataMember]
        public string ReverseWords { get; set; }
    }

    [DataContract]
    public class CommonCharacters
    {
        [DataMember]
        public string FirstString { get; set; }

        [DataMember]
        public string SecondString { get; set; }

        [DataMember]
        public string Common { get; set; }


    }
}