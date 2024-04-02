using System.Text.RegularExpressions;

namespace AutoPartsForFree.Web.Infrastructure
{
    public class StringConverter
    {
        /// <summary>
        /// Конвертирует строку с числом в целочисленный тип Int32.
        /// </summary>
        /// <param name="countString">Строка с числом (может содержать спецсимволы).</param>
        /// <returns>Число типа Int32.</returns>
        /// <exception cref="FormatException"></exception>
        public static int ParseCount(string countString)
        {
            if (int.TryParse(countString, out int count))
                return count;

            // Если не удалось распарсить число,проверяем на наличие '-' в строке
            if (countString.Contains("-"))
            {
                string[] parts = countString.Split('-');
                if (parts.Length == 2 && int.TryParse(parts[1], out int max))
                    return max;
            }

            // Если '-' в строке нет, пытаемся извлечь числа из строк вида '>10', '<13'
            countString = Regex.Replace(countString, "[^0-9]", ""); // Удаление всех символов, кроме цифр
            if (int.TryParse(countString, out int single))
                return single; // Возвращаем простое число
            else
                throw new FormatException(nameof(countString)); // Если не удалось распарсить, возвращаем 0
        }

        /// <summary>
        /// Удаляет специальные символы (нециферно-буквенные) из строки и провдит символы к верхнему регистру.
        /// </summary>
        /// <param name="input">Строка, содержащая спецсимволы.</param>
        /// <returns>Строка из символов верхнего регистра, содержащая только цифры и буквы.</returns>
        public static string RemoveSpecialCharactersAndToUpper(string input)
        {
            return Regex.Replace(input, "[^0-9a-zA-Z]+", "").ToUpper();
        }
    }
}
