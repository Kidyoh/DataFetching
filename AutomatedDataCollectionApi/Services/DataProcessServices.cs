using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ProcessStartInfo = System.Diagnostics.ProcessStartInfo;
using Process = System.Diagnostics.Process;
using System.IO.Compression;
using AutomatedDataCollectionApi.Models;
using AutomatedDataCollectionApi.Data;


namespace AutomatedDataCollectionApi.Services
{
   public class DataProcessServices : IDataProcessService
    {
        private readonly IConfiguration _configuration;
        private readonly MyDbContext _context;

        public DataProcessServices(IConfiguration configuration, MyDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public List<UrlEntity> GetConfigFileEndPoints()
        {
            // Fetch endpoints from database
            var endpoints = _context.Urls.ToList();

            return endpoints;
        }

        public List<ParsedEntity> GetParsedData()
        {
            // Fetch parsed data from database
            var parsedData = _context.ParsedData.ToList();

            return parsedData;
        }

        public async Task<string> AddConfigFileEndPoints(UrlEntity urlEntity)
        {
            // Add new endpoint to database
            _context.Urls.Add(urlEntity);
            await _context.SaveChangesAsync();

            return "Endpoint added successfully";
        }

        public async Task<string> EditConfigFileEndPoints(UrlEntity urlEntity)
        {
            // Update endpoint in database
            _context.Urls.Update(urlEntity);
            await _context.SaveChangesAsync();

            return "Endpoint edited successfully";
        }

        public async Task<string> DeleteConfigFileEndPoints(int id)
        {
            // Find endpoint in database
            var urlEntity = _context.Urls.Find(id);

            // Delete endpoint from database
           if (urlEntity != null)
            {
                _context.Urls.Remove(urlEntity);
                await _context.SaveChangesAsync();
            }

            return "Endpoint deleted successfully";
        }

        public async Task<string> RunPythonScript()
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = "C:/users/HP 290 G4/AppData/Local/Programs/Python/Python312/python.exe"; // Assuming 'python' is in your PATH environment variable

                string scriptPath = "c:/Users/HP 290 G4/Documents/AutomatedDataCollectionApi/AutomatedDataCollectionApi/Automation/Finals/data_processing.py";
                start.Arguments = scriptPath;
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                start.RedirectStandardError = true;

                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    using (StreamReader errorReader = process.StandardError)
                    {
                        string result = await reader.ReadToEndAsync();
                        string error = await errorReader.ReadToEndAsync();
                        Console.WriteLine("Python Output: " + result);
                        Console.WriteLine("Python Error: " + error);
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public void CreateZipArchive(string sourceFolderPath, string zipPath)
        {
            // Make sure the source folder exists
            if (!Directory.Exists(sourceFolderPath))
            {
                throw new DirectoryNotFoundException("Source folder not found.");
            }

            // Create the zip archive
            ZipFile.CreateFromDirectory(sourceFolderPath, zipPath);
        }




    }
}

