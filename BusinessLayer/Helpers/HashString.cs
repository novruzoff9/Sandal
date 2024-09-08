using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BusinessLayer.Helpers;

public static class HashString
{
    private static readonly string key = "buusagcoxboyukseylerbacaracag"; 
    private const string Charset = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public static string Encrypt(this string text)
    {
        var result = new StringBuilder();
        for (int i = 0; i < text.Length; i++)
        {
            // Encrypt each character based on the key and spread the result over multiple characters
            int charCode = (int)text[i] + (int)key[i % key.Length];

            // Split the charCode into two parts and map each part to a character in the Charset
            result.Append(Charset[(charCode % Charset.Length)]);
            result.Append(Charset[(charCode / Charset.Length) % Charset.Length]);
        }
        return result.ToString();
    }

    public static string Decrypt(this string encryptedText)
    {
        var result = new StringBuilder();
        for (int i = 0; i < encryptedText.Length; i += 2) // Read two characters at a time
        {
            // Reverse the encryption process
            int part1 = Charset.IndexOf(encryptedText[i]);
            int part2 = Charset.IndexOf(encryptedText[i + 1]);

            // Combine the parts to get the original character code
            int charCode = part1 + (part2 * Charset.Length);

            // Subtract the key value to get the original character
            result.Append((char)(charCode - (int)key[(i / 2) % key.Length]));
        }
        return result.ToString();
    }
}
