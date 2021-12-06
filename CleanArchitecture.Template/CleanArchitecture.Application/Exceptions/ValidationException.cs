using CleanArchitecture.Application.Resources;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Failures { get;}

        public ValidationException() : base(ExceptionMessage.ValidationFailure)
        {
            Failures = new Dictionary<string, string[]>();
        }

        public ValidationException(IList<ValidationFailure> failures) : this()
        {
            var propertyNames = failures.Select(f => f.PropertyName).Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(f => f.PropertyName == propertyName)
                    .Select(f => f.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }
    }
}
