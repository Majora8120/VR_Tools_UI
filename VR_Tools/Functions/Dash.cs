﻿using System;
using System.Net.Http;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace VR_Tools.Functions;

public static class Dash
{
    private const string OculusKillerURL = @"https://github.com/BnuuySolutions/OculusKiller/releases/download/v1.3.0/OculusDash.exe";
    private const string FullFilePath = @"C:\Program Files\Oculus\Support\oculus-dash\dash\bin\OculusDash.exe";
    private const string FilePath = @"C:\Program Files\Oculus\Support\oculus-dash\dash\bin\";
    private const string DashBackup = @"C:\Program Files\Oculus\Support\oculus-dash\dash\bin\OculusDash.exe.bak";
    private const string KillerBackup = @"C:\Program Files\Oculus\Support\oculus-dash\dash\bin\OculusDash.exe.killer";

    public static async Task<LogMessage> SwapToSteamVR()
    {
        string message = "null";
        string type = "ERROR";
        string? currentDash = GetCurrentDash();

        if (IsDashRunning() == true)
        {
            (message, type) = ("Close Oculus Dash and SteamVR!!!", "ERROR");
            return new LogMessage(message, type);
        }

        if (currentDash == "SteamVR")
        {
            (message, type) = ("Oculus Killer already installed", "ERROR");
            return new LogMessage(message, type);
        }
        if (currentDash == null)
        {
            (message, type) = ("GetCurrentDash returned null", "ERROR");
            return new LogMessage(message, type);
        }
        File.Move(FullFilePath, DashBackup);

        if (File.Exists(KillerBackup) == true)
        {
            File.Move(KillerBackup, FullFilePath);
            (message, type) = ("Oculus Killer installed", "INFO");
            return new LogMessage(message, type);
        }
        else
        {
            Stream fileStream = await GetFileStream(OculusKillerURL);
            if (fileStream != Stream.Null)
            {
                await SaveFileStream(fileStream);
                (message, type) = ("Oculus Killer installed", "INFO");
                return new LogMessage(message, type);
            }
        }
        return new LogMessage(message, type);
    }
    public static LogMessage SwapToOculusDash()
    {
        string message = "null";
        string type = "ERROR";
        string? currentDash = GetCurrentDash();

        if (IsDashRunning() == true)
        {
            (message, type) = ("Close OculusDash and SteamVR!!!", "ERROR");
            return new LogMessage(message, type);
        }

        if (currentDash == "OculusDash")
        {
            (message, type) = ("Oculus Dash already installed", "ERROR");
            return new LogMessage(message, type);
        }
        if (currentDash == null)
        {
            (message, type) = ("GetCurrentDash returned null", "ERROR");
            return new LogMessage(message, type);
        }
        File.Move(FullFilePath, KillerBackup);

        File.Move(DashBackup, FullFilePath);
        (message, type) = ("Oculus Dash restored", "INFO");
        return new LogMessage(message, type);
    }
    private static string? GetCurrentDash()
    {
        if (Directory.Exists(FilePath) == false)
        {
            Debug.WriteLine("Directory doesn't exist");
            return null;
        }
        if (File.Exists(FullFilePath) == false)
        {
            Debug.WriteLine("OculusDash.exe doesn't exist? Something is very wrong");
            return null;
        }
        FileInfo fileInfo = new FileInfo(FullFilePath);
        if (fileInfo.Length < 1000000) // Oculus Killer is less than 1MB in size. Dash is ~32MB
        {
            return("SteamVR");
        }
        else
        {
            return ("OculusDash");
        }
    }
    private static bool IsDashRunning()
    {
        Process[] process = Process.GetProcessesByName("OculusDash");
        if (process.Length != 0)
        {
            return true;
        }
        process = Process.GetProcessesByName("vrserver");
        if (process.Length != 0)
        {
            return true;
        }
        return false;
    }
    private static async Task<Stream> GetFileStream(string url)
    {
        HttpClient client = new HttpClient();
        try
        {
            Stream fileStream = await client.GetStreamAsync(url);
            return fileStream;
        }
        catch(Exception e)
        {
            Debug.WriteLine(e);
            return(Stream.Null);
        }
    }
    private static async Task SaveFileStream(Stream fileStream)
    {
        using (FileStream outputFileStream = new FileStream(FullFilePath, FileMode.CreateNew))
        {
            await fileStream.CopyToAsync(outputFileStream);
        }
    }
}