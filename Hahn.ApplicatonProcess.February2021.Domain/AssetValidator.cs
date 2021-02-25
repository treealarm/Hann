using FluentValidation;
using FluentValidation.Validators;
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
		public AssetValidator(IHttpClientFactory httpClientFactory)
		{
			RuleFor(x => x.AssetName).NotEmpty().MinimumLength(5);
			RuleFor(x => x.Department).IsInEnum();
			
			DateTime time_from = DateTime.UtcNow.AddYears(-1);
			RuleFor(x => x.PurchaseDate).GreaterThanOrEqualTo(time_from);
			RuleFor(x => x.EMailAdressOfDepartment).EmailAddress();

            RuleFor(x => x.CountryOfDepartment)
                .NotEmpty()
                .SetValidator(new CountryValidator(httpClientFactory));
        }
    }

    public class CountryValidator : PropertyValidator
    {
        //private readonly IHttpClientFactory httpClientFactory;
        private readonly HttpClient httpClient;

        public CountryValidator(IHttpClientFactory httpClientFactory)
            : base("{PropertyName} is not a valid country")
        {
            httpClient = httpClientFactory.CreateClient(nameof(CountryValidator)); ;

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
