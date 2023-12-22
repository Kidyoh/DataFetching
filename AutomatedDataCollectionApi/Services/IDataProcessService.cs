using System.Collections.Generic;
using System.Threading.Tasks;
using AutomatedDataCollectionApi.Models;

namespace AutomatedDataCollectionApi.Services
{
    public interface IDataProcessService
    {
        List<UrlEntity> GetConfigFileEndPoints();
        Task<string> AddConfigFileEndPoints(UrlEntity urlEntity);
        Task<string> EditConfigFileEndPoints(UrlEntity urlEntity);

        List<ParsedEntity> GetParsedData();
        Task<string> DeleteConfigFileEndPoints(int id);
        Task<string> RunPythonScript();
    }
}