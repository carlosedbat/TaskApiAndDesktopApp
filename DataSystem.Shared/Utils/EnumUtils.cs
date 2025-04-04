namespace DataSystem.Shared.Utils
{
    public static class EnumUtils
    {
        public static bool ValidateEnum<TEnum>(TEnum value)
        {
            return Enum.IsDefined(typeof(TEnum), value);
        }

        public static TEnum Parse<TEnum>(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException($"Tipo do parametro {typeof(TEnum).Name} deve ser um enum");
            }

            try
            {
                TEnum parsedString = (TEnum)Enum.Parse(typeof(TEnum), value);

                if (!ValidateEnum(parsedString))
                {
                    throw new ArgumentException($"Valor {value} nao e um enum definido {typeof(TEnum).Name}");
                }

                return parsedString;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Falha ao converter {value} para o enum {typeof(TEnum).Name}", ex);
            }
        }
    }
}
