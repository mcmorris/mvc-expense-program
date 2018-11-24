namespace ExpenseModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class SelfValidator
    {
        public bool IsValid => this.Validate().Any() == false;
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext = null)
        {
            return this.TryValidate(this, validationContext);
        }
        
        protected IEnumerable<ValidationResult> TryValidate(object @object, ValidationContext context)
        {
            var results = new List<ValidationResult>();
            if (context == null) { context = new ValidationContext(@object, null, null); }
            Validator.TryValidateObject(@object, context, results, true);
            return results;
        }
    }
}
