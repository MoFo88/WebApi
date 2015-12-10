using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly ILogger logger;
        private readonly IRepository repository;

        public ValuesController(ILogger logger, IRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
            logger.Log("ValuesController created");
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            logger.Log("Get() invoked");
            return repository.GetAllValues();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }

    public interface IRepository
    {
        IEnumerable<string> GetAllValues();
    }

    public interface ILogger
    {
        void Log(string logMessage);
    }

    public class Logger : ILogger
    {
        private string logFile;
        public Logger()
        {
            logFile = HostingEnvironment.MapPath("~/log.txt");
            File.WriteAllText(logFile, "");
        }

        public void Log(string logMessage)
        {
            File.AppendAllText(logFile,
                DateTime.Now.ToString("O") + ":: " + logMessage + "\r\n");
        }
    }

    public class RepositoryANotImplemented : IRepository
    {
        public IEnumerable<string> GetAllValues()
        {
            throw new NotImplementedException();
        }
    }

    public class RepositoryImplemented : IRepository
    {
        public IEnumerable<string> GetAllValues()
        {
            return new[] {"Values", "returned", "from", "implemented", "and", "injected", "repository"};
        }
    }
}