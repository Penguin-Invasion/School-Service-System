using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolServiceSystem.Utils
{
    public static class Patcher
    {
        public static void Patch<T, U>(T model, U patch)
        {
            var modelType = model.GetType();
            var patchType = patch.GetType();
            foreach (var patchProperty in patchType.GetProperties())
            {
                if (patchProperty.GetValue(patch) == null)
                {
                    continue;
                }
                var modelProperty = modelType.GetProperty(patchProperty.Name);
                modelProperty.SetValue(model, patchProperty.GetValue(patch));
            }
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
