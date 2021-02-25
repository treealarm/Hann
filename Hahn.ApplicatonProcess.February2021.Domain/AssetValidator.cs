using FluentValidation;
using FluentValidation.Validators;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Domain
{
    public class AssetValidator : AbstractValidator<Asset>
	{
		public AssetValidator(IHttpClientFactory httpClientFactory, IConfiguration config)
		{
			RuleFor(x => x.AssetName).NotEmpty().MinimumLength(5).WithMessage(GetLocalizedString("at_least_5", config));
			RuleFor(x => x.Department).IsInEnum().WithMessage(GetLocalizedString("must_be_valid_enum", config)); ;
			
			DateTime time_from = DateTime.UtcNow.AddYears(-1);
			RuleFor(x => x.PurchaseDate).GreaterThanOrEqualTo(time_from).WithMessage(GetLocalizedString("must_be_valid_date", config));
			RuleFor(x => x.EMailAdressOfDepartment).EmailAddress().WithMessage(GetLocalizedString("must_be_valid_email", config)); ;

            RuleFor(x => x.CountryOfDepartment)
                .NotEmpty()
                .SetValidator(new CountryValidator(httpClientFactory, config));
        }
        public static string GetLocalizedString(string str_id, IConfiguration _config)
        {
            string culture = _config["AppOptions:Culture"];
            string str = str_id;
            try
            {
                str = _config[culture + ":" + str_id];
            }
            catch (Exception)
            { }
            return str;
        }
    }
    
    public class CountryValidator : PropertyValidator
    {
        //private readonly IHttpClientFactory httpClientFactory;
        private readonly HttpClient httpClient;
        IConfiguration _config;
        public CountryValidator(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _config = config;
            httpClient = httpClientFactory.CreateClient(nameof(CountryValidator)); ;

        }
        protected override string GetDefaultMessageTemplate()
        {
            return @"{PropertyName}" + AssetValidator.GetLocalizedString("invalid_country", _config);
        }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            var countryName = context.PropertyValue as string;

            if (string.IsNullOrEmpty(countryName))
            {
                return false;
            }

            using var response = httpClient.GetAsync($"{countryName}?fullText=true").GetAwaiter().GetResult();
            return response.IsSuccessStatusCode ? true : false;
        }


    }
}
