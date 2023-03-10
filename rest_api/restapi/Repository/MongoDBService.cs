using restapi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using restapi.Interface;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using static MongoDB.Driver.WriteConcern;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;
using DnsClient.Protocol;
using System.Formats.Asn1;

namespace restapi.Repository
{
    /// <summary>
    /// service provide for handle mongodb database
    /// needed to register in program.cs
    /// </summary>
    public class MongoDBService : IMongoDBService
    {
        //readoly var which represent metric collection in this class
        private readonly IMongoCollection<Metric> _metricCollection;

        /// <summary>
        /// Constructor
        /// the parameter pass the mongodb property setting in to used them as class varible
        /// </summary>
        /// <param name="mongoDBSetting"></param>
        public MongoDBService(IOptions<MongoDbSetting> mongoDBSetting)
        {
            MongoClient client = new MongoClient(mongoDBSetting.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSetting.Value.DatabaseName);
            _metricCollection = database.GetCollection<Metric>(mongoDBSetting.Value.CollectionName);

        }

        /// <summary>
        /// async function to insert one single document based on the parameter
        /// </summary>
        /// <param name="metric"></param>
        /// <returns></returns>
        public async Task CreateAsync(Metric metric)
        {
            await _metricCollection.InsertOneAsync(metric);
            return;
        }

        /// <summary>
        /// Function for return every documentation that matches the fields in metric.cs in a list
        /// </summary>
        /// <returns></returns>
        public async Task<List<Metric>> GetAsync()
        {
            //new BsonDocument used to make sure no filter is here
            //tolistasync - used to avoid cursor
            return await _metricCollection.Find(new BsonDocument()).ToListAsync();
        }

        /// <summary>
        /// function for return the document that matches with user input id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Metric?> GetIdAsync(string id)
        {
            return await _metricCollection.Find(x => x.ID == id).FirstOrDefaultAsync();

        }




        /// <summary>
        /// function to get data base on user input time
        /// </summary>
        /// <param name="upper"></param> is the boundary of time
        /// <param name="down"></param> is the boundary of time
        /// <returns></returns>
        public async Task<List<Metric>> GetTimeAsync(DateTime upper, DateTime lower)
        {
            return await _metricCollection.Find(x => x.Timestamp <= upper && x.Timestamp >= lower).ToListAsync();
        }


        public void WriteCsvToMemory(List<Metric> target)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ",",
                Encoding = Encoding.UTF8
            };


            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (TextWriter tw = new StreamWriter(memoryStream))
                using (CsvWriter csv = new CsvWriter(tw, csvConfig))
                {
                    csv.WriteRecords(target);
                }
            }

        }
    }
}
