using Application.Helper;
using Application.Interface.IService;
using MassTransit;
using SWD392.MessageBroker;

namespace Infrastructure.Messaging.Consumer
{
    public class CollectionReportStatusUpdateConsumer : IConsumer<CollectionReportStatusUpdateDTO>
    {
        private readonly ICitizenService citizenService;

        public CollectionReportStatusUpdateConsumer(
            ICitizenService citizenService)
        {
            this.citizenService = citizenService;
        }

        public async Task Consume(ConsumeContext<CollectionReportStatusUpdateDTO> context)
        {
            try
            {
                var message = context.Message;
                ServiceLogger.Logging(
                    Level.Infrastructure, $"Update collection report status: {message.Status}");
                await citizenService.UpdateCollectionReportStatus(message);
            }
            catch (Exception ex)
            {
                ServiceLogger.Error(
                    Level.Infrastructure, $"Failed when update collection report status: {ex.Message}");
            }
        }
    }
}
