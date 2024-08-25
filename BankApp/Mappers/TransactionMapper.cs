using BankApp.Models;
using System.Text.RegularExpressions;
using System.Xml;

namespace BankApp.Mappers
{
    public class TransactionMapper : ITransactionMapper
    {
        private readonly IConfiguration _configuration;

        public TransactionMapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<Transaction> MapXmlToModel(XmlNode xmlNode, string fileName)
        {
            var settings = _configuration.GetSection("TransactionSettings");
            string pattern = settings["Pattern"];
            string defaultDebit = settings["DefaultDebit"];
            var attributes = settings.GetSection("Attributes");

            string beforeTrim = xmlNode.Attributes[attributes["TransactionDate"]]?.Value;
            string afterTrim = Regex.Replace(beforeTrim, pattern, "");

            var model = new Transaction
            {
                TransactionDate = afterTrim,
                Reference = xmlNode.Attributes[attributes["Reference"]]?.Value,
                Recipient = xmlNode.Attributes[attributes["Recipient"]]?.Value,
                TransactionDescription = xmlNode.Attributes[attributes["TransactionDescription"]]?.Value,
                Debit = decimal.Parse(xmlNode.Attributes[attributes["Debit"]]?.Value ?? defaultDebit),
                FileName = fileName
            };

            return await Task.FromResult(model);
        }
    }
}
