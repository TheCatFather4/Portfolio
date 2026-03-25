namespace Portfolio.Areas.Academy.Utilities
{
    public class PasswordGenerator
    {
        public static string GeneratePassword()
        {
            int length = 0;
            string letters = "qQwWeErRtTyYuUiIoOpPaAsSdDfFgGhHjJkKlLzZxXcCvVbBnNmM";
            string numbers = "1234567890!@#$%^&*?.";
            Random lengthGen = new Random();
            Random rlg = new Random();
            Random rng = new Random();
            Random charType = new Random();

            length = lengthGen.Next(8, 13);

            char[] parts = new char[length];

            for (int i = 0; i < parts.Length; i++)
            {
                int ct = charType.Next(1, 3);

                if (ct == 1)
                {
                    int le = rlg.Next(0, 52);
                    parts[i] = letters[le];
                }
                else
                {
                    int ne = rng.Next(0, 20);
                    parts[i] = numbers[ne];
                }
            }

            string password = new string(parts);
            return password;
        }
    }
}