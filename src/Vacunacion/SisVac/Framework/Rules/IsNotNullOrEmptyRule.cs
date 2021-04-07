using Plugin.ValidationRules.Interfaces;

namespace SisVac.Helpers.Rules
{
    public class IsNotNullOrEmptyRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }

        public bool Check(string value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;
            return !string.IsNullOrWhiteSpace(str);
        }
    }

}
