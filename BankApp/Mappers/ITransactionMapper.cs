using BankApp.Models;
using System.Xml;

namespace BankApp.Mappers
{
    public interface ITransactionMapper
    {
        Task<RaiffeisenRsd> MapXmlToModel(XmlNode xmlNode, string fileName);
    }
}
