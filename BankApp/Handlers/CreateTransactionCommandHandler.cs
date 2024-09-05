using BankApp.Commands;
using BankApp.Mappers;
using BankApp.Repository.Interfaces;
using MediatR;
using System.Xml;

namespace BankApp.Handlers
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Unit>
    {
        public IUnitOfWork _unitOfWork { get; private set; }
        public ITransactionMapper _transactionMapper;

        public CreateTransactionCommandHandler(IUnitOfWork unitOfWork, ITransactionMapper transactionMapper)
        {
            _unitOfWork = unitOfWork;
            _transactionMapper = transactionMapper;
        }

        public async Task<Unit> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            if (request.FormFiles == null || !request.FormFiles.Any())
            {
                throw new ArgumentException("No file uploaded");
            }

            foreach (var file in request.FormFiles)
            {
                var fileName = file.FileName;
                var xmlContent = new XmlDocument();

                try
                {
                    xmlContent.Load(file.OpenReadStream());
                }
                catch (Exception ex)
                {
                    throw new XmlException("The file that was provided is not of valid format", ex);
                }

                var transactionNode = xmlContent.SelectSingleNode("//TransakcioniRacunPrivredaIzvod/Stavke");

                if (transactionNode == null)
                {
                    throw new XmlException("XML structure is not valid.");
                }

                var transactionModel = await _transactionMapper.MapXmlToModel(transactionNode, fileName);

                _unitOfWork.RaiffeisenRsdRepository.Add(transactionModel);
                await _unitOfWork.SaveChangesAsync();
            }

        return Unit.Value;
        }
    }
}
