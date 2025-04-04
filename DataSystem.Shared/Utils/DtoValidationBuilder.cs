using System.Linq.Expressions;

namespace DataSystem.Shared.Utils
{
    public class DtoValidationBuilder<T>
    {
        private readonly T _instance;
        private bool _hasUpdated;

        public DtoValidationBuilder(T instance)
        {
            _instance = instance;
            _hasUpdated = false;
        }

        public DtoValidationBuilder<T> UpdateProperty<U>(Expression<Func<T, U>> propertyExpression, U newValue, Func<U, bool> validator = null)
        {
            var property = (MemberExpression)propertyExpression.Body;
            var propertyInfo = (System.Reflection.PropertyInfo)property.Member;

            var currentValue = (U)propertyInfo.GetValue(_instance);

            if (validator != null && !validator(newValue))
            {
                return this;
            }

            if (!EqualityComparer<U>.Default.Equals(newValue, currentValue))
            {
                propertyInfo.SetValue(_instance, newValue);
                _hasUpdated = true;
            }

            return this;
        }

        public DtoValidationBuilder<T> UpdateEnum<U>(Expression<Func<T, U>> propertyExpression, U newValue, Func<U, bool> validator)
        {
            return UpdateProperty(propertyExpression, newValue, validator);
        }

        public DtoValidationBuilder<T> UpdateGuid(Expression<Func<T, Guid>> propertyExpression, Guid newValue)
        {
            return UpdateProperty(propertyExpression, newValue, value => value != Guid.Empty);
        }

        public bool VerifyIfHasAnyChange()
        {
            return _hasUpdated;
        }
    }
}
