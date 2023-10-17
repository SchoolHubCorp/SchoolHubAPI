using System;
using System.Net.WebSockets;

namespace SchoolHubApi.Helpers;

public static class AccessCodeGenerator
{
    private static Random random = new Random();
    private const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"; 

    public static string GenerateAccessCode(int codeLength = 6)
    {
        char[] code = new char[codeLength];

        for (int i = 0; i < codeLength; i++)
        {
            code[i] = characters[random.Next(characters.Length)];
        }

        return new string(code);
    }

    public static string GenerateAccessCode(Func<string, bool> codeExist, int codeLength = 6)
    {
        var code = GenerateAccessCode(codeLength);

        while(codeExist(code))
        {
            code = GenerateAccessCode(codeLength);
        }

        return code;
    }
}
