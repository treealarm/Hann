using System;

namespace Hahn.ApplicatonProcess.February2021.Web
{
    public class Asset
    {
        public int ID { get; set; }
        public string AssetName { get; set; }

        public enum EN_DEPARTMENT
        {
            HQ, Store1, Store2, Store3, MaintenanceStation
        }

        public EN_DEPARTMENT Department { get; set; }
        string CountryOfDepartment { get; set; }
        string EMailAdressOfDepartment { get; set; }
        DateTime PurchaseDate { get; set; } //in UTC.
        bool broken { get; set; } = false;// – false if not provided.
        
        //The WebApi accepts and returns application/json data.
        //The object and the properties should be validated by fluentValidation (nuget ) with the following rules:
        //AssetName – at least 5 Characters
        //Department – must be a valid enumvalue
        //CountryOfDepartment– must be a valid Country – therefore ask with an httpclient here https://restcountries.eu/rest/v2/… 
        //– ApiDescription: https://restcountries.eu/#api-endpoints-full-name if the country is found, the country is valid.
        //PurchaseDate - must not be older then one year.
        //EMailAdressOfDepartment – must be an valid email (only check for valid syntax *@*.[valid topleveldomain])
    }
}
