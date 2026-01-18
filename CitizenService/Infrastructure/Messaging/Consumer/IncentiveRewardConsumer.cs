using Application.Helper;
using Application.Interface.IService;
using MassTransit;
using SWD392.MessageBroker;

namespace Infrastructure.Messaging.Consumer
{
    public class IncentiveRewardConsumer : IConsumer<IncentiveRewardDTO>
    {
        private readonly ICitizenService citizenService;

        public IncentiveRewardConsumer(
            ICitizenService citizenService)
        {
            this.citizenService = citizenService;
        }

        public async Task Consume(ConsumeContext<IncentiveRewardDTO> context)
        {
            try
            {
                var message = context.Message;
                ServiceLogger.Logging(
                    Level.Infrastructure, $"Update incentive reward: {message.Point}");
                await citizenService.UpdateIncentiveReward(message);
            }
            catch (Exception ex)
            {
                ServiceLogger.Error(
                    Level.Infrastructure, $"Failed when update incentive reward: {ex.Message}");
            }
        }


    }
}
